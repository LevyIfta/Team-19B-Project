using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
     class Member : User
    {
        protected string username;
        protected string password;
        protected userDetails uDetails;
        //add list of permissions
        public Member()
        {
            username = "";
            password = "";
            uDetails = null;
        }
        public Member(string username, string password)
        {
            this.username = username;
            this.password = password;
            this.uDetails = new userDetails();
        }
        public Member(string username, string password, userDetails uDetails)
        {
            this.username = username;
            this.password = password;
            this.uDetails = uDetails;
        }

        //public abstract void logout();
        override
        public bool EstablishStore(string storeName)
        {
            return StoresServices.addStore(storeName, this.username);
        }

        override
        public LinkedList<string> getPurchHistory()
        {
            return PurchaseReceipts.Instance.getReceipts(username);
        }
        override
        public bool saveProduct(string storeName, Dictionary<int, int> products) 
        {
           bool ret = true;
           foreach(KeyValuePair<int,int> p in products){
                ret = ret && ShoppingCartsServices.addProducts(username, storeName, p.Key, p.Value);
            }
           return ret;
        }
        override
        public bool removeProduct(string storeName, Dictionary<int, int> Products)
        {
            bool ret = true;
            foreach(KeyValuePair<int, int> p in Products){
                ret = ret && ShoppingCartsServices.removeProducts(this.username, storeName, p.Key, p.Value);
            }
            return ret;
        }
        override
        public bool isManager(string storeName){
            return uDetails.haveManagerPremisition(storeName);
        }
        public string getPassword(){ return this.password;}
        public userDetails GetUserDetails() { return uDetails;}

        public override bool register(string username, string password)
        {
            return false;
        }

        public override ShoppingBasket checkShoppingBasketDetails(string storeName)
        {
            return ShoppingCartsServices.getBasket(username, storeName);
        }

        public override bool purchase(string storeName)
        {
            return ShoppingCartsServices.purchaseBasket(username, storeName);
        }

        public override string getUserName()
        {
            return this.username;
        }
    }
}
