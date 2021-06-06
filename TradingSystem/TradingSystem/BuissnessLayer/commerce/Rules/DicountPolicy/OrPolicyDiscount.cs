using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.DicountPolicy
{
    class OrPolicyDiscount : ConditioningPolicyDiscount
    {
        public OrPolicyDiscount()
        {
            this.policies = new List<ConditioningPolicyDiscount>();
        }


        public override bool isValid(ICollection<Product> products, double totalprice)
        {
            foreach (ConditioningPolicyDiscount policy in this.policies)
                if (policy.isValid(products, totalprice))
                    return true;
            // non of the policies is valid
            return false;
        }
    }
}
