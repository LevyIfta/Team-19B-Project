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
        string[] register(string userName, string password);
        string[] register(string userName, string password, double age, string gender, string address);
        void logout();
        aUser getUser();
        string getUserName();
        bool isUserExist(string username, string password);
        bool isUserUnique(string username);
        bool isUserLoggedIn(string username);
        int onlineUserCount();
        bool openStore(string storeName);
        bool isStoreExist(string storeName);

        bool isProductExist(string productName, string manufacturar);
        ShoppingBasket getBasket(string storeName);

        int getProductAmount(ShoppingBasket basket, ProductInfo info);
        double getProductAmount(string storeName, string productName, string manufacturer);
        void saveProducts(string userName, string storeName, string manufacturer, Dictionary<string, int> product); //to cart
        void addProducts(ShoppingBasket basket);
        void removeProducts(ShoppingBasket basket); //to cart

        void addInventory(ShoppingBasket basket); //to store
        void removeInventory(ShoppingBasket basket);
        bool isItemAtStore(string storeName, string productName, string manufacturar);
        
        Store getStore(string storeName);
        Receipt GetRecieptByUser(string storeName, string userName, DateTime Date);
        Receipt GetRecieptByStore(string storeName, string userName, DateTime Date);
        double checkPrice(string username);
        string[] purchase(string creditNumber, string validity, string cvv);

        void purchase(ShoppingBasket basket);


    }
}
