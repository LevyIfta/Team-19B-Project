using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.User.Permmisions;

namespace TradingSystem.BuissnessLayer
{
    public abstract class aUser
    {
        public ShoppingCart myCart { get; set; }


        public abstract string getUserName();

        

        public virtual object todo(PersmissionsTypes func, object[] args)
        {
            return null;
        }

        public bool saveProduct(ShoppingBasket basket1)
        {
            bool match = false;
            foreach (ShoppingBasket basket2 in myCart.baskets)
            {
                if(!match && basket2.store.Equals(basket1.store))
                {
                    match = true;
                    basket2.margeBasket(basket1);
                }
            }
            return true;
        }

        public bool removeProduct(ShoppingBasket basket1)
        {
            basket1.reverse();
            bool match = false;
            foreach (ShoppingBasket basket2 in myCart.baskets)
            {
                if (!match && basket2.store.Equals(basket1.store))
                {
                    match = true;
                    basket2.margeBasket(basket1);
                }
            }
            return true;
        }

        public ShoppingBasket getBasket(Store store)
        {
            return myCart.GetBasket(store);
        }

        public ShoppingCart getCart()
        {
            return myCart;
        }

        public virtual bool EstablishStore(string storeName)
        {
            return false;
        }

        public virtual bool purchase(PaymentMethod payment)
        {
            throw new NotImplementedException();
        }

        public double checkPrice()
        {
            return myCart.checkPrice();

        }
        /// <summary>
        /// call everytime you chane anything in the user data
        /// </summary>
        protected virtual void update()
        {
            return;
        }
    }
}
