using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public MemberData user { get; set; }
        public StoreData store { get; set; }
        public DateTime date { get; set; }
        [NotMapped]
        public iPolicyDiscountData discount { get; set; }
        [NotMapped]
        public iPolicyData purchasePolicy { get; set; }

        //CONSTRUCTORS
        public ReceiptData(int receiptID, BasketInRecipt basket, StoreData store, MemberData user, double price, DateTime date, iPolicyDiscountData discount, iPolicyData purchasePolicy)
        {
            this.receiptID = receiptID;
            this.basket = basket;
            this.store = store;
            this.user = user;
            this.price = price;
            this.date = date;
            this.discount = discount;
            this.purchasePolicy = purchasePolicy;
        }

        public ReceiptData(int receiptID, BasketInRecipt basket, double price, MemberData user, StoreData store, DateTime date, iPolicyDiscountData discount, iPolicyData purchasePolicy)
        {
            this.receiptID = receiptID;
            this.basket = basket ?? throw new ArgumentNullException(nameof(basket));
            this.price = price;
            this.user = user ?? throw new ArgumentNullException(nameof(user));
            this.store = store ?? throw new ArgumentNullException(nameof(store));
            this.date = date;
            this.discount = discount ?? throw new ArgumentNullException(nameof(discount));
            this.purchasePolicy = purchasePolicy ?? throw new ArgumentNullException(nameof(purchasePolicy));
        }

        public ReceiptData()
        {
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
