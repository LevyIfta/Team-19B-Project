using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules
{
    interface IRule
    {
        bool Check(ShoppingBasket shoppingBasket);
        Guid GetId();
        Rule AndRules(IRule additionalRule);

    }
}
