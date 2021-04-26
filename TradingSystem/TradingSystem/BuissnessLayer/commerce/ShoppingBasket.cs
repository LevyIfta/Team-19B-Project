using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer
{
    public class ShoppingBasket
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
        public ShoppingBasket(BasketData shoppingBasketData)
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
        public void margeBasket(ShoppingBasket basket)
        {
            bool isMatch = false;
            foreach (Product product1 in basket.products)
            {
                foreach (Product product2 in products)
                {
                    if (!isMatch && product1.Equals(product2))
                    {
                        isMatch = true;
                        product2.addAmount(product1.amount);
                        products.Remove(product1);
                        products.Add(product2);
                    }
                }
                if (!isMatch)
                {
                    products.Add(product1);
                }
                isMatch = false;
            }
        }
        public void addProduct(Product pro)
        {
            products.Add(pro);
        }
        public void reverse()
        {
            foreach (Product pro in products)
            {
                pro.amount = pro.amount * -1;
            }
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


        public override bool Equals(object obj)
        {
            return false;
        }
        public bool Equals(ShoppingBasket obj)
        {
            bool isMatch = false;
            foreach(Product product1 in obj.products)
            {
                foreach(Product product2 in products)
                {
                    if (product1.Equals(product2))
                    {
                        isMatch = true;
                    }
                }
                if (!isMatch)
                {
                    return false;
                }
            }
            return true;
        }
        
        public BasketData toDataObject()
        {
            return new BasketData((ICollection<ProductData>)this.products.Select(p => p.toDataObject()), store.toDataObject(), Member.objectToData(this.owner));
        }

        public void update()
        {
            BasketDAL.update(this.toDataObject());
        }
    }
}
