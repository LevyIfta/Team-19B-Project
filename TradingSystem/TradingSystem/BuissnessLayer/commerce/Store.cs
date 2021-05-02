using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;
using TradingSystem.BuissnessLayer;

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

        public Receipt executePurchase(ShoppingBasket basket, PaymentMethod paymentMethod)
        {
            ICollection<Product> products = basket.products;
            Receipt receipt = null;
            // lock the store for purchase
            lock (this.purchaseLock)
            {
                // check for amounts validation
                if (checkAmounts(products) & checkPolicies(basket))
                {
                    validPurchase(basket, paymentMethod, receipt);
                }
            }

            return receipt;
        }

        private void validPurchase(ShoppingBasket basket, PaymentMethod paymentMethod, Receipt receipt)
        {
            // calc the price
            double price = calcPrice(basket.products);
            // request for payment
            if (paymentMethod.pay(price))
            {
                // create the receipt
                receipt = new Receipt();
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

                // clean the basket
                basket.clean();
                // update basket in DB
                basket.update();
                // fill receipt fields
                fillReceipt(receipt, price);
            }
        }

        private void fillReceipt(Receipt receipt, double price)
        {
            receipt.store = this;
            receipt.discount = 0;
            receipt.date = DateTime.Now;
            receipt.price = price;
            // save the receipt
            this.receipts.Add(receipt);
            // add receipt to DB
            receipt.save();
        }

        private bool checkPolicies(ShoppingBasket basket)
        {
            return true;
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
                    return new Product(product);
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
    }
}
