using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    public class ProductsInReceiptData
    {
        //FIELDS
        public int receiptID { get; set; }
        public int productID { get; set; }
        public int amount { get; set; }

        //CONSTRUCTORS
        public ProductsInReceiptData(int receiptID, int productID, int amount)
        {
            this.receiptID = receiptID;
            this.productID = productID;
            this.amount = amount;
        }

        //EQUALS
        public override bool Equals(object obj)
        {
            return false;
        }

        public bool Equals(ProductsInReceiptData other)
        {
            return this.receiptID.Equals(other.receiptID) & this.productID.Equals(other.productID) & this.amount.Equals(other.amount);
        }
    }
}
