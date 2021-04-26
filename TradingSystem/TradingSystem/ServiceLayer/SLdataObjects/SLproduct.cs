using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.ServiceLayer
{
    class SLproduct
    {
        public int amount;
        public double price;
        public string productName;
        public string category;
        public string manufacturer;
        public int productID;
        public Dictionary<string, string> feedbacks;

        private const int PARAMETER_COUNT = 7;

        public SLproduct(int amount, double price, string productName, string category, string manufacturer, int productID, Dictionary<string, string> feedbacks)
        {
            this.amount = amount;
            this.price = price;
            this.productName = productName;
            this.category = category;
            this.manufacturer = manufacturer;
            this.productID = productID;
            this.feedbacks = feedbacks;
        }

        public SLproduct(Object[] parameters)
        {
            this.amount = (int)parameters[0];
            this.price = (double)parameters[1];
            this.productName = (string)parameters[2];
            this.category = (string)parameters[3];
            this.manufacturer = (string)parameters[4];
            this.productID = (int)parameters[5];
            this.feedbacks = (Dictionary<string, string>)parameters[6];
        }

    }
}
