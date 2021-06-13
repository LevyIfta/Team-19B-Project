using System;
using System.Collections.Generic;
using System.Text;
using TradingSystem.BuissnessLayer;
using TradingSystem.BuissnessLayer.commerce;
using TradingSystem.ServiceLayer;
using TradingSystem.BuissnessLayer.User.Permmisions;

namespace Tests.Bridge
{
    class RealBridge : Bridge
    {
        public void addInventory(ShoppingBasket basket)
        {
            foreach (Product item in basket.products)
            {
                basket.store.addProduct(item.info.name, item.info.category, item.info.manufacturer);
                basket.store.editPrice(item.info.name, item.info.manufacturer, item.price);
                basket.store.supply(item.info.name, item.info.manufacturer, item.amount);
            }
        }

        public void saveProducts(string userName, string storeName, string manufacturer, Dictionary<string, int> product)
        {
            //ShoppingBasket userBasket = getBasket(basket.store.name);
            //userBasket.margeBasket(basket);
            UserController.saveProduct( userName,  storeName,  manufacturer,  product);
        }
        public void addProducts(ShoppingBasket basket)
        {
            ShoppingBasket userBasket = getBasket(basket.owner.userName, basket.store.name);
            userBasket.margeBasket(basket);
        }

        public ShoppingBasket getBasket(string userName,string storeName)
        {
            return UserServices.getBasket(userName, storeName);
        }

        public int getProductAmount(ShoppingBasket basket, ProductInfo info)
        {
            foreach (Product item in basket.products)
            {
                if (item.info.Equals(info))
                    return item.amount;
            }
            return 0;
        }

        public double getProductAmount(string storeName, string productName, string manufacturer)
        {
            foreach (Product product in getStore(storeName).inventory)
                if (product.info.name.Equals(productName) & product.info.manufacturer.Equals(manufacturer))
                    return product.amount;
            return -1;
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
            foreach (Receipt receipt in UserServices.getAllReceiptsHistory(userName, storeName) )
            {
                if (receipt.date.Equals(Date))
                    return receipt;
            }
            return null;
        }

        public Store getStore(string storeName)
        {
            return Stores.searchStore(storeName);
        }
        public aUser getUser(string userName)
        {
            return UserServices.getUser(userName);
        }

        

        public bool isItemAtStore(string storeName, string productName, string manufacturar)
        {
            return Stores.searchStore(storeName).isProductExist(productName, manufacturar);
        }

        public bool isProductExist(string productName, string manufacturar)
        {
            return Stores.searchProduct(productName, manufacturar).Count != 0;
        }

        public bool isStoreExist(string storeName)
        {
            return Stores.searchStore(storeName) != null;
        }

        public bool isUserExist(string username, string password)
        {
            aUser user = UserServices.getUser(username);
            if (user == null)
                return false;
            if (user is Guest)
                return false;
            return ((Member)user).password.Equals(password);
        }

        public bool isUserLoggedIn(string username)
        {
            return UserServices.isUserOnline(username);
        }

        public bool isUserUnique(string username)
        {
            return UserServices.getUser(username) == null;
        }

        public string[] login(string userName, string password)
        {
            return UserController.login(userName, password);
        }

        public void logout()
        {
            UserController.logout();
         
        }

        public int onlineUserCount()
        {
            return UserServices.countOnlineUsers();
        }

        public bool openStore(string userName, string storeName)
        {
            return UserServices.EstablishStore(userName, storeName);
        }

        public string[] purchase(string userName, string creditNumber, string validity, string cvv)
        {
            return UserController.purchase(userName, creditNumber, validity, cvv);
        }

        public void purchase(ShoppingBasket basket)
        {
            throw new NotImplementedException();
        }

        public string[] register(string userName, string password)
        {
            return UserController.register(userName, password);
        }
        public string[] register(string userName, string password, double age, string gender, string address)
        {
            return UserController.register(userName, password, age, gender, address);
        }
        public double checkPrice(string username)
        {
            return UserController.checkPrice(username);
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
        public bool hireNewStoreManager(string ownerName, string storeName, string employeeName)
        {
            return UserController.hireNewStoreManager(ownerName, storeName, employeeName);
        }
        public bool editManagerPermissions(string ownerName, string storeName, string employeeName, List<string> permissions)
        {
            return UserController.editManagerPermissions(ownerName, storeName, employeeName, permissions);
        }
        public bool addNewProduct(string username, string storeName, string productName, double price, int amount, string category, string manufacturer)
        {
            return UserController.addNewProduct(username, storeName, productName, price, amount, category, manufacturer);
        }
        public bool editProduct(string userName, string storeName, string productName, double price, string manufacturer)
        {
            return UserController.editProduct(userName, storeName, productName, price, manufacturer);
        }
        public bool hireNewStoreOwner(string ownerName, string storeName, string employeeName, List<string> permissions)
        {
            return UserController.hireNewStoreOwner(ownerName, storeName, employeeName, permissions);
        }
        public ICollection<SLemployee> getInfoEmployees(string userName, string storeName)
        {
            return UserController.getInfoEmployees(userName, storeName);
        }
        public bool removeManager(string ownerName, string storeName, string employeeName)
        {
            return UserController.removeManager(ownerName, storeName, employeeName);
        }
        public bool removeOwner(string ownerName, string storeName, string employeeName)
        {
            return UserController.removeOwner(ownerName, storeName, employeeName);
        }
        public aUser getAdmin()
        {
            return UserServices.getAdmin();
        }

    }
}
