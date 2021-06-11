using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.DicountPolicy
{
    class MaxPolicyDiscount : ConditioningPolicyDiscount
    {

        public MaxPolicyDiscount()
        {
            this.policies = new List<ConditioningPolicyDiscount>();
        }

        
        public override bool isValid(ICollection<Product> products, double totalPrice)
        {
            throw new NotImplementedException();
        }
    }
}
