using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer
{
    static class UserServices
    {

        public static Member login(string username, string password)
        {
            memberData data = UserDAL.getUser(username, password);
            return Member.dataToObject(data);

        }
        public static bool register(string username, string password)
        {
            if (UserDAL.isExist(username))
                return false;
            UserDAL.addUser(new memberData(username, password));
            return true;
        }
    }
}
