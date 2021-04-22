using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer;

namespace TradingSystem.DataLayer
{
    class StoresData
    {
        private static Dictionary<string, StoreData> stores = new Dictionary<string, StoreData>();

        internal static StoreData getStore(string storeName)
        {
            return stores[storeName];
        }

        internal static void addStore(StoreData storeData)
        {
            stores.Add(storeData.name, storeData);
        }


    }
}