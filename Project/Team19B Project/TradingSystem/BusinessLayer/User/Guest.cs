using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem;

namespace TradingSystem
{
    internal class Guest : User
    {
        private string id;
        public Guest()
        {
           id = "guest";
        }
        

        override
        public bool saveProduct(string storeName, Dictionary<int, int> products)
        {
           bool ret = true;
           foreach(KeyValuePair<int,int> p in products){
                ret = ret && ShoppingCartsServices.addProducts(id, storeName, p.Key, p.Value);
            }
           return ret;
        }
        override
        public bool removeProduct(string storeName, Dictionary<int, int> Products)
        {
            bool ret = true;
            foreach(KeyValuePair<int,int> p in Products){
                ret = ret && ShoppingCartsServices.removeProducts(id, storeName, p.Key, p.Value);
            }
            return ret;
        }
        override
        public ShoppingBasket checkShoppingBasketDetails(string storeName)
        {
            return ShoppingCartsServices.getBasket(id, storeName);
        }

        override
        public bool register(string name, string pass)
        {
           return DataClass.addRegistered(name, pass);
        }

        override
        public bool purchase(string storeName)
        {
            return ShoppingCartsServices.purchaseBasket(id, storeName);
        }
        override
        public bool isManager(string storeName){
            return false;
        }

        override
         public bool EstablishStore(string storeName)
        {
            return false;
        }
        override
        public LinkedList<string> getPurchHistory()
        {
            return null;
        }

        public override string getUserName()
        {
            return "";
        }
    }
}
