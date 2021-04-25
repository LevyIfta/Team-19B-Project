using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer
{
    class Receipt
    {
        public ShoppingBasket basket { get; set; }
        public Store store { get { return basket.store; } private set {  } }
        public string username { get { return basket.owner.userName; } private set { } }
        public double price { get; set; }
        public DateTime date { get; set; }
        public int id;
        private static int currentId = -1;
        private static Object idLocker = new Object();
        //public Object Discount { get; set; } //todo
        //public PurchasePolicy purchasePolicy { get; set; }

        public ReceiptData toDataObject()
        {
            //return new ReceiptData(this.basket.toDataObject(), this.price, this.date);
            return null;
        }

        public Receipt(ReceiptData receiptData)
        {

            this.basket = new ShoppingBasket(receiptData.basket);
            this.price = receiptData.price;
            this.date = receiptData.date;
        }

        public Receipt()
        {
            lock (idLocker)
            {
                currentId++;
                this.id = currentId;
            }
        }
    }
}
