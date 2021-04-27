using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    static class BasketDAL
    {
        private static List<BasketData> shoppingBaskets = new List<BasketData>();

        public static BasketData getShoppingBasket(string storeName, string useName)
        {
            foreach (BasketData BasketData in shoppingBaskets)
            {
                if (BasketData.storeName == storeName && BasketData.useName == useName)
                    return BasketData;
            }
            return null;
        }


        public static bool isExist(string storeName, string useName)
        {
            foreach (BasketData BasketData in shoppingBaskets)
            {
                if (BasketData.storeName == storeName && BasketData.useName == useName)
                    return true;
            }
            return false;

        }

        public static void addShoppingBasket(BasketData BasketData)
        {
            shoppingBaskets.Add(BasketData);
        }

        public static bool update(BasketData BasketData)
        {
            if (!shoppingBaskets.Remove(BasketData))
                return false;
            shoppingBaskets.Add(BasketData);
            return true;

        }

        public static bool remove(BasketData BasketData)
        {
            return shoppingBaskets.Remove(BasketData);
        }
    }
}
