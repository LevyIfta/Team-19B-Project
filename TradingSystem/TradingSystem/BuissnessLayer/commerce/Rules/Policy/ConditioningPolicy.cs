using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.Policy
{
    public class ConditioningPolicy : iPolicy
    {
        public Func<Product, bool> isRelevant;
        private bool Default;
        public ConditioningPolicy(Func<Product, bool> isRelevant)
        {
            this.isRelevant = isRelevant;
            this.Default = true;
            this.policies = new List<iPolicy>();
        }

        public override bool isValid(ICollection<Product> products, aUser user)
        {
            if (this.policies.Count == 0) return this.Default;

            foreach (Product item in products)
            {
                if (this.isRelevant(item))
                {
                    return this.policies[0].isValid(products, user);
                }
            }

            return this.Default;
        }
    }
}
