using System;
using System.Collections.Generic;
using System.Text;
using TradingSystem.ServiceLayer;
using TradingSystem.BuissnessLayer;
using TradingSystem.BuissnessLayer.commerce;

namespace Tests.Bridge
{
    interface Bridge
    {
        //unit tests
        bool login(string userName, string password);
        bool register(string userName, string password);
        void logout();
        aUser getUser();
        string getUserName();
        bool isUserExist(string username, string password);
        bool isUserUnique(string username);
        bool isUserLoggedIn(string username);
        int onlineUserCount();
        bool openStore(string storeName);
        bool isStoreExist(string storeName);
        bool isProductExist(string productName);
        ShoppingBasket getBasket(string storeName);
        int getProductAmount(ShoppingBasket basket, ProductInfo info);
        void addProducts(ShoppingBasket basket); //to cart
        void removeProducts(ShoppingBasket basket); //to cart

        void addInventory(ShoppingBasket basket); //to store
        void removeInventory(ShoppingBasket basket);
        
        Store getStore(string storeName);
        Receipt GetRecieptByUser(string storeName, string userName, DateTime Date);
        Receipt GetRecieptByStore(string storeName, string userName, DateTime Date);
        void purchase();


    }
}
