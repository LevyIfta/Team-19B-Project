using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Key]
        public int productID { get; set; }
        public virtual ICollection<ProductData> Products { get; set; }

        //CONSTRUCTORS
        public ProductInfoData(string productName, string category, string manufacturer, int productID)
        { 
            this.productName = productName;
            this.category = category;
            this.manufacturer = manufacturer;
            this.productID = productID;
            this.Products = new List<ProductData>();
        }
        public ProductInfoData(string productName, string category, string manufacturer, int productID, List<ProductData> products)
        {
            this.productName = productName;
            this.category = category;
            this.manufacturer = manufacturer;
            this.productID = productID;
            this.Products = products;
        }

        public ProductInfoData()
        {
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
