using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.DicountPolicy
{
    class MaxPolicyDiscount : iPolicyDiscount
    {
        public bool isValid(ICollection<Product> products)
        {
            return true;
        }
    }
}
