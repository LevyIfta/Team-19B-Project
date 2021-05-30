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
        public double age { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        //CONSTRUCTORS
        public MemberData(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
            this.age = -1;
            this.gender = "nun";
            this.address = "";
        }
        public MemberData(string userName, string password, double age, string gender, string address)
        {
            this.userName = userName;
            this.password = password;
            this.age = age;
            this.gender = gender;
            this.address = address;
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
