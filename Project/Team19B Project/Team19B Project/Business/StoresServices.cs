using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team19B_Project.Business
{
    public class StoresServices
    {
        public static Store getStore(string name)
        {
            Store output = null;
            try
            {
                output = Stores.Instance.stores[name];
            }catch (Exception) { }

            return output;
        }

        public static bool addStore(string name)
        {
            if (Stores.Instance.stores.ContainsKey(name))
                return false;

            Stores.Instance.stores.Add(name, new Store(name));
            return true;
        }

        public static bool removeStore(string name)
        {
            return Stores.Instance.stores.Remove(name);
        }

        public static double basketPrice(string storeName, Dictionary<int, int> basket)
        {
            // basket = <product_id, amount>
            // returns -1 if there is no store with name=storeName
            //         -2 if there are products in the basket and not in the store
            //         -3 if one of the wanted amounts in the basket is more than what is in the store
            Store store;

            try
            {
                store = Stores.Instance.stores[storeName];
            }
            catch (Exception) { return -1; }

            return store.basketPrice(basket);
        }
    }
}
