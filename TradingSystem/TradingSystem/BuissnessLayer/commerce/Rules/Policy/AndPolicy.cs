using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules
{
    class AndPolicy : iPolicy
    {
        public AndPolicy()
        {
            this.policies = new List<iPolicy>();
        }

        public override bool isValid(ICollection<Product> products, aUser user)
        {
            foreach (iPolicy policy in this.policies)
                if (!policy.isValid(products, user))
                    return false;
            // all the policies are valid
            return true;
        }
    }
}
