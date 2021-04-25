using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    public class memberData : IComparable
    {
        string userName;
        string password;

        public memberData(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        public int CompareTo(object obj)
        {
            return -1;
        }

        public int CompareTo(memberData other)
        {
            if(this.userName == other.userName & this.password == other.password)
                return 0;
            return 1;
        }
        public bool sameUser(string userName)
        {
            return this.userName == userName;
        }

        public string getUsername() { return this.userName; }
    }
}
