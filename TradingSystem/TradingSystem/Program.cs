using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // init stores names and founders usernames
            string store1 = "My Store",
                store2 = "Your Store";
            string store1_founder = "Ali",
                store2_founder = "Not Ali";

            // add stores
            StoresServices.addStore(store1, store1_founder);
            StoresServices.addStore(store2, store2_founder);

            // create new products
            ProductInfo product1 = new ProductInfo("Bamba", "not that bad", "Food", "Osem");
            ProductInfo product2 = new ProductInfo("Doritos", "great", "Food", "Osem");

            // add the products to the singleton
            int id1 = Products.Instance.addProduct(product1);
            int id2 = Products.Instance.addProduct(product2);

            // add the products to the stores
            StoresServices.getStore(store1).addProduct(id1, 3.0);
        }
    }
}
