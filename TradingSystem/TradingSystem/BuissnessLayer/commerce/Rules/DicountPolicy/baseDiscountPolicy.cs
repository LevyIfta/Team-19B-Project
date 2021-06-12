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
        public Func<Product, bool> predicate { get; set; }
        public bool Default { get; set; }

        public baseDiscountPolicy(Func<Product, bool> isRelevant, Func<Product, bool> pred , DateTime deadline ,double discount_percent)
        {
            this.isRelevant = isRelevant;
            //this.predicate = pred;
            this.Default = true;
            this.deadline = deadline;
            this.discount_percent = discount_percent;
        }
        public baseDiscountPolicy(Func<Product, bool> isRelevant, Func<Product, bool> pred, bool isRequired)
        {
            this.isRelevant = isRelevant;
            //this.predicate = pred;
            this.Default = !isRequired;
        }

        public override double ApplyDiscount(ShoppingBasket basket, double totalPrice)
        {
            // check for conditions (if there are any)
            foreach (ConditioningPolicyDiscount condition in this.policies)
                if (!condition.isValid(basket, totalPrice))
                    return 0;

            double totalDiscount = 0;
            foreach (Product item in basket.products)
            {
                if (isRelevant(item))
                    if (check_discount_deadline(this.deadline))
                    {
                        // check if the discount doesn't exceed 100% - prevent overdiscount
                        if (item.discount_percent + this.discount_percent <= 1)
                        {
                            item.discount_percent += this.discount_percent;
                            totalDiscount += this.discount_percent * item.price;
                        }
                    }
            }

            return totalDiscount;
        }

        /*
        public override bool isValid(ICollection<Product> products, double totalprice)
        {
            foreach (Product item in products)
            {
                if (this.isRelevant(item))
                    return predicate(item, totalprice);
            }
            return this.Default;
        }
        */
    }
}
