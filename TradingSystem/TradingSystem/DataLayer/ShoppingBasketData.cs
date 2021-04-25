using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    class ShoppingBasketData
    {
        public ICollection<ProductData> products { get; }
        public StoreData store { get; set; }
        public memberData owner { get; }

        public ShoppingBasketData(ICollection<ProductData> products, StoreData store, memberData owner)
        {
            this.products = products;
            this.store = store;
            this.owner = owner;
        }
    }
}
