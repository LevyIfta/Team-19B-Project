using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
    public class SupplySystem
    {
        public static bool supply(ShoppingBasket basket)
        {
            DirAppend.AddToLogger("supply from: " + basket.storeName + ", to: " + basket.owner + ".", Result.LOG);
            return true;
        }
    }
}
