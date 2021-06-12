using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.DicountPolicy
{
    class BaseCondition : ConditioningPolicyDiscount
    {
        private Func<ShoppingBasket, double, bool> predicate;

        public BaseCondition(Func<ShoppingBasket, double, bool> predicate)
        {
            this.predicate = predicate;
        }
        public override bool isValid(ShoppingBasket basket, double totalPrice)
        {
            return predicate(basket, totalPrice);
        }
    }
}
