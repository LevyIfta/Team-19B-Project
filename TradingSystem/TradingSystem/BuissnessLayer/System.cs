using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TradingSystem.DataLayer.ORM;
using TradingSystem.DataLayer;
using TradingSystem.BuissnessLayer.commerce;

namespace TradingSystem.BuissnessLayer
{
    public static class Build
    {
        
        private static bool unLoaded = true;
        private static Queue<ThreadStart> linkings { get; set; } = new Queue<ThreadStart>();
        /// <summary>
        /// load all data from DB into memory
        /// load the 3 standalone object: productinfo, user and store.
        /// if during loading an object require data that hasnt been loaded yet, it will add a requst to aquire to the linking queue (via AddLink), and that will be run after all other loading is done
        /// </summary>
        public static void Load()
        {
            if(unLoaded)
            {
                unLoaded = false;

                //load product info
                ICollection<ProductInfoData> productInfos = DataAccess.getAllProductsInfo();
                foreach (ProductInfoData item in productInfos)
                {
                    ProductInfo.AddProductInfo(item);
                }

                //load users
                ICollection<MemberData> members = DataAccess.getAllMembers();
                foreach (MemberData item in members)
                {
                    UserServices.LoadUser(item);

                }

                // load stores

                ICollection<StoreData> stores = DataAccess.getAllStore();
                foreach (StoreData item in stores)
                {
                    Stores.loadStore(item);
                }

                //run all linkings
                foreach (ThreadStart link in linkings)
                {
                    link.Invoke();
                }
            }

        }

        public static void addLink(ThreadStart func)
        {
            linkings.Enqueue(func);
        }
    }

    public static class TearDown
    {
        public static void tearDown()
        {
            DataLayer.ORM.DataAccess.tearDown();
        }
    }
}
