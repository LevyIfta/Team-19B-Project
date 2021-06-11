using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules
{
    class OrPolicy : iPolicy
    {
        public OrPolicy()
        {
            this.policies = new List<iPolicy>();
        }

        public override bool isValid(ICollection<Product> products, aUser user)
        {
            foreach (iPolicy policy in this.policies)
                if (policy.isValid(products, user))
                    return true;
            // non of the policies is valid
            return false;
        }
    }
}
