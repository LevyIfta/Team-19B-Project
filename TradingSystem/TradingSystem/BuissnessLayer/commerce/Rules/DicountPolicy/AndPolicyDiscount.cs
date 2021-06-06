using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.DicountPolicy
{
    class AndPolicyDiscount : iPolicyDiscount
    {
        public iPolicyDiscount policy1 { get; set; }
        public iPolicyDiscount policy2 { get; set; }

        public AndPolicyDiscount(iPolicyDiscount pol1, iPolicyDiscount pol2)
        {
            this.policy1 = pol1;
            this.policy2 = pol2;
        }

        public bool isValid(ICollection<Product> products)
        {
            return this.policy1.isValid(products) && this.policy2.isValid(products);
        }
    }
}
