using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    public class ProductInfoData
    {
        //FIELDS
        public string productName { get; set; }
        public string category { get; set; }
        public string manufacturer { get; set; }
        public int productID { get; set; }

        //CONSTRUCTORS
        public ProductInfoData(string productName, string category, string manufacturer, int productID)
        {
            this.productName = productName;
            this.category = category;
            this.manufacturer = manufacturer;
            this.productID = productID;
        }

        //EQUALS
        public override bool Equals(object obj)
        {
            return false;
        }

        public bool Equals(ProductInfoData other)
        {
            return this.productName.Equals(other.productName) & this.category.Equals(other.category)
                & this.manufacturer.Equals(other.manufacturer) & this.productID.Equals(other.productID);
        }
    }
}
