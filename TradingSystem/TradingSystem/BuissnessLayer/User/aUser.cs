using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.User.Permmisions;
using TradingSystem.BuissnessLayer.commerce;

namespace TradingSystem.BuissnessLayer
{
    public abstract class aUser
    {
        public ShoppingCart myCart { get { return myCart; } set { } }


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
            return myCart.getBasket(store);
        }

        public ShoppingCart getCart()
        {
            return myCart;
        }

        public virtual bool EstablishStore(string storeName)
        {
            return false;
        }
        public virtual ICollection<Receipt> purchase(PaymentMethod payment)
        {
            return null;
        }

        public double checkPrice()
        {
            return myCart.checkPrice();

        }
        public Dictionary<Store, Product> browseProducts(string productName, string manufacturer)
        {
            return Stores.searchProduct(productName, manufacturer);
        }
        public Dictionary<Store, Product> browseProducts(string productName, string category, string manufacturer, double minPrice, double maxPrice)
        {
            return Stores.searchProduct(productName, category, manufacturer, minPrice, maxPrice);
        }
        public Store browseStore(string storeName)
        {
            return Stores.searchStore(storeName);
        }
        public virtual ICollection<Receipt> getPurchHistory()
        {
            return null;
        }
        public virtual bool addNewProduct(string storeName, string productName, double price, int amount, string category, string manufacturer)
        {
            return false;
        }
        public virtual bool removeProduct(string storeName, string productName, string manufacturer)
        {
            return false;
        }
        public virtual bool editProduct(string storeName, int productName, double price, string manufacturer)
        {
            return false;
        }
        public virtual ICollection<Receipt> getMyPurchaseHistory(string storeName)
        {
            return null;
        }
        public virtual ICollection<Receipt> getPurchaseHistory(string storeName)
        {
            return null;
        }
        public virtual bool hireNewStoreManager(string storeName, string username)
        {
            return false;
        }
        public virtual bool editManagerPermissions(string storeName, string username, List<PersmissionsTypes> Permissions)
        {
            return false;
        }
        public virtual bool hireNewStoreOwner(string storeName, string username, List<PersmissionsTypes> Permissions)
        {
            return false;
        }
        public virtual bool removeManager(string storeName, string username)
        {
            return false;
        }
        public virtual ICollection<aUser> getInfoEmployees(string storeName)
        {
            return null;
        }
        public virtual Dictionary<string, ICollection<string>> GetAllPermissions()
        {
            return null;
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
