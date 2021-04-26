using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer.commerce
{
    public class Product
    {
        public ProductInfo info { get; set; }
        public int amount { get; set; }

        public double price { get; set; }

        public Product(ProductInfo info, int amount, double price)
        {
            this.info = info;
            this.amount = amount;
            this.price = price;
        }

        public Product(ProductData productData)
        {
            ProductInfoData productInfoData = ProductInfoDAL.getProductInfo(productData.productID);
            this.info = ProductInfo.getProductInfo(productInfoData.productName, productInfoData.category, productInfoData.manufacturer);
            this.amount = productData.amount;
            this.price = productData.price;
        }

        public Product(Product product)
        {
            // clone the product
            this.info = product.info;
            this.amount = product.amount;
            this.price = product.price;
        }

        public ProductData toDataObject(string storeName)
        {
            return new ProductData(this.info.id, this.amount, this.price, storeName);
        }
        public override bool Equals(object obj)
        {
            return false;
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
            ProductDAL.update(this.toDataObject(storeName));
        }

        public void remove(string name)
        {
            ProductDAL.remove(this.toDataObject(name));
        }
    }
}
