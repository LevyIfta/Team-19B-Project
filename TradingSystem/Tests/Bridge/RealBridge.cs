using System;
using System.Collections.Generic;
using System.Text;
using TradingSystem.BuissnessLayer;
using TradingSystem.BuissnessLayer.commerce;

namespace Tests.Bridge
{
    class RealBridge : Bridge
    {
        public void addInventory(ShoppingBasket basket)
        {
            foreach (Product item in basket.products)
            {
                basket.store.supply(item.info.name, item.info.manufacturer, item.amount);
            }
        }

        public void addProducts(ShoppingBasket basket)
        {
            ShoppingBasket userBasket = getBasket(basket.store.storeName);
            userBasket.margeBasket(basket);
        }

        public ShoppingBasket getBasket(string storeName)
        {
            throw new NotImplementedException();
        }

        public int getProductAmount(ShoppingBasket basket, ProductInfo info)
        {
            //non
            throw new NotImplementedException();
        }

        public Receipt GetRecieptByStore(string storeName, string userName, DateTime Date)
        {
            foreach (Receipt item in getStore(storeName).receipts)
            {
                if (item.username == userName && item.date == Date)
                    return item;
            }
            return null;
        }

        public Receipt GetRecieptByUser(string storeName, string userName, DateTime Date)
        {
            throw new NotImplementedException();
        }

        public Store getStore(string storeName)
        {
            return Stores.searchStore(storeName);
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
            return Stores.searchProduct(productName).Count != 0;
        }

        public bool isStoreExist(string storeName)
        {
            return Stores.searchStore(storeName) == null;
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
            return Stores.addStore(storeName, (Member)getUser());
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
            //none
            throw new NotImplementedException();
        }

        public void removeProducts(ShoppingBasket basket)
        {
            //non
            throw new NotImplementedException();
        }
    }
}
