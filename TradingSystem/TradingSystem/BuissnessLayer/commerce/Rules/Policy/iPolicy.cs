using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce.Rules
{
    interface iPolicy
    {
        bool isValid(ICollection<Product> products, aUser user);

    }
}
