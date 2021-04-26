using System;
using System.Collections.Generic;
using System.Text;
using TradingSystem.BuissnessLayer;

namespace Tests.Bridge
{
    class RealBridge : Bridge
    {
        public void addInventory(ShoppingBasket basket)
        {
            throw new NotImplementedException();
        }

        public void addProducts(ShoppingBasket basket)
        {
            throw new NotImplementedException();
        }

        public ShoppingBasket getBasket(string storeName)
        {
            throw new NotImplementedException();
        }

        public int getProductAmount(ShoppingBasket basket, ProductInfo info)
        {
            throw new NotImplementedException();
        }

        public Reciept GetRecieptByStore(string storeName, string userName, DateTime Date)
        {
            throw new NotImplementedException();
        }

        public Reciept GetRecieptByUser(string storeName, string userName, DateTime Date)
        {
            throw new NotImplementedException();
        }

        public Store getStore(string storeName)
        {
            throw new NotImplementedException();
        }

        public aUser getUser()
        {
            throw new NotImplementedException();
        }

        public string getUserName()
        {
            throw new NotImplementedException();
        }

        public bool isProductExist(string productName)
        {
            throw new NotImplementedException();
        }

        public bool isStoreExist(string storeName)
        {
            throw new NotImplementedException();
        }

        public bool isUserExist(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool isUserLoggedIn(string username)
        {
            throw new NotImplementedException();
        }

        public bool isUserUnique(string username)
        {
            throw new NotImplementedException();
        }

        public bool login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public void logout()
        {
            throw new NotImplementedException();
        }

        public int onlineUserCount()
        {
            throw new NotImplementedException();
        }

        public bool openStore(string storeName)
        {
            throw new NotImplementedException();
        }

        public void purchase()
        {
            throw new NotImplementedException();
        }

        public bool register(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public void removeInventory(ShoppingBasket basket)
        {
            throw new NotImplementedException();
        }

        public void removeProducts(ShoppingBasket basket)
        {
            throw new NotImplementedException();
        }
    }
}
