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
        private Object productsLock = new Object();

        public Store(string name)
        {
            this.name = name;
            this.products = new ConcurrentDictionary<int, int>();
            this.prices = new Dictionary<int, double>();
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

        public bool addProduct(int productId, double price, int amount)
        {
            // check for the amount
            if (amount <= 0)
                return false;
            // try to add the product to this.priducts
            if (!addProduct(productId, price))
                return false;
            // the product was added, update the amount
            if (this.products.TryAdd(productId, amount))
                return true;
            // the product amount wasn't added, delete the product
            removeProduct(productId);
            return false;
        }

        public bool removeProduct(int productId)
        {
            if (!this.products.ContainsKey(productId))
            {
                // check for the product in other data structures - there might be some bug
                if (this.prices.ContainsKey(productId))
                    this.prices.Remove(productId);
                return false;
            }
            // remove the product
            this.products.TryRemove(productId, out int ignored);
            // remove the price
            this.prices.Remove(productId);
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

        public bool purchaseBasket(Dictionary<int, int> basket, CreditCardInfo creditCard)
        {
            // calc the price
            double price = basketPrice(basket);
            if (price < 0)
                return false;
            // lock the shop
            lock (this.productsLock)
            {
                // check if the account has the money
                if (!PaymentSystem.pay(creditCard, price))
                    return false;
                // remove the products from the store
                foreach (int pId in basket.Keys)
                    this.products[pId] = this.products[pId] - basket[pId];
                return true;
            }
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
