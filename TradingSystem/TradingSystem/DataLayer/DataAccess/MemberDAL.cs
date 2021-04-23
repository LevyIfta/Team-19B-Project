using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TradingSystem.DataLayer
{
    static class MemberDAL
    {
        private static List<MemberData> members;

        /// <summary>
        /// retrive a user from the db
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>incase it cant find the user, return null</returns>
        public static MemberData getMember(string userName)
        {
            foreach (MemberData memberData in members)
            {
                if (memberData.userName == userName)
                    return memberData;
            }
            return null;
        }


        public static bool isExist(string username)
        {
            foreach (MemberData user in members)
            {
                if (user.userName == username)
                    return true;
            }
            return false;
            
        }

        public static void addMember(string userName, string password)
        {
            members.Add(new MemberData(userName, password));
        }

        public static bool update(string userName, string password)
        {
            MemberData temp = new MemberData(userName, password);
            if (!members.Remove(temp))
                return false;
            members.Add(temp);
            return true;
 
        }

        public static bool remove(string userName, string password)
        {
            return members.Remove(new MemberData(userName, password));
        }

    }
}
