using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    class ShoppingBasketData
    {
        //FIELDS
        public string storeName { get; set; }
        public string useName { get; }

        //CONSTRUCTORS
        public ShoppingBasketData(string storeName, string useName)
        {
            this.storeName = storeName;
            this.useName = useName;
        }

        //EQUALS
        public override bool Equals(object obj)
        {
            return false;
        }

        public bool Equals(ShoppingBasketData other)
        {
            return this.storeName.Equals(other.storeName) & this.useName.Equals(other.useName);
        }
    }
}
