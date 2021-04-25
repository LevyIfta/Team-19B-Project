using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer
{
    public class Product
    {
        public ProductInfo info { get; set; }
        public int amount { get; set; }
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
        public ProductData toDataObject()
        {
            return new ProductData(info.toDataObject(), amount, price);
        }
    }
}
