using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    public class MemberData
    {
        //FIELDS
        public string userName { get; set; }
        public string password { get; set; }

        //CONSTRUCTORS
        public MemberData(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        //EQUALS
        public override bool Equals(object obj)
        {
            return false;
        }

        public  bool Equals(MemberData other)
        {
            return this.userName.Equals(other.userName) & this.password.Equals(other.password);
        }
    }
}
