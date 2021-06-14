using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.ServiceLayer
{
    public class StoreController
    {
        public static SLstore makeSLstore(BuissnessLayer.commerce.Store store)
        {
            if(store != null)
            {
                Object[] parameters = new Object[SLstore.PARAMETER_COUNT];
                ICollection<string> ownerNames = new List<string>();
                foreach (BuissnessLayer.aUser owner in store.owners)
                {
                    ownerNames.Add(owner.getUserName());
                }
                ICollection<string> managerNames = new List<string>();
                foreach (BuissnessLayer.aUser manager in store.managers)
                {
                    managerNames.Add(manager.getUserName());
                }
                parameters[0] = store.name;
                parameters[1] = ProductController.makeSLreceiptCollection(store.receipts);
                parameters[2] = ProductController.makeSLproductCollection(store.inventory);
                parameters[3] = ownerNames;
                parameters[4] = managerNames;
                parameters[5] = store.founder.userName;
                return (new SLstore(parameters));
            }
            return null;
            
        }

        public static SLstore searchStore(string storeName)
        {
            BuissnessLayer.commerce.Store store = BuissnessLayer.commerce.Stores.searchStore(storeName);
            return StoreController.makeSLstore(store);
        }

    }
}
