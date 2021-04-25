using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    static class ProductsInReceiptDAL
    {
        private static List<ProductsInReceiptData> ProductsInReceipts;

        public static ProductsInReceiptData getProductsInBasket(int receiptID, int productID)
        {
            foreach (ProductsInReceiptData productsInReceiptData in ProductsInReceipts)
            {
                if (productsInReceiptData.receiptID == receiptID && productsInReceiptData.productID == productID)
                    return productsInReceiptData;
            }
            return null;
        }


        public static bool isExist(int receiptID, int productID)
        {
            foreach (ProductsInReceiptData productsInReceiptData in ProductsInReceipts)
            {
                if (productsInReceiptData.receiptID == receiptID && productsInReceiptData.productID == productID)
                    return true;
            }
            return false;

        }

        public static void addProductsInBasket(ProductsInReceiptData productsInReceiptData)
        {
            ProductsInReceipts.Add(productsInReceiptData);
        }

        public static bool update(ProductsInReceiptData productsInReceiptData)
        {
            if (!ProductsInReceipts.Remove(productsInReceiptData))
                return false;
            ProductsInReceipts.Add(productsInReceiptData);
            return true;

        }

        public static bool remove(ProductsInReceiptData productsInReceiptData)
        {
            return ProductsInReceipts.Remove(productsInReceiptData);
        }
    }
}
