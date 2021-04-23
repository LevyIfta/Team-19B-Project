using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer.commerce
{
    class Stores
    {
        public static Store addStore(string storeName, Member founder)
        {
            // check for a name duplicant
            if (StoresData.getStore(storeName) != null)
                return null;
            Store newStore = new Store(storeName, founder);
            // add the new store to StoresData
            StoresData.addStore(newStore.toDataObject());
            return newStore;
        }

        public static Store searchStore(string storeName)
        {
            StoreData storeData = StoresData.getStore(storeName);
            return storeData == null ? null : new Store(storeData);
        }

        public static void removeStore(Store store)
        {
            StoresData.removeStore(store.toDataObject());
        }
    }
}