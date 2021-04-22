using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer
{
    class ShoppingBasket
    {
        public ICollection<Product> products { get; set; }
        public Store store { get; set; }
        public string owner { get; }

        public ShoppingBasket(Store store, string owner)
        {
            this.store = store;
            this.products = new LinkedList<Product>();
            this.owner = owner;
        }

        public double checkPrice()
        {
            return store.calcPrice(products);
        }

        public Receipt purchase(PaymentMethod payment)
        {
            return store.executePurchase(this, payment);
        }

        public ShoppingBasket clone()
        {
            return null;
        }

        internal void clean()
        {
            this.products = new LinkedList<Product>();
        }
    }
}
