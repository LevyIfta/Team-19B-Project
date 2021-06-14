using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;
using TradingSystem.BuissnessLayer.commerce;


namespace TradingSystem.BuissnessLayer.commerce
{
    public static class Stores

    {
        public static Dictionary<string, Store> stores = new Dictionary<string, Store>();
        public static Dictionary<string, Store> storesClose = new Dictionary<string, Store>();
        public static bool addStore(string storeName, Member founder)
        {
            Store newStore = new Store(storeName, founder);
            //newStore.purchasePolicies = new LinkedList<Rules.iPolicy>();
            // check for a name duplicant
            if ((storeName == null | founder == null) || stores.ContainsKey(storeName))
                return false;
            stores.Add(storeName, newStore);
            // add the new store to DB
            DataLayer.ORM.DataAccess.create(newStore.toDataObject());
            SupplySystem.Supply.InformStore(storeName);
            return true;
        }

        public static Store searchStore(string storeName)
        {
            if (storeName == null || !stores.ContainsKey(storeName))
                return null;
            return stores[storeName];
        }

        public static void removeStore(Store store)
        {
            stores.Remove(store.name);
            // update DB TODO
            store.remove();
        }

        public static ICollection<Store> getAllStores()
        {
            return stores.Values;
        }

        public static Dictionary<Store, Product> searchProduct(string productName, string manufacturer)
        {
            Dictionary<Store, Product> result = new Dictionary<Store, Product>();
            foreach (Store store in stores.Values)
                if (store.searchProduct(productName, manufacturer) != null)
                    result.Add(store, store.searchProduct(productName, manufacturer));
            return result;
        }
        public static Dictionary<Store, Product> searchProduct(string productName, string category, string manufacturer, double minPrice, double maxPrice)
        {
            Dictionary<Store, Product> result = new Dictionary<Store, Product>();
            foreach (Store store in stores.Values)
                if (store.searchProduct(productName, manufacturer) != null)
                    result.Add(store, store.searchProduct(productName, category, manufacturer, minPrice, maxPrice));
            return result;
        }
        public static bool closeStore(Store store)
        {
            if ((store == null) || !stores.ContainsKey(store.name))
                return false;
            storesClose.Add(store.name, store);
            stores.Remove(store.name);
            // SuppluSystem.Supply.remove
            return true;
        }
        public static bool reopenStore(Store store)
        {
            if ((store == null) || stores.ContainsKey(store.name))
                return false;
            stores.Add(store.name, store);
            storesClose.Remove(store.name);
            // SupplySystem.Supply.open
            return true;
        }


        public static void loadStore(StoreData store)
        {
            Store newStore = new Store(store);
            stores.Add(store.storeName, newStore);
        }

    }
}