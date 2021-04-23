using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer
{
    class Store
    {
        public string name { get; private set; }
        /*
        public ICollection<Receipt> receipts { get; private set; }
        public ICollection<Product> inventory { get; private set; }
        public ICollection<Member> owners { get; private set; }
        public ICollection<Member> managers { get; private set; }
        */
        public Member founder { get; private set; }

        public Store(string name, Member founder)
        {
            this.name = name;
            this.founder = founder;
            /*
            this.receipts = new List<Receipt>();
            this.inventory = new List<Product>();
            this.owners = new List<Member>();
            this.managers = new List<Member>();
            */
        }

        public Store(StoreData storeData)
        {
            this.name = storeData.name;
            this.founder = Member.dataToObject(storeData.founder);
        }

        public ProductInfo addProduct(string name, string category, string manufacturer)
        {
            ProductInfo productInfo = ProductInfo.getProductInfo(name, category, manufacturer);
            lock (StoresData.getStore(this.name))
            {
                // check if the product already exists in the store
                foreach (ProductData p in StoresData.getStore(this.name).inventory)
                    if (p.info.Equals(productInfo.toDataObject()))
                        return null;
                // the product doesn't exist, add it to the DB
                StoresData.getStore(this.name).inventory.Add(new ProductData(productInfo.toDataObject(), 0, -1));
            }
            return productInfo;
        }

        public bool editPrice(string name, string manufacturer, double newPrice)
        {
            // check if the product exists
            foreach (ProductData p in StoresData.getStore(this.name).inventory)
                if (p.info.name.Equals(name) & p.info.manufacturer.Equals(manufacturer))
                {
                    p.price = newPrice;
                    return true;
                }
            // the product doesn't exist, can't edit price
            return false;
        }

        public bool supply(string name, string manufacturer, int amount)
        {
            if (amount <= 0)
                return false;
            lock (StoresData.getStore(this.name).getPurchaseLock())
            {
                // check if the product exists
                foreach (ProductData p in StoresData.getStore(this.name).inventory)
                    if (p.info.name.Equals(name) & p.info.manufacturer.Equals(manufacturer))
                    {
                        p.amount += amount;
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
                price += StoresData.getStore(this.name).getPrice(product.info.toDataObject()) * product.amount;

            return price;
        }

        public Receipt executePurchase(ShoppingBasket basket, PaymentMethod paymentMethod)
        {
            ShoppingBasket cloned = basket.clone();
            ICollection<Product> products = basket.products;
            Receipt receipt = null;
            // lock the store for purchase
            lock (StoresData.getStore(this.name).getPurchaseLock())
            {
                // check for amounts validation
                if (checkAmounts(products) & checkPolicies(basket))
                {
                    // calc the price
                    double price = calcPrice(products);
                    // request for payment
                    if (paymentMethod.pay(price))
                    {
                        // create the receipt
                        receipt = new Receipt();
                        // the payment was successful
                        foreach (Product product in products)
                            StoresData.getStore(this.name).removeProducts(product.toDataObject());
                        // clean the basket
                        basket.clean();
                        // fill receipt fields
                        receipt.basket = cloned;
                        receipt.date = DateTime.Now;
                        receipt.price = price;
                        // save the receipt
                        StoresData.getStore(this.name).receipts.Add(receipt.toDataObject());
                    }
                }
            }

            return receipt;
        }

        private bool checkPolicies(ShoppingBasket basket)
        {
            return true;
        }

        private bool checkAmounts(ICollection<Product> products)
        {
            foreach (Product product in products)
                foreach (ProductData productData in StoresData.getStore(this.name).inventory)
                    if (product.toDataObject().Equals(productData) & product.amount > productData.amount)
                        return false;
            return true;
        }
        public void addOwner(Member owner)
        {
            //this.owners.Add(owner);
            // update data
            StoresData.getStore(this.name).addOwner(Member.objectToData(owner));
        }
        public void addManager(Member manager)
        {
            //this.managers.Add(manager);
            // update data
            StoresData.getStore(this.name).addManager(Member.objectToData(manager));
        }
        public void removeOwner(Member owner)
        {
            //this.owners.Remove(owner);
            // update data
            StoresData.getStore(this.name).removeOwner(Member.objectToData(owner));
        }

        public void removeManager(Member manager)
        {
            //this.managers.Remove(manager);
            // update data
            StoresData.getStore(this.name).removeManager(Member.objectToData(manager));
        }

        public bool isManager(Member member)
        {
            return StoresData.getStore(this.name).getManagers().Contains(Member.objectToData(member));
            //return this.managers.Contains(userrname);
        }

        public bool isOwner(Member member)
        {
            return StoresData.getStore(this.name).getOwners().Contains(Member.objectToData(member));
        }
        /*
        public ICollection<Member> getOwners()
        {
            return owners;
        }

        public ICollection<Member> getManagers()
        {
            return managers;
        }
        */
        public StoreData toDataObject()
        {
            // init the data object
            StoreData storeData = new StoreData(this.name, Member.objectToData(this.founder));
            // convert collections elements to data ocjects and add them to the store data object
            /*
            foreach (Member owner in this.owners)
                storeData.addOwner(Member.objectToData(owner));
            
            foreach (Member manager in this.managers)
                storeData.addManager(Member.objectToData(manager));

            foreach (Product product in this.inventory)
                storeData.addProduct(product.toDataObject());
                */
            return storeData;
        }

    }
}
