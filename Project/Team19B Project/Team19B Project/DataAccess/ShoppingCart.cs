using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team19B_Project.DataAccess
{
    public class ShoppingCart
    {
        public Dictionary<string, ShoppingBasket> baskets { get; } // <storeName, basket>
        public string ownerId { get; }

        public ShoppingCart(string ownerId)
        {
            this.ownerId = ownerId;
            this.baskets = new Dictionary<string, ShoppingBasket>();
        }

        public ShoppingBasket getBasket(string storeName)
        {
            return this.baskets[storeName];
        }


    }
}
