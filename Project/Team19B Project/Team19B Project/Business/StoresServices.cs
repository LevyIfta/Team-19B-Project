using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team19B_Project.DataAccess;

namespace Team19B_Project.Business
{
    public class StoresServices
    {
        public static Store getStore(string name)
        {
            Store output = null;
            try
            {
                output = Stores.Instance.stores[name];
            }catch (Exception) { }

            return output;
        }

        public static bool addStore(string name, string founder)
        {
            if (Stores.Instance.stores.ContainsKey(name))
                return false;

            Stores.Instance.stores.Add(name, new Store(name, founder));
            return true;
        }

        public static bool removeStore(string name)
        {
            return Stores.Instance.stores.Remove(name);
        }

        public static Dictionary<string, Dictionary<int, int>> searchProducts(string productName, double minPrice, double maxPrice, string category, string manufacturer)
        {
            Dictionary<string, Dictionary<int, int>> products = new Dictionary<string, Dictionary<int, int>>();

            // search for the product in all stores
            foreach (string storeName in Stores.Instance.stores.Keys) {
                Store store = getStore(storeName);
                foreach (int productId in store.products.Keys)
                {
                    double price = store.prices[productId];
                    ProductInfo product = Products.Instance.products[productId];
                    Dictionary<int, int> currentProducts = new Dictionary<int, int>();
                    if (product.name.Equals(productName) & product.category.Equals(category) & product.manufacturer.Equals(manufacturer) & price <= maxPrice & price >= minPrice)
                        currentProducts.Add(productId, store.products[productId]);
                    if (currentProducts.Count > 0)
                        products.Add(storeName, currentProducts);
                }
            }


            return products;
        }
        /*
        public static double basketPrice(string storeName, ShoppingBasket basket)
        {
            // basket = <product_id, amount>
            // returns -1 if there is no store with name=storeName
            //         -2 if there are products in the basket and not in the store
            //         -3 if one of the wanted amounts in the basket is more than what is in the store
            Store store;

            try
            {
                store = Stores.Instance.stores[storeName];
            }
            catch (Exception) { return -1; }

            return store.basketPrice(basket);
        }
        */
    }
}