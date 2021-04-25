using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.ServiceLayer
{
    class UserController
    {
        public static bool login(string username, string password)
        {
            return BuissnessLayer.UserServices.login(username, password);
        }

        public static bool logout()
        {
            return BuissnessLayer.UserServices.logout();
        }

        public static bool register(string userName, string password)
        {
            return BuissnessLayer.UserServices.register(userName, password);
        }

        public static bool saveProduct(string userName, string storeName, string manufacturer, int amount, List<string> productName)
        {
            return BuissnessLayer.UserServices.saveProduct(string userName, string storeName, string manufacturer, int amount, List<string> productName);
        }
    }
}
