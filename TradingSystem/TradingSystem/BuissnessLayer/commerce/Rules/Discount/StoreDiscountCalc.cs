using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.Discount
{
    class StoreDiscountCalc : IDiscount
    {
        IDiscount DiscountCalc;
        public StoreDiscountCalc(double percent)
        {
            Func<ShoppingBasket, double> f = new Func<ShoppingBasket, double>((ShoppingBasket basket) => Calc(basket, percent));
            DiscountCalc = new DiscountCalc(f);
        }

        private double Calc(ShoppingBasket basket, double percent)
        {
            double discount = 0;
         /*   foreach (var p_q in basket.GetDictionaryProductQuantity())
            {
                var product = p_q.Key;
                var quantity = p_q.Value;
                discount += quantity * product.Price * percent;
            }*/ //todo
            return discount;
        }

        public IDiscount Add(IDiscount otherDiscountCalc)
        {
            return DiscountCalc.Add(otherDiscountCalc);
        }

        public double CalcDiscount(ShoppingBasket shoppingBasket)
        {
            return DiscountCalc.CalcDiscount(shoppingBasket);
        }

        public Func<ShoppingBasket, double> GetFunction()
        {
            return DiscountCalc.GetFunction();
        }

        public IDiscount Max(IDiscount otherDiscountCalc)
        {
            return DiscountCalc.Max(otherDiscountCalc);
        }
    }
}
