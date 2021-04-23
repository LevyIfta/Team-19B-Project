using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    class ProductsInfoData
    {
        public static ICollection<ProductInfoData> productsInfo = new LinkedList<ProductInfoData>();
    }
}
