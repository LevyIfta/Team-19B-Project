using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.User.Permmisions
{
    public class basePermmision : aPermission
    {
        public basePermmision(string storeName, string sponser) : base(storeName, sponser) { }
    }
}
