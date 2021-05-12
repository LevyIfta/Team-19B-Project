using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    static class RemoveProductDAL
    {
        private static List<RemoveProductData> RemoveProduct = new List<RemoveProductData>();

        public static RemoveProductData getEditManagerPermissions(string userName, string storeName)
        {
            foreach (RemoveProductData removeProductData in RemoveProduct)
            {
                if (removeProductData.userName == userName && removeProductData.storeName == storeName)
                    return removeProductData;
            }
            return null;
        }


        public static bool isExist(string userName, string storeName)
        {
            foreach (RemoveProductData removeProductData in RemoveProduct)
            {
                if (removeProductData.userName == userName && removeProductData.storeName == storeName)
                    return true;
            }
            return false;

        }

        public static void addEditManagerPermissions(RemoveProductData removeProductData)
        {
            RemoveProduct.Add(removeProductData);
        }

        public static bool update(RemoveProductData removeProductData)
        {
            if (!RemoveProduct.Remove(removeProductData))
                return false;
            RemoveProduct.Add(removeProductData);
            return true;

        }

        public static bool remove(RemoveProductData removeProductData)
        {
            return RemoveProduct.Remove(removeProductData);
        }
    }
}
