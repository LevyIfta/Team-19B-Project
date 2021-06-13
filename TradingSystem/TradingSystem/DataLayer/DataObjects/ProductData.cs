using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    public class ProductData
    {
        //FIELDS
        [ForeignKey("ProductInfoData")]
        public virtual ProductInfoData productData { get; set; }
        public int amount { get; set; }
        public double price { get; set; }
        public string storeName { get; set; }
        [Key]
        public Guid id { get; set; }

        //CONSTRUCTORS
        public ProductData (ProductInfoData productData, int amount, double price, string storeName, Guid id)
        {
            this.productData = productData;
            this.amount = amount;
            this.price = price;
            this.storeName = storeName;
            this.id = id;
        }

        //EQUALS
    

        public bool Equals(ProductData obj)
        {
            return this.id.Equals(obj.id);
        }

        public override bool Equals(object obj)
        {
            return false;
        }
    }
}
