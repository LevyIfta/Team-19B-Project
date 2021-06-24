using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplySystem
{
    public class SupplySystem
    {
        public static SupplyInterface supplySystem = new SupplyProxy();
        public static void setReal(string url)
        {
            SupplyInterface real = new SupplyReal(url);
            supplySystem.setReal(real);
        }
    }
}
