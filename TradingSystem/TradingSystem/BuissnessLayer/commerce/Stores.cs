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
        public static ICollection<Store> stores = new LinkedList<Store>();
        public static Store addStore(string storeName, Member founder)
        {
            Store newStore = new Store(storeName, founder);
            // check for a name duplicant
            if (stores.Contains(newStore))
                return null;
            stores.Add(newStore);
            // add the new store to DB TODO
            StoreDAL.addStore(newStore.toDataObject());
            return newStore;

        }

        public static Store searchStore(string storeName)
        {
            foreach (Store store in stores)
                if (store.name.Equals(storeName))
                    return store;
            return null;

        }

        public static void removeStore(Store store)
        {
            stores.Remove(store);
            // update DB TODO
            store.remove();
        }

        public static ICollection<Store> getAllStores()
        {
            return stores;
        }
        public static ICollection<Store> getAllStores()
        {
            throw new NotImplementedException();
        }
        public static Dictionary<Store, Product> searchProduct(string productName, string category, string manufacturer, double minPrice, double maxPrice)
        {
            throw new NotImplementedException();
        }
        public static Dictionary<Store, Product> searchProduct(string productName)
        {
            throw new NotImplementedException();
        }

        
    }
}