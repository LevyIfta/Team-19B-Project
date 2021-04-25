using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer
{
    public static class UserServices
    {
        public static ICollection<string> onlineUsers { get; private set; }
        public static ICollection<string> offlineUsers { get; private set; }

        // menu functions
        // users
        public static Member login(string username, string password)
        {
            memberData data = MemberDAL.getUser(username, password);
            return Member.dataToObject(data);

        }
        public static bool register(string username, string password)
        {
            if (MemberDAL.isExist(username))
                return false;
            if (!checkUserNameValid(username))
                return false;
            if (!checkPasswordValid(password))
                return false;
            MemberDAL.addUser(new memberData(username, password));
            return true;
        }
        public static int countOnlineUsers()
        {
            return onlineUsers.Count;
        }
        public static bool isUserOnline(string username)
        {
            return onlineUsers.Contains(username);
        }
        

        // users
        public static aUser gerUser(string username)
        {
            throw new NotImplementedException();
        }
        

        // other
        private static bool checkUserNameValid(string username)
        {
            if(username == null || username.Length < 4 || containNumber(username))
            {
                return false;
            }
            return true;
        }
        private static bool checkPasswordValid(string password)
        {
            if (password == null || password.Length < 4 || password.Length > 20 || !containNumber(password) || !containLatter(password) || !containCapital(password))
            {
                return false;
            }
            return true;
        }
        private static bool containNumber(string str)
        {
            foreach (char letter in str)
            {
                if (122 >= (int)letter & (int)letter >= 97)
                    return true;
            }
            return false;
        }
        private static bool containCapital(string str)
        {
            foreach (char letter in str)
            {
                if (60 <= (int)letter && (int)letter <= 90)
                    return true;
            }
            return false;
        }
        private static bool containLatter(string str)
        {
            foreach (char letter in str)
            {
                if (97 <= (int)letter && (int)letter <= 122)
                    return true;
            }
            return false;
        }

    }
}
