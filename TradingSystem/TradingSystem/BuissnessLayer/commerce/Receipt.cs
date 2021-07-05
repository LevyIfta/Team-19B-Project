using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.commerce;
using TradingSystem.BuissnessLayer.commerce.Rules;
using TradingSystem.BuissnessLayer.commerce.Rules.DicountPolicy;
using TradingSystem.DataLayer;
using TradingSystem.DataLayer.ORM;

namespace TradingSystem.BuissnessLayer.commerce
{
    public class Receipt
    {
        public ShoppingBasket basket{ get; set; }
        public double price { get; set; }
        public aUser user { get { return basket.owner; }  }
        public DateTime date { get; set; }
        public int receiptId;
        private static int currentId = -1;
        private static Object idLocker = new Object();
        public Store store { get { return basket.store; } }
        public iPolicyDiscount discount { get; set; } //todo
        public iPolicy purchasePolicy { get; set; }

        public ReceiptData toDataObject()
        {
            BasketInRecipt bask = basket.toDataObjectRecipt(this.receiptId);

            ReceiptData ans = DataAccess.getReciept(this.receiptId); //= new ReceiptData(this.receiptId, bask, this.store.toDataObject(), this.user.toDataObject(), this.price, this.date, new iPolicyDiscountData(), new iPolicyData());//todo
            if (ans == null)
            {
                bask.recipt = ans;

                return ans;
            }
            ans.price = this.price;
            ans.date = this.date;
            ans.purchasePolicy = this.purchasePolicy.toDataObject();
            ans.discount = this.discount.toDataObject();
            return ans;
            
                
        }

        public Receipt(ReceiptData receiptData)
        {
            this.receiptId = receiptData.receiptID;
            
            this.basket = new ShoppingBasket(receiptData.basket);
            this.discount = null;//new iPolicyDiscount(receiptData.discount);  //todo
            this.purchasePolicy = null;
            //this.actualProducts = new LinkedList<Product>();
            //foreach (ProductsInReceiptData pInR in ProductsInReceiptDAL.getProducts(this.receiptId))
                //this.actualProducts.Add(new Product(ProductInfo.getProductInfo(pInR.productID), pInR.amount, 0));
            // get products and fill in this.products
            this.price = receiptData.price;
            this.date = receiptData.date;
            this.receiptId = receiptData.receiptID;
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
            return this.basket.products;
        }
    }
}
