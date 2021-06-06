using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.DicountPolicy
{
    class BasePolicyDiscount : iPolicyDiscount
    {
        public ProductInfo subject { get; set; }
        public int amount { get; set; }
        public Func<int, int, bool> predicate { get; set; }
        public bool Default { get; set; }

        public BasePolicyDiscount(ProductInfo subject, int amount, Func<int, int, bool> pred)
        {
            this.subject = subject;
            this.amount = amount;
            this.predicate = pred;
            this.Default = true;
        }

        public BasePolicyDiscount(ProductInfo subject, int amount, Func<int, int, bool> pred, bool isRequired)
        {
            this.subject = subject;
            this.amount = amount;
            this.predicate = pred;
            this.Default = !isRequired;
        }

        public static bool LargerThan(int a, int b)
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


        public bool isValid(ICollection<Product> products)
        {
            foreach (Product item in products)
            {
                if (item.info.Equals(subject))
                    return predicate(item.amount, this.amount);
            }
            return this.Default;
        }
    }
}
