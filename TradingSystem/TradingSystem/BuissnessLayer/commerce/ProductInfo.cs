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
        public string name { get; set; }
        public string category { get; set; }
        public string manufacturer { get; set; }
        public Dictionary<string, string> feedbacks { get; }

        public ProductInfo(string name, string category, string manufacturer)
        {
            this.name = name;
            this.category = category;
            this.manufacturer = manufacturer;
            this.feedbacks = new Dictionary<string, string>();
        }

        public ProductInfo(ProductInfoData productInfoData)
        {
            this.name = productInfoData.name;
            this.category = productInfoData.category;
            this.manufacturer = productInfoData.manufacturer;
            this.feedbacks = productInfoData.feedbacks;
        }

        public ProductInfoData toDataObject()
        {
            return new ProductInfoData(this.name, this.category, this.manufacturer, this.feedbacks);
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
