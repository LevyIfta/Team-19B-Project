using System;
using System.Collections.Generic;
using System.Text;
using TradingSystem.ServiceLayer;
using TradingSystem.BuissnessLayer;

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
        ShoppingBasket getBasket(string storeName);//what happens in case of few users with baskets in the same store? maybe should add string userName to get specific basket
        int getProductAmount(ShoppingBasket basket, ProductInfo info);
        int getProductAmount(string storeName, string productName);
        void addProducts(ShoppingBasket basket); //to cart
        void removeProducts(ShoppingBasket basket); //to cart

        void addInventory(ShoppingBasket basket); //to store
        void removeInventory(ShoppingBasket basket);
        bool isItemAtStore(string storeName, string productName);
        
        Store getStore(string storeName);
        Reciept GetRecieptByUser(string storeName, string userName, DateTime Date);
        Reciept GetRecieptByStore(string storeName, string userName, DateTime Date);

        void purchase(ShoppingBasket basket);


    }
}
