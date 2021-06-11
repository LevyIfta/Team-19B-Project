using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.DicountPolicy
{
    public class baseDiscountPolicy : iPolicyDiscount
    {
      
        public DateTime deadline { get; set; } 

        public double discount_percent { get; set; }

        public Func<Product, bool> isRelevant;
        public Func<Product, double, bool> predicate { get; set; }
        public bool Default { get; set; }


        /*
        public baseDiscountPolicy(ProductInfo subject, DateTime deadline, int discount_percent , Func<Product, bool> isRelevant, Func<Product, double, bool> pred)
        {
            this.subject = subject;
            this.deadline = deadline;
            this.discount_percent = discount_percent;
            this.isRelevant = isRelevant;
            this.predicate = predicate;
            this.Default = true;

        }
        */

        public baseDiscountPolicy(Func<Product, bool> isRelevant, Func<Product, double, bool> pred , DateTime deadline ,double discount_percent)
        {
            this.isRelevant = isRelevant;
            this.predicate = pred;
            this.Default = true;
            this.deadline = deadline;
            this.discount_percent = discount_percent;
        }
        public baseDiscountPolicy(Func<Product, bool> isRelevant, Func<Product, double, bool> pred, bool isRequired)
        {
            this.isRelevant = isRelevant;
            this.predicate = pred;
            this.Default = !isRequired;
        }

        public override void ApplyDiscount(ICollection<Product> products)
        {
            foreach (Product item in products)
            {
                if (isRelevant(item))
                    if (check_discount_deadline(this.deadline))
                    {
                        item.price = calculateDiscountPrice(item, this.discount_percent);
                    }
            }
        }


        public override bool isValid(ICollection<Product> products, double totalprice)
        {
            foreach (Product item in products)
            {
                if (this.isRelevant(item))
                    return predicate(item, totalprice);
            }
            return this.Default;
        }
    }
}
