using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer;

namespace TradingSystem.ServiceLayer
{
    public static class SeviceClass
    {
        private static aUser user { get; set; }

        public static bool login(string username, string password)
        {
            if (user.getUserName() != "guest")
                return false;
            aUser temp = UserServices.login(username, password);
            if (temp == null)
                return false;
            user = temp;
            return true;
        }


        public static bool register(string username, string password)
        {
            return UserServices.register(username, password);
        }

    }
}
