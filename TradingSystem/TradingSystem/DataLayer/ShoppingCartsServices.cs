using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
    public class ShoppingCartsServices
    {
        public static bool addProducts(string username, string storeName, int productId, int amount)
        {
            return ShoppingCarts.Instance.carts[username].baskets[storeName].addProducts(productId, amount);
        }

        public static bool removeProducts(string username, string storeName, int productId, int amount)
        {
            return ShoppingCarts.Instance.carts[username].baskets[storeName].removeProducts(productId, amount);
        }

        public static double basketPrice(string username, string storeName)
        {
            return ShoppingCarts.Instance.carts[username].baskets[storeName].calculatePrice();
        }

        public static bool purchaseBasket(string username, string storeName)
        {
            return ShoppingCarts.Instance.carts[username].baskets[storeName].checkout();
        }

        public static ShoppingBasket getBasket(string username, string store)
        {
            return ShoppingCarts.Instance.carts[username].baskets[store];
        }

        public static bool addCart(string username)
        {
            return ShoppingCarts.Instance.addCart(username);
        }
    }
}
