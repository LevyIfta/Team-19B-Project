using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules
{
    class BasePolicy : iPolicy
    {

        public ProductInfo subject { get; set; }
        //public int amount { get; set; }
        public Func<Product, aUser, bool> predicate { get; set; }
        public bool Default { get; set; }
        
        public BasePolicy(ProductInfo subject, Func<Product, aUser, bool> pred)
        {
            this.subject = subject;
            //this.amount = amount;
            this.predicate = pred;
            this.Default = true;
        }

        public BasePolicy(ProductInfo subject, Func<Product, aUser, bool> pred, bool isRequired)
        {
            this.subject = subject;
            //this.amount = amount;
            this.predicate = pred;
            this.Default = !isRequired;
        }

        /*
        public static bool LargerThan(int a, int b )
        {
            return a > b;
        }
        public static bool LargerEqual(int a, int b)
        {
            return a >= b;
        }
        public static bool SmallerThan(int a, int b)
        {
            return a < b;
        }

        public static bool SmallerEqual(int a, int b)
        {
            return a <= b;
        }
        */

        public bool isValid(ICollection<Product> products, aUser user)
        {
            foreach (Product item in products)
            {
                if (item.info.Equals(subject))
                    //return predicate(item.amount, this.amount);
                    return predicate(item, user);
            }
            return this.Default;
        }
    }
}
