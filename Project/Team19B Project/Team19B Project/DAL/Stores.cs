using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team19B_Project.DAL
{
    public class Stores
    {
        // this class holds data about all the stores in the system
        // it provides all store-related services in terms of data access
        private Dictionary<string, Store> stores;

        public Stores()
        {
            this.stores = new Dictionary<string, Store>();
        }

        public bool addStore(Store store)
        {
            // check if there is a store with the given name, if so don't add the new store
            if (this.stores.ContainsKey(store.name))
                return false;
            this.stores.Add(store.name, store);
            return true;
        }

        public Store getStore(string name)
        {
            if (this.stores.ContainsKey(name))
                return this.stores[name];
            return null;
        }

        public void removeStore(string name)
        {
            if (this.stores.ContainsKey(name))
                this.stores.Remove(name);
        }
    }
}
