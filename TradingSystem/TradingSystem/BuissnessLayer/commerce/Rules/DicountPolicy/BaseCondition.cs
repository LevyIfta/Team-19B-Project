using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.DicountPolicy
{
    class BaseCondition : ConditioningPolicyDiscount
    {
        private Func<ShoppingBasket, bool> predicate;

        public BaseCondition(Func<ShoppingBasket, bool> predicate)
        {
            this.predicate = predicate;
        }
        public override bool isValid(ShoppingBasket basket)
        {
            return predicate(basket);
        }
    }
}
