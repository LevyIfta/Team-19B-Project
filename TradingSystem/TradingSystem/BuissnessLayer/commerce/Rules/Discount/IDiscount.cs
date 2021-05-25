using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.Discount
{
    public interface IDiscount
    {
        double CalcDiscount(ShoppingBasket shoppingBasket);

        IDiscount Max(IDiscount otherDiscountCalc);

        IDiscount Add(IDiscount otherDiscountCalc);
        Func<ShoppingBasket, double> GetFunction();
    }
}
