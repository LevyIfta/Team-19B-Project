using System;
using System.Collections.Generic;
using System.Text;


namespace SupplySystem
{
    public class SupplyProxy : SupplyInterface
    {
        public SupplyInterface real { get; set; }
        private static ICollection<string> StoreList;
        public bool OrderPackage(string storeName, string userName, string address, string orderInfo)
        { // orderInfo -> order_order_order -> product$1
            if (storeName == null || userName == null || orderInfo == null)
                return false;
            if (storeName.Length == 0 || userName.Length == 0 || orderInfo.Length == 0)
                return false;
            if (!StoreList.Contains(storeName))
                return false;
            string[] product = orderInfo.Split('_');
            for (int i = 0; i < product.Length; i++)
            {
                string[] info = product[i].Split('$');
                if (info.Length != 2)
                    return false;
                if (containLatter(info[1]))
                    return false;
            }

            if (this.real == null)
                return true;

            return this.real.OrderPackage(storeName, userName, address, orderInfo);
        }
        public void InformStore(string storeName)
        {
            if (StoreList == null)
                StoreList = new List<string>();
            if (!StoreList.Contains(storeName))
                StoreList.Add(storeName);
        }
        public bool RemoveStore(string storeName)
        {
            if (StoreList == null)
                return false;
            if (!StoreList.Contains(storeName))
                return false;
            StoreList.Remove(storeName);
            return true;
        }
        private bool containNumber(string str)
        {
            foreach (char letter in str)
            {
                if (48 <= (int)letter && (int)letter <= 57)
                    return true;
            }
            return false;
        }
        private bool containLatter(string str)
        {
            foreach (char letter in str)
            {
                if (97 <= (int)letter && (int)letter <= 122)
                    return true;
            }
            return false;
        }

        public void setReal(SupplyInterface real)
        {
            this.real = real;
        }
    }
}
