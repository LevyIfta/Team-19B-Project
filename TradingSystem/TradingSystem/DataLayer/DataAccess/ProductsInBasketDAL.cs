using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    static class ProductsInBasketDAL
    {
        private static List<ProductsInBasketData> ProductsInBaskets = new List<ProductsInBasketData>();

        public static ProductsInBasketData getProductsInBasket(string storeName, string useName, int productID)
        {
            foreach (ProductsInBasketData productsInBasketData in ProductsInBaskets)
            {
                if (productsInBasketData.storeName == storeName && productsInBasketData.useName == useName && productsInBasketData.productID == productID)
                    return productsInBasketData;
            }
            return null;
        }


        public static bool isExist(string storeName, string useName, int productID)
        {
            foreach (ProductsInBasketData productsInBasketData in ProductsInBaskets)
            {
                if (productsInBasketData.storeName == storeName && productsInBasketData.useName == useName && productsInBasketData.productID == productID)
                    return true;
            }
            return false;

        }

        public static void addProductsInBasket(ProductsInBasketData productsInBasketData)
        {
            ProductsInBaskets.Add(productsInBasketData);
        }

        public static bool update(ProductsInBasketData productsInBasketData)
        {
            if (!ProductsInBaskets.Remove(productsInBasketData))
                return false;
            ProductsInBaskets.Add(productsInBasketData);
            return true;

        }

        public static bool remove(ProductsInBasketData productsInBasketData)
        {
            return ProductsInBaskets.Remove(productsInBasketData);
        }

        public static ICollection<ProductsInBasketData> getProductIDs(string storeName, string useName)
        {
            ICollection<ProductsInBasketData> products = new List<ProductsInBasketData>();
            foreach (ProductsInBasketData productsInBasketData in ProductsInBaskets)
            {
                if (productsInBasketData.storeName == storeName && productsInBasketData.useName == useName)
                {
                    products.Add(productsInBasketData);
                }
            }
            return products;
        }
    }
}
