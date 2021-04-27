using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    static class MemberDAL
    {
        private static List<MemberData> members = new List<MemberData>();

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

        public static void addMember(MemberData memberData)
        {
            members.Add(memberData);
        }

        public static bool update(MemberData memberData)
        {
            if (!members.Remove(memberData))
                return false;
            members.Add(memberData);
            return true;
 
        }

        public static bool remove(MemberData memberData)
        {
            return members.Remove(memberData);
        }

    }
}
