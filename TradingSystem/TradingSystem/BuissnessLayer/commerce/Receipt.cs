using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.commerce;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer.commerce
{
    public class Receipt
    {
        public Dictionary<int, int> products { get; set; } // <product_id, amount>
        public Store store { get; set; }
        public string username { get; set; }
        public double price { get; set; }
        public DateTime date { get; set; }
        public int receiptId;
        private static int currentId = -1;
        private static Object idLocker = new Object();
        public int discount { get; set; } //todo
        //public PurchasePolicy purchasePolicy { get; set; }

        public ReceiptData toDataObject()
        {
            return new ReceiptData(this.receiptId, this.store.storeName, this.username, this.price, this.date, this.discount, -1);
        }

        public Receipt(ReceiptData receiptData)
        {
            this.receiptId = receiptData.receiptID;
            this.store = Stores.searchStore(receiptData.storeName);
            this.products = new Dictionary<int, int>();
            foreach (ProductsInReceiptData pInR in ProductsInReceiptDAL.getProducts(this.receiptId))
                this.products.Add(pInR.productID, pInR.amount);
            // get products and fill in this.products
            this.price = receiptData.price;
            this.date = receiptData.date;
        }

        public Receipt()
        {
            lock (idLocker)
            {
                currentId++;
                this.receiptId = currentId;
            }
            this.products = new Dictionary<int, int>();
        }

        public void save()
        {
            // update self
            ReceiptDAL.addReceipt(this.toDataObject());
            // update products
            foreach (int id in this.products.Keys)
                ProductsInReceiptDAL.addProductsInBasket(new ProductsInReceiptData(this.receiptId, id, this.products[id]));
        }
    }
}
