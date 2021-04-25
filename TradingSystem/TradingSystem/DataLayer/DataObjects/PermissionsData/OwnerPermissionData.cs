using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    class OwnerPermissionData
    {
        //FIELDS
        public string ownerName { get; set; }
        public string storeName { get; set; }

        //CONSTRUCTORS
        public OwnerPermissionData(string ownerName, string storeName)
        {
            this.ownerName = ownerName;
            this.storeName = storeName;
        }

        //EQUALS
        public override bool Equals(object obj)
        {
            return false;
        }

        public bool Equals(OwnerPermissionData other)
        {
            return this.ownerName.Equals(other.ownerName) & this.storeName.Equals(other.storeName);
        }
    }
}