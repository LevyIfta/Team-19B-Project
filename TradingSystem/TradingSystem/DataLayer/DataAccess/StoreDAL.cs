using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    static class StoreDAL
    {
        private static List<StoreData> stores;

        public static StoreData getStore(string storeName)
        {
            foreach (StoreData storeData in stores)
            {
                if (storeData.storeName == storeName)
                    return storeData;
            }
            return null;
        }


        public static bool isExist(string storeName)
        {
            foreach (StoreData storeData in stores)
            {
                if (storeData.storeName == storeName)
                    return true;
            }
            return false;

        }

        public static void addStore(StoreData storeData)
        {
            stores.Add(storeData);
        }

        public static bool update(StoreData storeData)
        {
            if (!stores.Remove(storeData))
                return false;
            stores.Add(storeData);
            return true;

        }

        public static bool remove(StoreData storeData)
        {
            return stores.Remove(storeData);
        }
    }
}
