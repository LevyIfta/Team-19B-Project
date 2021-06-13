using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.commerce;
using TradingSystem.BuissnessLayer.commerce.Rules;
using TradingSystem.DataLayer;
using TradingSystem.DataLayer.ORM;

namespace TradingSystem.BuissnessLayer.commerce
{
    public class Receipt
    {
        public BasketInRecipt basket{ get; set; }
        public double price { get; set; }
        public Member user { get; set; }
        public DateTime date { get; set; }
        public int receiptId;
        private static int currentId = -1;
        private static Object idLocker = new Object();
        public Store store;
        public iPolicyDiscountData discount { get; set; } //todo
        public iPolicy purchasePolicy { get; set; }

        public ReceiptData toDataObject()
        {
            return new ReceiptData(this.receiptId, basket, this.store.toDataObject(), this.user.toDataObject(), this.price, this.date, this.discount, new iPolicyData());
        }

        public Receipt(ReceiptData receiptData)
        {
            this.receiptId = receiptData.receiptID;
            this.store = Stores.searchStore(receiptData.store.storeName);
            this.basket.products = new List<ProductData>();
            foreach (ProductData product in receiptData.basket.products)
                this.basket.products.Add(product);
            this.discount = receiptData.discount;
            this.purchasePolicy = null;
            //this.actualProducts = new LinkedList<Product>();
            //foreach (ProductsInReceiptData pInR in ProductsInReceiptDAL.getProducts(this.receiptId))
                //this.actualProducts.Add(new Product(ProductInfo.getProductInfo(pInR.productID), pInR.amount, 0));
            // get products and fill in this.products
            this.price = receiptData.price;
            this.date = receiptData.date;
        }

        public Receipt()
        {
            //this.actualProducts = new LinkedList<Product>();
            lock (idLocker)
            {
                currentId++;
                this.receiptId = currentId;
            }
            //this.products = new Dictionary<int, int>();
        }

        public void save()
        {
            DataLayer.ORM.DataAccess.create(toDataObject());
            // update self
            //ReceiptDAL.addReceipt(this.toDataObject());
            // update products
            //foreach (int id in this.products.Keys)
                //ProductsInReceiptDAL.addProductsInBasket(new ProductsInReceiptData(this.receiptId, id, this.products[id]));
        }
        public ICollection<Product> getProducts() {
            List<Product> products = new List<Product>();
            foreach (ProductData product in this.basket.products)
                products.Add(new Product(product));
            return products;
        }
    }
}
