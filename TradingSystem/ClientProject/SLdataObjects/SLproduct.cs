using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientProject
{
    public class SLproduct
    {
        public int amount { get; }
        public double price { get; }
        public string productName { get; }
        public string category { get; }
        public string manufacturer { get; }
        public Dictionary<string, string> feedbacks { get; }

        public const int PARAMETER_COUNT = 7;

        public SLproduct(int amount, double price, string productName, string category, string manufacturer, Dictionary<string, string> feedbacks)
        {
            this.amount = amount;
            this.price = price;
            this.productName = productName;
            this.category = category;
            this.manufacturer = manufacturer;
            this.feedbacks = feedbacks;
        }

        public SLproduct(Object[] parameters)
        {
            this.amount = (int)parameters[0];
            this.price = (double)parameters[1];
            this.productName = (string)parameters[2];
            this.category = (string)parameters[3];
            this.manufacturer = (string)parameters[4];
            this.feedbacks = (Dictionary<string, string>)parameters[5];
        }

    }
}
