using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    class ProductsInBasketData
    {
        //FIELDS
        public string storeName { get; set; }
        public string useName { get; }
        public int productID { get; set; }
        public int amount { get; set; }

        //CONSTRUCTORS
        public ProductsInBasketData(string storeName, string useName, int productID, int amount)
        {
            this.storeName = storeName;
            this.useName = useName;
            this.productID = productID;
            this.amount = amount;
        }

        //EQUALS
        public override bool Equals(object obj)
        {
            return false;
        }

        public bool Equals(ProductsInBasketData other)
        {
            return this.storeName.Equals(other.storeName) & this.useName.Equals(other.useName)
                & this.productID.Equals(other.productID) & this.amount.Equals(other.amount);
        }
    }
}
