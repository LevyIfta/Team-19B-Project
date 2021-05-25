using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.Discount
{
    class ConditionDiscount : Discount
    {
        private IRule _rule;
        public ConditionDiscount(IDiscount calc) : base(calc)
        {

        }
        public override IRule GetRule()
        {
            return _rule;
        }
        public void AddRule(IRule rule)
        {
            _rule = rule;
        }

        public void RemoveRule(IRule rule)
        {
            _rule = null;
        }
        public override double ApplyDiscounts(ShoppingBasket shoppingBasket)
        {
            if (Available(shoppingBasket))
            {
                return base.Calc.CalcDiscount(shoppingBasket);
            }
            return 0;
        }

        public bool Available(ShoppingBasket shoppingBasket)
        {
            return _rule == null || _rule.Check(shoppingBasket);
        }
        public double XorHelper(ShoppingBasket shoppingBasket, ConditionDiscount d1, ConditionDiscount d2, bool decide)
        {
            bool available1 = d1.Available(shoppingBasket);
            bool available2 = d2.Available(shoppingBasket);
            //if both discounts are available decide by the 'decide' value
            if (available1 && available2)
            {
                if (decide)
                {
                    return d1.Calc.CalcDiscount(shoppingBasket);
                }
                else
                {
                    return d2.Calc.CalcDiscount(shoppingBasket);
                }
            }
            else if (available1)
            {
                return d1.Calc.CalcDiscount(shoppingBasket);
            }
            else
            {
                return d2.ApplyDiscounts(shoppingBasket);
            }
        }

        public ConditionDiscount Xor(ConditionDiscount d, bool decide)
        {
            Func<ShoppingBasket, double> f = new Func<ShoppingBasket, double>
                ((ShoppingBasket shoppingBasket) => XorHelper(shoppingBasket, this, d, decide)
                );

            return new ConditionDiscount(new DiscountCalc(f));
        }

        public ConditionDiscount And(IRule additionalRule)
        {
            ConditionDiscount discount = new ConditionDiscount(new DiscountCalc(base.Calc));
            IRule andRule = Rule.AddTwoRules(_rule, additionalRule);
            discount.AddRule(andRule);
            return discount;
        }

        public ConditionDiscount Or(IRule additionalRule)
        {
            ConditionDiscount discount = new ConditionDiscount(new DiscountCalc(base.Calc));
            IRule orRule = Rule.OrTwoRules(_rule, additionalRule);
            discount.AddRule(orRule);
            return discount;

        }
    }
}
