using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.DicountPolicy
{
    class AndPolicyDiscount : ConditioningPolicyDiscount
    {
        public AndPolicyDiscount()
        {
            this.policies = new List<ConditioningPolicyDiscount>();
        }
        public override bool isValid(ICollection<Product> products, double totalPrice)
        {
            foreach (ConditioningPolicyDiscount policy in this.policies)
                if (!policy.isValid(products, totalPrice))
                    return false;
            return true;
        }
    }
}
