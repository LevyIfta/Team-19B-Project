using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;
using TradingSystem.BuissnessLayer;
using PaymentSystem;






namespace TradingSystem.BuissnessLayer.commerce
{
    public class Store
    {
        public string name { get; private set; }

        public ICollection<Receipt> receipts { get; private set; }
        public ICollection<Product> inventory { get; private set; }
        public ICollection<Member> owners { get; private set; }
        public ICollection<Member> managers { get; private set; }
        private Object purchaseLock = new Object();

        public Member founder { get; private set; }

        public Store(string name, Member founder)
        {
            this.name = name;
            this.founder = founder;

            this.receipts = new List<Receipt>();
            this.inventory = new List<Product>();
            this.owners = new List<Member>();
            this.managers = new List<Member>();
        }

        public Store(StoreData storeData)
        {
            this.name = storeData.storeName;
            this.founder = (Member)UserServices.getUser(storeData.founder);
            
            // fill the collections
            this.fillReceipts();
            this.fillInventory();
            this.fillOwners();
            this.fillManagers();
        }

        private void fillReceipts()
        {
            this.receipts = new LinkedList<Receipt>();
            ICollection<ReceiptData> receiptsData = ReceiptDAL.getStoreReceipts(this.name);
            foreach (ReceiptData receipt in receiptsData)
                this.receipts.Add(new Receipt(receipt));
        }

        private void fillInventory()
        {
            this.inventory = new LinkedList<Product>();
            ICollection<ProductData> productsData = ProductDAL.getStoreProducts(this.name);
            foreach (ProductData productData in productsData)
                this.inventory.Add(new Product(productData));
        }

        private void fillManagers()
        {
            this.managers = new LinkedList<Member>();
            ICollection<HireNewStoreManagerPermissionData> managersData = HireNewStoreManagerPermissionDAL.getStoreManagers(this.name);
            foreach (HireNewStoreManagerPermissionData manager in managersData)
                this.managers.Add((Member)UserServices.getUser(manager.userName));
        }

        private void fillOwners()
        {
            this.owners = new LinkedList<Member>();
            ICollection<HireNewStoreOwnerPermissionData> ownersData = HireNewStoreOwnerPermissionDAL.getStoreOwners(this.name);
            foreach (HireNewStoreOwnerPermissionData owner in ownersData)
                this.owners.Add((Member)UserServices.getUser(owner.userName));
        }

        public ProductInfo addProduct(string name, string category, string manufacturer)
        {
            ProductInfo productInfo = ProductInfo.getProductInfo(name, category, manufacturer);
            lock (this.purchaseLock)
            {
                // check if the product already exists in the store
                foreach (Product p in this.inventory)
                    if (p.info.Equals(productInfo.toDataObject()))
                        return null;
                // the product doesn't exist, add it
                this.inventory.Add(new Product(productInfo, 0, 0));
                // update DB
                ProductDAL.addProduct(new ProductData(productInfo.id, 0, 0, this.name));
            }
            return productInfo;
        }

        public void removeProduct(string productName, string manufacturer)
        {
            lock (this.purchaseLock)
            {
                foreach (Product product in this.inventory)
                    if (product.info.name.Equals(productName) & product.info.manufacturer.Equals(manufacturer))
                    {
                        this.inventory.Remove(product);
                        // update DB
                        product.remove(this.name);
                        return;
                    }
            }
        }

        public bool editPrice(string productName, string manufacturer, double newPrice)
        {
            // check if the product exists
            foreach (Product p in this.inventory)
                if (p.info.name.Equals(productName) & p.info.manufacturer.Equals(manufacturer))
                {
                    p.price = newPrice;
                    
                    // update DB
                    ProductDAL.update(new ProductData(p.info.id, p.amount, p.price, this.name));
                    return true;
                }
            // the product doesn't exist, can't edit price
            return false;
        }


        public bool supply(string name, string manufacturer, int amount)
        {
            if (amount <= 0)
                return false;
            lock (this.purchaseLock)
            {
                // check if the product exists
                foreach (Product p in this.inventory)
                    if (p.info.name.Equals(name) & p.info.manufacturer.Equals(manufacturer))
                    {
                        p.amount += amount;
                        // update DB
                        ProductDAL.update(new ProductData(p.info.id, p.amount, p.price, this.name));
                        return true;
                    }
            }
            // the product doesn't exist
            return false;
        }

        public double calcPrice(ICollection<Product> products)
        {
            double price = 0.0;

            foreach (Product product in products)
                foreach (Product localProduct in this.inventory)
                    if (localProduct.info.Equals(product.info))
                        price += localProduct.price * product.amount;

            return price;
        }

        public string[] executePurchase(ShoppingBasket basket, string creditNumber, string validity, string cvv)
        {
            ICollection<Product> products = basket.products;
            // lock the store for purchase
            Receipt receipt;
            lock (this.purchaseLock)
            {
                // check for amounts validation
                string policy = checkPolicies(basket);
                if (policy.Length == 0)
                    return new string[] { "false", policy };
                if (!checkAmounts(products))
                    return new string[] { "false", "not enough items in stock" };
                if(PaymentSystem.Verification.Pay(basket.owner.userName, creditNumber, validity, cvv))
                    return new string[] { "false", "payment not approved" };
                if(!SupplySystem.Supply.OrderPackage(name, basket.owner.userName, basket.owner.getAddress(), ""))
                    return new string[] { "false", "supply not approved" };
                receipt = validPurchase(basket);
                basket.owner.addReceipt(receipt);
                // clean the basket
                basket.clean();
                // update basket in DB
                basket.update();
                // update the origin store
                Stores.stores[this.name].inventory = this.inventory;
                Stores.stores[this.name].receipts = this.receipts;
            }

            return new string[] { "true", "" + receipt.receiptId }; ;
        }

