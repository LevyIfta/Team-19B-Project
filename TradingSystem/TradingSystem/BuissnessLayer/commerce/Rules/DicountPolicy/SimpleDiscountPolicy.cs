using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.DicountPolicy
{
    class SimpleDiscountPolicy 
    {
        public ProductInfo subject { get; set; }

        public Func<Product, bool> isRelevant;
        
        public DateTime deadline { get; set; } //discount time

        public int discount_percent { get; set; }

        public Func<int, int, bool> predicate { get; set; }
        public bool Default { get; set; }

        public SimpleDiscountPolicy(ProductInfo subject, DateTime deadline, int discount_percent)
        {
            this.subject = subject;
            this.deadline = deadline;
            this.discount_percent = discount_percent;
            this.Default = true;
        }


        public SimpleDiscountPolicy(ProductInfo subject, int duration, int discount_percent, bool isRequired)
        {
            this.subject = subject;
            this.deadline = deadline;
            this.discount_percent = discount_percent;
            this.Default = !isRequired;
        }

        public void applyDiscount(ICollection<Product> products)
        {
            foreach (Product item in products)
            {
                if (item.info.Equals(subject))
                     item.price = item.price - item.price * this.discount_percent;
                   
            }
        }


    }
}
