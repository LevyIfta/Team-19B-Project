using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.Policy
{
    class maxPolicy : iPolicy
    {

        public iPolicy policy1 { get; set; }
        public iPolicy policy2 { get; set; }

        public maxPolicy(iPolicy pol1, iPolicy pol2)
        {
            this.policy1 = pol1;
            this.policy2 = pol2;
        }

        /*public bool isValid(ICollection<Product> products, aUser user)
        {

            // var a =  Math.Max(this.policy1.isValid(products, user), this.policy2.isValid(products, user))
            return false;
        }*/

        public override bool isValid(ICollection<Product> products, aUser user)
        {
            throw new NotImplementedException();
        }
    }
}
