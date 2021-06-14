using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.DicountPolicy
{
    public class MaxPolicyDiscount : iPolicyDiscount
    {
        public new List<iPolicyDiscount> policies;
        public MaxPolicyDiscount()
        {
            this.policies = new List<iPolicyDiscount>();
        }

        public void addIPolicy(iPolicyDiscount policy)
        {
            this.policies.Add(policy);
        }
        public override double ApplyDiscount(ShoppingBasket basket, double totalPrice)
        {
            double maxDiscount = 0;

            foreach(iPolicyDiscount discountPolicy in this.policies)
            {
                maxDiscount = Math.Max(discountPolicy.ApplyDiscount(basket, totalPrice), maxDiscount);
            }

            return maxDiscount;
        }
    }
}
