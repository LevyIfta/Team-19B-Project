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

        private ProductInfo(string name, string category, string manufacturer)
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
            return new ProductInfoData(this.name, this.category, this.manufacturer, this.id);
        }

        override
        public bool Equals(object other)
        {
            return (other is ProductInfo && (this.name.Equals(((ProductInfo)other).name) & this.category.Equals(((ProductInfo)other).category) & this.manufacturer.Equals(((ProductInfo)other).manufacturer)));
        }

        public static ProductInfo getProductInfo(string name, string category, string manufacturer)
        {
            // checks a product with the given info already exists, if so returns it else creates a new one
            foreach (ProductInfo p in productsInfo)
                if (p.name.Equals(name) & p.category.Equals(category) & p.manufacturer.Equals(manufacturer))
                    return p;
            // the productInfo doesn't exist, add it and update DAL
            ProductInfo productInfo = new ProductInfo(name, category, manufacturer);
            productsInfo.Add(productInfo);
            // update in DB
            ProductInfoDAL.addProductInfo(productInfo.toDataObject());
            return productInfo;
        }
        public static ProductInfo getProductInfo(int id)
        {
            foreach (ProductInfo pInfo in productsInfo)
                if (pInfo.id == id)
                    return pInfo;
            return null;
        }

        public bool roomForFeedback (string username)
        {
            if (this.feedbacks.ContainsKey(username))
            {
                return false;
            }
            else
            {
                this.feedbacks.Add(username, "");
                return true;
            }
        }

        public bool leaveFeedback(string username, string comment)
        {
            feedbacks[username] += comment;
            return true;
            /*
            if (this.feedbacks.ContainsKey(username))
            {
                if (feedbacks[username].CompareTo("") == 0)
                {
                    this.feedbacks[username] = comment;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }*/
        }

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

        public Dictionary<string, string> getAllFeedbacks()
        {
            Dictionary<string, string> allFeedbacks = new Dictionary<string, string>();
            foreach (string userName in this.feedbacks.Keys)
            {
                if(this.feedbacks[userName].CompareTo("") != 0)
                {
                    allFeedbacks.Add(userName, this.feedbacks[userName]);
                }
            }
            return allFeedbacks;
        }
        /*
        public static string getProductName(int productId)
        {
            foreach (ProductInfo productInfo in productsInfo)
                if (productInfo.id == productId)
                    return productInfo.name;
            return "";
        }*/
        public override string ToString()
        {
            string output = "";

            output += "ID: " + this.id + "\n";
            output += "Product name: " + this.name + "\n";
            output += "Category: " + this.category + "\n";
            output += "Manufacturer: " + this.manufacturer + "\n";

            return output;
        }
    }
    

}
