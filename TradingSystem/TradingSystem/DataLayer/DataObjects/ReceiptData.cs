using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    public class ReceiptData
    {
        //FIELDS
        public int receiptID { get; set; }
        public string storeName { get; set; }
        public string userName { get; set; }
        public double price { get; set; }
        public DateTime date { get; set; }
        public int discount { get; set; }
        public int purchasePolicy { get; set; }

        //CONSTRUCTORS
        public ReceiptData(int receiptID, string storeName, string userName, double price, DateTime date, int discount, int purchasePolicy)
        {
            this.receiptID = receiptID;
            this.storeName = storeName;
            this.userName = userName;
            this.price = price;
            this.date = date;
            this.discount = discount;
            this.purchasePolicy = purchasePolicy;
        }

        //EQUALS
        public override bool Equals(object obj)
        {
            return false;
        }

        public bool Equals(ReceiptData other)
        {
            return this.receiptID.Equals(other.receiptID) & this.storeName.Equals(other.storeName)
                & this.userName.Equals(other.userName) & this.price.Equals(other.price)
                & this.date.Equals(other.date) & this.discount.Equals(other.discount)
                & this.purchasePolicy.Equals(other.purchasePolicy);
        }
    }
}
