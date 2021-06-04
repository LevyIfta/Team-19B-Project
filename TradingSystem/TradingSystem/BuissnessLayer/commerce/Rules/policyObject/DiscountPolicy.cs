using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.policyObject
{
    class DiscountPolicy
    {
        public iPolicy policy1 { get; set; }
        public iPolicy policy2 { get; set; }

        public DiscountPolicy(iPolicy pol1, iPolicy pol2)
        {
            this.policy1 = pol1;
            this.policy2 = pol2;
        }

        /*
        public iPolicy maxPolicy(iPolicy pol1, iPolicy pol2)
        {
            return Math.Max(pol1, pol2);
        }
        */

    }
}