        private Receipt validPurchase(ShoppingBasket basket)
        {
            // calc the price
            double price = calcPrice(basket.products);
            Receipt receipt = new Receipt();
            // request for payment
            // the payment was successful
            foreach (Product product in basket.products)
                foreach (Product localProduct in this.inventory)
                {
                    if (localProduct.info.Equals(product.info))
                    {
                        localProduct.amount -= product.amount;
                        // update amount in DB
                        localProduct.update(this.name);
                        // add the products to receipt
                        receipt.products.Add(localProduct.info.id, product.amount);
                        // 
                        receipt.actualProducts.Add(new Product(localProduct));
                        // leave feedback
                        product.info.leaveFeedback(basket.owner.userName, "");
                        // update feedback in DB
                        FeedbackDAL.addFeedback(new FeedbackData(localProduct.info.name, localProduct.info.manufacturer, basket.owner.userName, ""));
                    }
                    //StoresData.getStore(this.name).removeProducts(product.toDataObject());
                    product.info.roomForFeedback(basket.owner.userName);
                }
            // fill receipt fields
            receipt = fillReceipt(receipt, price);
            return receipt;
        }

        private Receipt fillReceipt(Receipt receipt, double price)
        {
            receipt.store = this;
            receipt.discount = 0;
            receipt.date = DateTime.Now;
            receipt.price = price;
            // save the receipt
            this.receipts.Add(receipt);
            // add receipt to DB
            receipt.save();
            return receipt;
        }

        private string checkPolicies(ShoppingBasket basket)
        {
            return "";
        }

        private bool checkAmounts(ICollection<Product> products)
        {
            foreach (Product product in products)
                foreach (Product productData in this.inventory)
                    if (product.info.Equals(productData.info) & product.amount > productData.amount)
                        return false;
            return true;
        }
        public void addOwner(Member owner)
        {
            this.owners.Add(owner);
            // update DB
            HireNewStoreOwnerPermissionDAL.addHireNewStoreOwnerPermission(new HireNewStoreOwnerPermissionData(owner.userName, this.name));
        }
        public void addManager(Member manager)
        {
            this.managers.Add(manager);
            // update DB
            HireNewStoreManagerPermissionDAL.addHireNewStoreManagerPermission(new HireNewStoreManagerPermissionData(manager.userName, this.name));
        }
        public void removeOwner(Member owner)
        {
            this.owners.Remove(owner);
            // update DB
            HireNewStoreOwnerPermissionDAL.remove(new HireNewStoreOwnerPermissionData(owner.userName, this.name));
        }

        public void removeManager(Member manager)
        {
            this.managers.Remove(manager);
            // update DB
            HireNewStoreManagerPermissionDAL.remove(new HireNewStoreManagerPermissionData(manager.userName, this.name));
        }

        public bool isManager(string member)
        {
            foreach (Member manager in this.managers)
                if (manager.getUserName().Equals(member))
                    return true;
            return false;
        }

        public bool isOwner(string member)
        {
            foreach (Member owner in this.owners)
                if (owner.getUserName().Equals(member))
                    return true;
            return false;
        }
        public ICollection<Member> getManagers()
        {
            return managers;
        }
        public ICollection<Member> getOwners()
        {
            return owners;
        }



        public override bool Equals(object obj)
        {
            return (obj is Store) & ((Store)obj).name.Equals(name);
        }
        public bool Equals(Store obj)
        {
            return obj.name.Equals(name);
        }

        public ICollection<Receipt> getAllReceipts()
        {
            return this.receipts;
        }

        public Product searchProduct(string productName, string manufacturer)
        {
            foreach (Product product in this.inventory)
                if (product.info.name.Equals(productName) && product.info.manufacturer.Equals(manufacturer))
                    return new Product(product); // clone so that the user cannot edit price/amout ...
            return null; // no results
        }

        public Product searchProduct(string productName, double minPrice, double maxPrice)
        {
            foreach (Product product in this.inventory)
                if (product.info.name.Equals(productName) & product.price <= maxPrice & product.price >= minPrice)
                    return new Product(product);
            return null; // no results
        }

        public Product searchProduct(string productName, string category, string manufacturer, double minPrice, double maxPrice)
        {
            foreach (Product product in this.inventory)
                if (product.info.name.Equals(productName) & product.price <= maxPrice & product.price >= minPrice & product.info.category.Equals(category) & product.info.manufacturer.Equals(manufacturer))
                    return new Product(product);
            return null; // no results
        }

        public bool isProductExist(string name, string manufacturer)
        {
            foreach (Product product in this.inventory)
                if (product.info.name.Equals(name) & product.info.manufacturer.Equals(manufacturer))
                    return true;
            return false;
        }

        public void remove()
        {
            StoreDAL.remove(this.toDataObject());
        }
        public Product getProduct(int productId)
        {
            foreach (Product product in this.inventory)
                if (product.info.id == productId)
                    return product;
            return null;
        }

        public StoreData toDataObject()
        {
            return new StoreData(this.name, this.founder.userName);
        }

        public void removeFromInventory(Product product)
        {
            lock (this.purchaseLock)
            {
                foreach (Product p in this.inventory)
                    if (p.info.Equals(product.info) & p.amount >= product.amount)
                        p.amount -= product.amount;
            }
        }
    }
}
