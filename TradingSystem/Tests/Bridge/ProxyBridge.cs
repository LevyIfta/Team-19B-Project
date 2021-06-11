using System;
using System.Collections.Generic;
using System.Text;
using TradingSystem.BuissnessLayer;
using TradingSystem.BuissnessLayer.commerce;
using TradingSystem.ServiceLayer;

namespace Tests.Bridge
{
    class ProxyBridge : Bridge
    {
        private Bridge realBridge;
        public ProxyBridge(Bridge real)
        {
            this.realBridge = real;
        }

        public void addInventory(ShoppingBasket basket)
        {
            if (realBridge != null)
            {
                this.realBridge.addInventory(basket);
                return;
            }
            throw new NotImplementedException();
        }

        public void saveProducts(string userName, string storeName, string manufacturer, Dictionary<string, int> product)
        {
            if (realBridge != null)
            {
                this.realBridge.saveProducts( userName,  storeName,  manufacturer,  product);
                return;
            }
            throw new NotImplementedException();
        }
        public void addProducts(ShoppingBasket basket)
        {
            if (realBridge != null)
            {
                this.realBridge.addProducts(basket);
                return;
            }
            throw new NotImplementedException();
        }

        public ShoppingBasket getBasket(string storeName)
        {
            if (realBridge != null)
                return this.realBridge.getBasket(storeName);
            throw new NotImplementedException();
        }

        public int getProductAmount(ShoppingBasket basket, ProductInfo info)
        {
            if (realBridge != null)
            {
                return this.realBridge.getProductAmount(basket, info);
                
            }
            throw new NotImplementedException();
        }

        public double getProductAmount(string storeName, string productName, string manufacturer)
        {
            if (realBridge != null)
                return this.realBridge.getProductAmount(storeName, productName, manufacturer);
            throw new NotImplementedException();
        }

        public Receipt GetRecieptByStore(string storeName, string userName, DateTime Date)
        {
            if (realBridge != null)
                return this.realBridge.GetRecieptByStore(storeName, userName, Date);
            throw new NotImplementedException();
        }

        public Receipt GetRecieptByUser(string storeName, string userName, DateTime Date)
        {
            if (realBridge != null)
                return this.realBridge.GetRecieptByUser(storeName, userName, Date);
            throw new NotImplementedException();
        }

        public Store getStore(string storeName)
        {
            if (realBridge != null)
                return this.realBridge.getStore(storeName);
            throw new NotImplementedException();
        }

        public aUser getUser()
        {
            if (realBridge != null)
                return this.realBridge.getUser();
            throw new NotImplementedException();
        }

        public string getUserName()
        {
            if (realBridge != null)
                return this.realBridge.getUserName();
            throw new NotImplementedException();
        }

        public bool isItemAtStore(string storeName, string productName, string manufacturar)
        {
            if (realBridge != null)
                return this.realBridge.isItemAtStore(storeName, productName, manufacturar);
            throw new NotImplementedException();
        }

        public bool isProductExist(string productName, string manufacturar)
        {
            if (realBridge != null)
                return this.realBridge.isProductExist(productName, manufacturar);
            throw new NotImplementedException();
        }

        public bool isStoreExist(string storeName)
        {
            if (realBridge != null)
                return this.realBridge.isStoreExist(storeName);
            throw new NotImplementedException();
        }

        public bool isUserExist(string username, string password)
        {
            if (realBridge != null)
                return this.realBridge.isUserExist(username, password);
            throw new NotImplementedException();
        }

        public bool isUserLoggedIn(string username)
        {
            if (realBridge != null)
                return this.realBridge.isUserLoggedIn(username);
            throw new NotImplementedException();
        }

        public bool isUserUnique(string username)
        {
            if (realBridge != null)
                return this.realBridge.isUserUnique(username);
            throw new NotImplementedException();
        }

        public bool login(string username, string password)
        {
            if (realBridge != null)
                return this.realBridge.login(username, password);
            if (username == "good" && password != "bad")
                return true;
            return false;
        }

        public void logout()
        {
            if (realBridge != null)
            {
                this.realBridge.logout();
                return;
            }
            throw new NotImplementedException();
        }

        public int onlineUserCount()
        {
            if (realBridge != null)
                return this.realBridge.onlineUserCount();
            throw new NotImplementedException();
        }

        public bool openStore(string storeName)
        {
            if (realBridge != null)
                return this.realBridge.openStore(storeName);
            throw new NotImplementedException();
        }

        public string[] purchase(string creditNumber, string validity, string cvv)
        {
            if (realBridge != null)
            {
                return this.realBridge.purchase(creditNumber, validity, cvv);
            }
            throw new NotImplementedException();
        }

        public void purchase(ShoppingBasket basket)
        {
            throw new NotImplementedException();
        }

        public string[] register(string userName, string password)
        {
            if (realBridge != null)
                return this.realBridge.register(userName, password);
            throw new NotImplementedException();
        }
        public string[] register(string userName, string password, double age, string gender, string address)
        {
            if (realBridge != null)
                return this.realBridge.register(userName, password, age, gender, address);
            throw new NotImplementedException();
        }
        public void removeInventory(ShoppingBasket basket)
        {
            if (realBridge != null)
            {
                this.realBridge.removeInventory(basket);
                return;
            }
            throw new NotImplementedException();
        }

        public void removeProducts(ShoppingBasket basket)
        {
            if (realBridge != null)
            {
                this.realBridge.removeProducts(basket);
                return;
            }
            throw new NotImplementedException();
        }
        public double checkPrice(string username)
        {
            if (realBridge != null)
            {
                return this.realBridge.checkPrice(username);
            }
            throw new NotImplementedException();
        }
    }
}
