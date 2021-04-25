using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer
{
    class Product
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
            this.info = new ProductInfo(productData.info);
            this.amount = productData.amount;
            this.price = productData.price;
        }
        public ProductData toDataObject()
        {
            return new ProductData(info.toDataObject(), amount, price);
        }
        public override bool Equals(object obj)
        {
            return false;
        }
        public bool Equals(Product product)
        {
            return product.info.name.Equals(info.name);
        }
        public void addAmount(int add)
        {
            this.amount += add;
        }
    }
}
