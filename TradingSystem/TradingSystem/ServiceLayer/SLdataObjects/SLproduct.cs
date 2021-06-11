using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.ServiceLayer
{
 
    public class SLproduct
    {
        private static readonly int BEL = 7;
        private static readonly int ETX = 3;

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

        public List<string> toStringarr()
        {
            List<string> ans = new List<string>();
            ans.Add(amount + "");
            
            ans.Add(price + "");
            
            ans.Add(productName);
            
            ans.Add(category);
            
            ans.Add(manufacturer);
            
            foreach (KeyValuePair<string, string> item in feedbacks)
            {
                ans.Add(item.Key);
                ans.Add(item.Value);
            }
            ans.Add((char)ETX + "");

            return ans;

        }

        public List<string> toStringarr( string storeName)
        {
            List<string> ans = new List<string>();
            ans.Add(storeName);
            ans.Add(amount + "");

            ans.Add(price + "");

            ans.Add(productName);

            ans.Add(category);

            ans.Add(manufacturer);

            foreach (KeyValuePair<string, string> item in feedbacks)
            {
                ans.Add(item.Key);
                ans.Add(item.Value);
            }
            ans.Add((char)ETX + "");

            return ans;

        }

        public static List<SLproduct> makeObjects(string[] str)
        {
            List<SLproduct> ans = new List<SLproduct>();
            int index = 0;
            while (index < str.Length)
            {
                int amount = int.Parse(str[index]);         index++;
                double price = double.Parse(str[index]);    index++;
                string productname = str[index];            index++;
                string category = str[index];               index++;
                string manufacturer = str[index];           index++;
                Dictionary<string, string> feedback = new Dictionary<string, string>();
                bool runner = true;
                while(runner)
                {
                    if(str[index][0] == ETX)
                    {
                        index++;
                        runner = false;
                    }
                    else
                    {
                        feedback.Add(str[index], str[index + 1]);
                        index += 2;
                    }
                }

                ans.Add(new SLproduct(amount, price, productname, category, manufacturer, feedback));
            }
            return ans;

        }


        public static Dictionary<string, SLproduct> makeObjectsWithStore(string[] str)
        {
            Dictionary<string, SLproduct> ans = new Dictionary<string, SLproduct>();
            int index = 0;
            while (index < str.Length)
            {
                string storeName = str[index];      index++;
                int amount = int.Parse(str[index]); index++;
                double price = double.Parse(str[index]); index++;
                string productname = str[index]; index++;
                string category = str[index]; index++;
                string manufacturer = str[index]; index++;
                Dictionary<string, string> feedback = new Dictionary<string, string>();
                bool runner = true;
                while (runner)
                {
                    if (str[index][0] == ETX)
                    {
                        index++;
                        runner = false;
                    }
                    else
                    {
                        feedback.Add(str[index], str[index + 1]);
                        index += 2;
                    }
                }

                ans.Add(storeName, new SLproduct(amount, price, productname, category, manufacturer, feedback));
            }
            return ans;

        }

    }
}
