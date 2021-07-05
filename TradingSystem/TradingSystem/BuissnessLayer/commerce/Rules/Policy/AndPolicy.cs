using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;
using TradingSystem.DataLayer.ORM;

namespace TradingSystem.BuissnessLayer.commerce.Rules
{
    class AndPolicy : iPolicy
    {
        public AndPolicy()
        {
            this.policies = new List<iPolicy>();
        }

        public AndPolicy(AndPolicyData andPolicyData)
        {
            this.policies = new List<iPolicy>();

            foreach (Guid policyData in andPolicyData.policiesData)
                this.policies.Add(DataAccess.getPolicyByID(policyData).toObject());
        }

        public override bool isValid(ICollection<Product> products, aUser user)
        {
            foreach (iPolicy policy in this.policies)
                if (!policy.isValid(products, user))
                    return false;
            // all the policies are valid
            return true;
        }

        public override iPolicyData toDataObject()
        {
            List<Guid> policiesData = new List<Guid>();

            foreach (iPolicy policy in this.policies)
                policiesData.Add(policy.id);

            return new AndPolicyData(policiesData);
        }
    }
}
