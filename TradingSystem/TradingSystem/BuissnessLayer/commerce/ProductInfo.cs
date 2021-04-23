﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer
{
    class ProductInfo : IComparable
    {
        public string name { get; set; }
        public string category { get; set; }
        public string manufacturer { get; set; }

        public ProductInfo(string name, string category, string manufacturer)
        {
            this.name = name;
            this.category = category;
            this.manufacturer = manufacturer;
        }

        public ProductInfo(ProductInfoData productInfoData)
        {
            this.name = productInfoData.name;
            this.category = productInfoData.category;
            this.manufacturer = productInfoData.manufacturer;
        }

        public ProductInfoData toDataObject()
        {
            return new ProductInfoData(this.name, this.category, this.manufacturer);
        }

        public int CompareTo(object other)
        {
            return (other is ProductInfo && (this.name.Equals(((ProductInfo)other).name) & this.category.Equals(((ProductInfo)other).category) & this.manufacturer.Equals(((ProductInfo)other).manufacturer))) ? 0 : 1;
        }
    }
}
