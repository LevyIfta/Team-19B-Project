using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team19B_Project.DataAccess;

namespace Team19B_Project.Business
{
    public class Store
    {
        public string name { get; set; }
        public ConcurrentDictionary<int, int> products { get; } // a dictionary of - <product_id, amount>
        public Dictionary<int, double> prices { get; } // <product_id, price>
        private Dictionary<int, bool> locks;
        private Object productsLock = new Object();

        public Store(string name)
        {
            this.name = name;
            this.products = new ConcurrentDictionary<int, int>();
            this.prices = new Dictionary<int, double>();
            this.locks = new Dictionary<int, bool>();
        }

        public bool addProducts(int productId, int amount)
        {
            // adds amount of product info
            // pre-conditions: DAL.Products.Stores.getStore(this.name).products.get(productId) != null
            //                 & amount > 0
            // post conditions: DAL.Stores.getStore(this.name).products.get(productId) = @DAL.Stores.getStore(this.name).products.get(productId) + amount
            if (amount <= 0)
                return false;

            try
            {
                this.products[productId] = this.products[productId] + amount;
            }
            catch (Exception) { return false; }

            return true;
        }

        public bool addProduct(int productId, double price)
        {
            // check if the product already exists
            if (this.products.ContainsKey(productId))
                return false;

            this.products.TryAdd(productId, 0);
            this.prices.Add(productId, price);

            return true;
        }

        public double basketPrice(Dictionary<int, int> basket)
        {
            // returns -2 if there are products in the basket and not in the store
            //         -3 if one of the wanted amounts in the basket is more than what is in the store
            lock (productsLock) {
                foreach (int key in basket.Keys)
                {
                    if (!this.products.ContainsKey(key))
                        return -2;
                    if (this.products[key] < basket[key])
                        return -3;
                }
                // the basket is ok
                // calc the total price
                double totalPrice = 0;
                foreach (int pId in basket.Keys)
                {
                    // add the price of the products in the basket to the total price
                    totalPrice += this.prices[pId] * basket[pId];
                    // update the amount in the store
                    this.products[pId] -= basket[pId];
                }

                return totalPrice;
             }
        }

        public bool purchaseBasket(Dictionary<int, int> basket)
        {
            return false;
        }

        public bool editPrice(int productId, double price)
        {
            if (!this.prices.ContainsKey(productId))
                return false;

            this.prices[productId] = price;
            return true;
        }

        
    }
}
