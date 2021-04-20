using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer
{
    class ShoppingCart
    {
        public ICollection<ShoppingBasket> baskets { get; set; }
    }
}
