using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    static class ShoppingBasketDAL
    {
        private static List<ShoppingBasketData> shoppingBaskets;

        public static ShoppingBasketData getShoppingBasket(string storeName, string useName)
        {
            foreach (ShoppingBasketData shoppingBasketData in shoppingBaskets)
            {
                if (shoppingBasketData.storeName == storeName && shoppingBasketData.useName == useName)
                    return shoppingBasketData;
            }
            return null;
        }


        public static bool isExist(string storeName, string useName)
        {
            foreach (ShoppingBasketData shoppingBasketData in shoppingBaskets)
            {
                if (shoppingBasketData.storeName == storeName && shoppingBasketData.useName == useName)
                    return true;
            }
            return false;

        }

        public static void addShoppingBasket(ShoppingBasketData shoppingBasketData)
        {
            shoppingBaskets.Add(shoppingBasketData);
        }

        public static bool update(ShoppingBasketData shoppingBasketData)
        {
            if (!shoppingBaskets.Remove(shoppingBasketData))
                return false;
            shoppingBaskets.Add(shoppingBasketData);
            return true;

        }

        public static bool remove(ShoppingBasketData shoppingBasketData)
        {
            return shoppingBaskets.Remove(shoppingBasketData);
        }
    }
}
