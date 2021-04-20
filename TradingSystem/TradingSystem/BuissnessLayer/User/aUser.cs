using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.User.Permmisions;

namespace TradingSystem.BuissnessLayer
{
    abstract class aUser
    {
        public ShoppingCart myCart { get; set; }


        public abstract string getUserName();

        

        public virtual object todo(PersmissionsTypes func, object[] args)
        {
            return null;
        }

        public bool saveProduct(ShoppingBasket basket)
        {

            throw new NotImplementedException();
        }

        public bool removeProduct(ShoppingBasket basket)
        {

            throw new NotImplementedException();
        }

        public ShoppingBasket getBasket(Store store)
        {
            throw new NotImplementedException();
        }

        public ShoppingCart getCart(Store store)
        {
            throw new NotImplementedException();
        }

        public virtual bool EstablishStore(string storeName)
        {
            return false;
        }

        public virtual  bool purchase(PaymentMethod payment)
        {
            throw new NotImplementedException();
        }

        public double checkPrice()
        {
            throw new NotImplementedException();

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
