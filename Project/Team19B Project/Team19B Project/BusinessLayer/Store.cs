using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team19B_Project.DAL;

namespace Team19B_Project.BusinessLayer
{
    class Store
    {
        public string name { get; set; }
        public Dictionary<int, int> products { get; } // a dictionary of - <product_id, amount>
        private Dictionary<int, bool> locks;

        public Store(string name)
        {
            this.name = name;
            this.products = new Dictionary<int, int>();
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
            catch (Exception e) { return false; }

            return true;
        }

        
    }
}
