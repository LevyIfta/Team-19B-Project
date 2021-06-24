using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;
using TradingSystem.DataLayer.ORM;

namespace TradingSystem.BuissnessLayer.commerce
{
    public class Product
    {
        public ProductInfo info { get; set; }
        public int amount { get; set; }
        public double price { get; set; }

        public double discount_percent { get; set; }
        public Guid id { get; set; } = Guid.NewGuid();

        public Product(ProductInfo info, int amount, double price)
        {
            this.info = info;
            this.amount = amount;
            this.price = price;
            this.discount_percent = 0;
        }

        public Product(ProductData productData)
        {
            ProductInfoData productInfoData = productData.productData;
            this.info = ProductInfo.getProductInfo(productInfoData.productName, productInfoData.category, productInfoData.manufacturer);
            this.amount = productData.amount;
            this.price = productData.price;
            this.discount_percent = 0;
        }

        public Product(Product product)
        {
            // clone the product
            this.info = product.info;
            this.amount = product.amount;
            this.price = product.price;
            this.discount_percent = 0;
        }
        /*
        public Product(BasketInCart productsInBasketData)
        {
            ProductInfoData productInfoData = ProductInfoDAL.getProductInfo(productsInBasketData.productID);
            this.info = ProductInfo.getProductInfo(productInfoData.productName, productInfoData.category, productInfoData.manufacturer);
            this.amount = productsInBasketData.amount;
            this.price = 0;

        }*/

        public ProductData toDataObject(string storeName)
        {
            return new ProductData(this.info.toDataObject(), this.amount, this.price, storeName, this.id);
        }
        public override bool Equals(object obj)
        {
            return ((Product)obj).info.Equals(this.info);
        }
        public bool Equals(Product product)
        {
            return product.info.Equals(info); // maybe add amounts ?
        }
        public void addAmount(int add)
        {
            this.amount += add;
        }

        public void update(string storeName)
        {
            DataLayer.ORM.DataAccess.update(this.toDataObject(storeName));
        }

        public void remove(string name)
        {
            DataLayer.ORM.DataAccess.Delete(this.toDataObject(name));
        }
    }
}
