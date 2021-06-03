using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules
{
    class XorPolicy : iPolicy
    {
        public iPolicy policy1 { get; set; }
        public iPolicy policy2 { get; set; }

        public XorPolicy(iPolicy pol1, iPolicy pol2)
        {
            this.policy1 = pol1;
            this.policy2 = pol2;
        }

        public bool isValid(ICollection<Product> products, aUser user)
        {
            return this.policy1.isValid(products, user) ^ this.policy2.isValid(products, user);
        }
    }
}
