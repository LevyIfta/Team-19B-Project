using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    class RemoveProductData
    {
        //FIELDS
        public string userName { get; set; }
        public string storeName { get; set; }


        //CONSTRUCTORS
        public RemoveProductData(string userName, string storeName)
        {
            this.userName = userName;
            this.storeName = storeName;
        }

        //EQUALS
        public override bool Equals(object obj)
        {
            return false;
        }

        public bool Equals(RemoveProductData other)
        {
            return this.userName.Equals(other.userName) & this.storeName.Equals(other.storeName);
        }
    }
}
