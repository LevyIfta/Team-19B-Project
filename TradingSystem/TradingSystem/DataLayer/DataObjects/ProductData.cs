using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    public class ProductData
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
            return this.productID.Equals(((ProductData)obj).productID);
        }

        public bool Equals(ProductData obj)
        {
            return this.productID.Equals(obj.productID);
        }
    }
}
