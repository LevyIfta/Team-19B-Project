using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer
{
    class ShoppingBasket
    {
        public ICollection<Product> products { get; set; }
        public Store store { get; set; }
        public Member owner { get; }

        public ShoppingBasket(Store store, Member owner)
        {
            this.store = store;
            this.products = new LinkedList<Product>();
            this.owner = owner;
        }

        public ShoppingBasket(ShoppingBasketData shoppingBasketData)
        {
            this.products = (ICollection<Product>)shoppingBasketData.products.Select(p => new Product(p));
            this.store = new Store(shoppingBasketData.store);
            this.owner = Member.dataToObject(shoppingBasketData.owner);
        }

        public double checkPrice()
        {
            return store.calcPrice(products);
        }

        public Receipt purchase(PaymentMethod payment)
        {
            return store.executePurchase(this, payment);
        }

        public ShoppingBasketData toDataObject()
        {
            return new ShoppingBasketData((ICollection<ProductData>)this.products.Select(p => p.toDataObject()), store.toDataObject(), Member.objectToData(this.owner));
        }

        public ShoppingBasket clone()
        {
            ShoppingBasket basket = new ShoppingBasket(this.store, this.owner);
            // clone the products
            basket.products = new LinkedList<Product>(this.products.ToList());
            return basket;
        }

        internal void clean()
        {
            this.products = new LinkedList<Product>();
        }
    }
}
