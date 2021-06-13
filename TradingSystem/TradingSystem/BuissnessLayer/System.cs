using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer
{
    public static class Build
    {
        private static bool unLoaded = true;
        public static void Load()
        {
            if(unLoaded)
            {
                unLoaded = false;

                //todo 
            }

        }
    }

    public static class TearDown
    {
        public static void tearDown()
        {
            DataLayer.ORM.DataAccess.tearDown();
        }
    }
}
