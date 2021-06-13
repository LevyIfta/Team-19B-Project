using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    public class ReceiptData
    {
        //FIELDS
        [Key]
        public int receiptID { get; set; }
        public virtual BasketInRecipt basket { get; set; }

        public double price { get; set; }
        public DateTime date { get; set; }
        public iPolicyDiscountData discount { get; set; }
        public iPolicyData purchasePolicy { get; set; }

        //CONSTRUCTORS
        public ReceiptData(int receiptID, BasketInRecipt basket , double price, DateTime date, iPolicyDiscountData discount, iPolicyData purchasePolicy)
        {
            this.receiptID = receiptID;
            this.basket = basket;
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
            return this.receiptID == other.receiptID;
        }
    }
}
