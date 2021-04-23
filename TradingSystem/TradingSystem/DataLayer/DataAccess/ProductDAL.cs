using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer.DataAccess
{
    static class ProductDAL
    {
        private static List<ProductData> products;

        public static ProductData getProduct(int productID)
        {
            foreach (ProductData productData in products)
            {
                if (productData.productID == productID)
                    return productData;
            }
            return null;
        }


        public static bool isExist(int productID)
        {
            foreach (ProductData productData in products)
            {
                if (productData.productID == productID)
                    return true;
            }
            return false;

        }

        public static void addProduct(ProductData productData)
        {
            products.Add(productData);
        }

        public static bool update(ProductData productData)
        {
            if (!products.Remove(productData))
                return false;
            products.Add(productData);
            return true;

        }

        public static bool remove(ProductData productData)
        {
            return products.Remove(productData);
        }

    }
}
