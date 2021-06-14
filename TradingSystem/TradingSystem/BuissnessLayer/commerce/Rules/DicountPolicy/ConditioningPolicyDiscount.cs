using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.DicountPolicy
{
    public abstract class ConditioningPolicyDiscount
    {

        public List<ConditioningPolicyDiscount> policies = new List<ConditioningPolicyDiscount>();

        public abstract bool isValid(ShoppingBasket basket, double totalPrice);

        public void addPolicy(ConditioningPolicyDiscount policy)
        {
            this.policies.Add(policy);
        }

        public bool removePolicy()
        {
            if (this.policies.Count == 0) return false;
            this.policies.Remove(this.policies[this.policies.Count - 1]);
            return true;
        }
    }
}
