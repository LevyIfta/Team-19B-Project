using System;
using System.Collections.Generic;
using System.Text;
using TradingSystem;



namespace ProductDeliverySystem
{
    public static class Supply
    {
        private static ICollection<string> StoreList;
        public static bool OrderPackage(string storeName, string userName, string address, string orderInfo)
        { // orderInfo -> order_order_order -> product$1
            if (storeName == null || userName == null || address == null || orderInfo == null)
                return false;
            if (storeName.Length == 0 || userName.Length == 0 || address.Length == 0 || orderInfo.Length == 0)
                return false;
            if (!StoreList.Contains(storeName))
                return false;
            string[] product = orderInfo.Split('_');
            for (int i = 0; i < product.Length; i++)
            {
                string[] info = product[i].Split('$');
                if (info.Length != 2)
                    return false;
                if (containNumber(info[0]) || containLatter(info[1]))
                    return false;
            }
            return true;
        }
        public static void InformStore(string storeName)
        {
            if (StoreList == null)
                StoreList = new List<string>();
            if (!StoreList.Contains(storeName))
                StoreList.Add(storeName);
        }
        public static bool RemoveStore(string storeName)
        {
            if (StoreList == null)
                return false;
            if (!StoreList.Contains(storeName))
                return false;
            StoreList.Remove(storeName);
            return true;
        }
        private static bool containNumber(string str)
        {
            foreach (char letter in str)
            {
                if (48 <= (int)letter && (int)letter <= 57)
                    return true;
            }
            return false;
        }
        private static bool containLatter(string str)
        {
            foreach (char letter in str)
            {
                if (97 <= (int)letter && (int)letter <= 122)
                    return true;
            }
            return false;
        }
    }
}
