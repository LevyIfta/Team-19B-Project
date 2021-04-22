using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    class ProductData
    {
        public ProductInfoData info { get; set; }
        public int amount { get; set; }
        public double price { get; set; }

        public ProductData(ProductInfoData info, int amount, double price)
        {
            this.info = info;
            this.amount = amount;
            this.price = price;
        }
    }
}
