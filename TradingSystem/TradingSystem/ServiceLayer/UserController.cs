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

        public static bool register(string username, string password)
        {
            return BuissnessLayer.UserServices.register(username, password);
        }


    }
}
