using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.ServiceLayer.Controllers
{
    class SystemController
    {
        public static void tearDown()
        {
            TradingSystem.BuissnessLayer.TearDown.tearDown();
        }

        public static void Build()
        {
            TradingSystem.BuissnessLayer.Build.Load();
        }
    }
}
