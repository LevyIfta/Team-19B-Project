using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
    static class DataClass
    {
        private static userDetails nothingImportant = new userDetails();
        private static Dictionary<string, string> registered = new Dictionary<string, string>();
        private static Dictionary<string, string> managers = new Dictionary<string, string>();
        private static Dictionary<string, string> owners = new Dictionary<string, string>();

        public static void addRegistered(string username, string password)
        {
            registered.Add(username, password);
        }
        public static userDetails confirmPassword(string username, string password)
        {
            if (registered[username].Equals(password))
                return nothingImportant;
            return null;
        }
        public static bool isUserExists(string username)
        {
            return registered.ContainsKey(username);
        }
    }
}
