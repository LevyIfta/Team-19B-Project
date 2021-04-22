﻿using System;
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
        public ProductData toDataObject()
        {
            return new ProductData(info.toDataObject(), amount, price);
        }
    }
}
