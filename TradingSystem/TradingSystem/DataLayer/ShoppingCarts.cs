using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
    public class ShoppingCarts
    {
        // this class is a singleton that holds the shopping carts
        public Dictionary<string, ShoppingCart> carts { get; } // <owner username, cart>
        private static ShoppingCarts instance = null;
        public bool addCart(string ownerId)
        {
            // adds a new shopping cart for the user with id = ownerId
            if (this.carts.Keys.Contains(ownerId))
                return false;
            this.carts.Add(ownerId, new ShoppingCart(ownerId));
            return true;
        }

        private ShoppingCarts()
        {
            this.carts = new Dictionary<string, ShoppingCart>();
        }

        public static ShoppingCarts Instance
        {
            get
            {
                if (instance == null)
                    instance = new ShoppingCarts();
                return instance;
            }
        }


    }
}
