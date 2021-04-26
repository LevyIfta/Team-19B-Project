using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer.commerce
{
    public class ProductInfo
    {
        private static ICollection<ProductInfo> productsInfo = new LinkedList<ProductInfo>();
        public string name { get; set; }
        public string category { get; set; }
        public string manufacturer { get; set; }
        public int id;
        private static int currentId = -1;
        private static Object idLocker = new Object();
        public Dictionary<string, string> feedbacks { get; }

        public ProductInfo(string name, string category, string manufacturer)
        {
            // increment id
            lock (idLocker)
            {
                currentId++;
                this.id = currentId;
            }
            this.name = name;
            this.category = category;
            this.manufacturer = manufacturer;
            this.feedbacks = new Dictionary<string, string>();
        }

        public ProductInfo(ProductInfoData productInfoData)
        {
            this.name = productInfoData.productName;
            this.category = productInfoData.category;
            this.manufacturer = productInfoData.manufacturer;
            // get feedbacks - TODO

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
            ProductInfo productInfo = new ProductInfo(name, category, manufacturer);
            // checks a product with the given info already exists, if so returns it else creates a new one
            foreach (ProductInfo p in productsInfo)
                if (p.Equals(productsInfo))
                    return p;
            // the productInfo doesn't exist, add it and update DAL
            productsInfo.Add(productInfo);
            // update in DB
            ProductInfoDAL.addProductInfo(productInfo.toDataObject());
            return productInfo;
        }

        /// <summary>
        /// by shauli
        /// </summary>
        /// <param name="username"></param>
        /// <param name="comment"></param>
        public void LeaveFeedback (string username, string comment)
        {
            if (this.feedbacks.ContainsKey(username))
            {
                this.feedbacks[username] = comment;
            }
            else
            {
                this.feedbacks.Add(username, comment);
            }
        }

        /// <summary>
        /// by shauli
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string getUserFeeback(string username)
        {
            try
            {
                return this.feedbacks[username];
            }
            catch
            {
                return null;
            }
        }
    }
}
