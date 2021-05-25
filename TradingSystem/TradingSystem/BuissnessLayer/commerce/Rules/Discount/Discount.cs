using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules.Discount
{
    public class Discount
    {
        private Guid _id;

        private IDiscount _calc;

        public Discount(IDiscount calc)
        {
            Id = Guid.NewGuid();
            Calc = calc;
        }

        public Guid Id { get => _id; set => _id = value; }
        public IDiscount Calc { get => _calc; set => _calc = value; }

        public virtual double ApplyDiscounts(ShoppingBasket shoppingBasket)
        {
            return _calc.CalcDiscount(shoppingBasket);
        }
        public virtual IRule GetRule()
        {
            return null;
        }
    }
}
