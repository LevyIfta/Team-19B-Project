using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.Discount
{
    class DiscountCalc : IDiscount
    {

        private Func<ShoppingBasket, double> _f;

        public DiscountCalc(Func<ShoppingBasket, double> f)
        {
            F = f;
        }

        public DiscountCalc(IDiscount calc)
        {
            this.F = new Func<ShoppingBasket, double>(calc.GetFunction());
        }

        public Func<ShoppingBasket, double> F { get => _f; set => _f = value; }

        public double CalcDiscount(ShoppingBasket shoppingBasket)
        {
            return F(shoppingBasket);
        }

        //takes two discount values and activates the function
        private double CompositeHelper(double discount1, double discount2, Func<double, double, double> compositeFunction)
        {
            return compositeFunction(discount1, discount2);
        }

        public IDiscount Max(IDiscount otherDiscountCalc)
        {
            Func<ShoppingBasket, double> newF = new Func<ShoppingBasket, double>((ShoppingBasket shoppingBasket) =>
                                                    CompositeHelper(this.CalcDiscount(shoppingBasket), otherDiscountCalc.CalcDiscount(shoppingBasket), (double d1, double d2) => Math.Max(d1, d2)));
            return new DiscountCalc(newF);
        }

        public IDiscount Add(IDiscount otherDiscountCalc)
        {
            Func<ShoppingBasket, double> newF = new Func<ShoppingBasket, double>((ShoppingBasket shoppingBasket) =>
                                                    CompositeHelper(this.CalcDiscount(shoppingBasket), otherDiscountCalc.CalcDiscount(shoppingBasket), (double d1, double d2) => d1 + d2));
            return new DiscountCalc(newF);
        }

        public Func<ShoppingBasket, double> GetFunction()
        {
            return F;
        }
    }
}

