using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;

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

        public ConditioningPolicy(ConditioningPolicyData conditioningPolicyData)
        {
            this.isRelevant = conditioningPolicyData.isRelevant;
            this.Default = conditioningPolicyData.Default;
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

        public override iPolicyData toDataObject()
        {
            List<Guid> policiesData = new List<Guid>();

            foreach (iPolicy policy in this.policies)
                policiesData.Add(policy.id);

            return new ConditioningPolicyData(policiesData, this.Default);
        }
    }
}
