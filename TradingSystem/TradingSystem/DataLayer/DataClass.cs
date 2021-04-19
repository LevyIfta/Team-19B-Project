using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
    internal static class DataClass
    {
        private static Dictionary<string, Member> registered = new Dictionary<string, Member>();

        public static bool addRegistered(string username, string password)
        {
            registered.Add(username, new Member(username, password));
            return true;
        }
        public static userDetails confirmPassword(string username, string password)
        {
            if (registered[username].getPassword().Equals(password))
                return registered[username].GetUserDetails();
            return null;
        }
        public static bool isUserExists(string username)
        {
            return registered.ContainsKey(username);
        }

        public static Member getUser(string username){
            return registered[username];
        }
    }
}
