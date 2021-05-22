using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.Discount
{
    class ProductDiscountCalc : IDiscount
    {
        IDiscount DiscountCalc;
        public ProductDiscountCalc(Guid productId, double percent)
        {
            Func<ShoppingBasket, double> f = new Func<ShoppingBasket, double>((ShoppingBasket basket) => Calc(basket, productId, percent));
            DiscountCalc = new DiscountCalc(f);
        }

        private double Calc(ShoppingBasket basket, Guid productId, double percent)
        {
            double discount = 0;
            foreach (var p_q in basket.GetDictionaryProductQuantity())
            {
                var product = p_q.Key;
                var quantity = p_q.Value;
                if (product.Id.Equals(productId))
                {
                    discount += quantity * product.Price * percent;
                }
            }
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
