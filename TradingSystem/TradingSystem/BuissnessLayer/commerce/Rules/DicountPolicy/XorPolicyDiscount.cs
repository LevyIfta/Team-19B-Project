using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.DicountPolicy
{
    class XorPolicyDiscount : ConditioningPolicyDiscount
    {

        public XorPolicyDiscount()
        {
            this.policies = new List<ConditioningPolicyDiscount>();
        }


        public override bool isValid(ICollection<Product> products, double totalPrice)
        {
            int trueConds = 0;

            foreach (ConditioningPolicyDiscount policy in this.policies)
                trueConds += policy.isValid(products, totalPrice) ? 1 : 0;

            // only one is valid
            return trueConds == 1;
        }
    }
}
