using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    class ProductData
    {
        //FIELDS
        public int productID { get; set; }
        public int amount { get; set; }
        public double price { get; set; }
        public string storeName { get; set; }

        //CONSTRUCTORS
        public ProductData (int productID, int amount, double price, string storeName)
        {
            this.productID = productID;
            this.amount = amount;
            this.price = price;
            this.storeName = storeName;
        }

        //EQUALS
        public override bool Equals(object obj)
        {
            return false;
        }

        public bool Equals(ProductData other)
        {
            return this.productID.Equals(other.productID) & this.amount.Equals(other.amount) & this.price.Equals(other.price);
        }
    }
}
