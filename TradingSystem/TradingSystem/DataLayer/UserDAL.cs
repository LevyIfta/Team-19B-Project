using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TradingSystem.DataLayer
{
    static class UserDAL
    {
        private static List<memberData> members;

        /// <summary>
        /// retrive a user from the db
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>incase it cant find the user, return null</returns>
        public static memberData getUser(string username, string password)
        {
            memberData temp = new memberData(username, password);
            try
            {
                temp = members.Find((other) => { return other.CompareTo(temp) == 0; });
            }
            catch
            {
                return null;
            }
            return temp;
        }


        public static bool isExist(string username)
        {
            foreach (memberData user in members)
            {
                if (user.sameUser(username))
                    return true;
            }
            return false;
            
        }

        public static void addUser(memberData user)
        {
            members.Add(user);
        }

        public static bool update(memberData old, memberData newData)
        {
            if (!members.Remove(old))
                return false;
            members.Add(newData);
            return true;
 
        }

        public static bool remove(memberData member)
        {

            return members.Remove(member);
        }

    }
}
