using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer
{
    class ProductInfo
    {
        private static ICollection<ProductInfo> productsInfo = new LinkedList<ProductInfo>();
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

        override
        public Boolean Equals(object other)
        {
            return (other is ProductInfo && (this.name.Equals(((ProductInfo)other).name) & this.category.Equals(((ProductInfo)other).category) & this.manufacturer.Equals(((ProductInfo)other).manufacturer)));
        }

        public static ProductInfo getProductInfo(string name, string category, string manufacturer)
        {
            // sync productsInfo
            productsInfo = (ICollection<ProductInfo>)ProductsInfoData.productsInfo.Select(p => new ProductInfo(p));
            ProductInfo productInfo = new ProductInfo(name, category, manufacturer);
            // checks a product with the given info already exists, if so returns it else creates a new one
            foreach (ProductInfo p in productsInfo)
                if (p.Equals(productsInfo))
                    return p;
            // the productInfo doesn't exist, add it and update DAL
            productsInfo.Add(productInfo);
            ProductsInfoData.productsInfo.Add(productInfo.toDataObject());
            return productInfo;
        }

    }
}
