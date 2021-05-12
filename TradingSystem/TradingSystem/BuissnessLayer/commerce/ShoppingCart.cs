using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer.commerce
{
    public class ShoppingCart
    {
        public ICollection<ShoppingBasket> baskets { get; set; }
        public aUser owner;
        public ShoppingCart(aUser owner)
        {
            this.owner = owner;
            this.baskets = new LinkedList<ShoppingBasket>();
        }

        public ShoppingBasket getBasket(Store store)
        {
            foreach (ShoppingBasket b in this.baskets)
                if (b.store.Equals(store))
                    return b;
            // create a new basket for the store and return it
            ShoppingBasket basket = new ShoppingBasket(store, this.owner);
            this.baskets.Add(basket);
            return basket;
        }

        public double checkPrice()
        {
            double ans = 0;
            
            foreach (ShoppingBasket basket in baskets)
            {
                ans += basket.store.calcPrice(basket.products);
            }
            return ans;
        }
    }
}
