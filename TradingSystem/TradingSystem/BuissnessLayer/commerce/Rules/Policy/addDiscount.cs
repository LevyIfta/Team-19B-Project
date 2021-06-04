using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.Policy
{
    class addDiscount : iPolicy
    {

        public iPolicy policy1 { get; set; }
        public iPolicy policy2 { get; set; }

        public addDiscount(iPolicy pol1, iPolicy pol2)
        {
            this.policy1 = pol1;
            this.policy2 = pol2;
        }

        public bool isValid(ICollection<Product> products, aUser user)
        {

            return true;
        }
    }
}
