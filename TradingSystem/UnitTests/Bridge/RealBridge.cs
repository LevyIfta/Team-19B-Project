using System;
using System.Collections.Generic;
using System.Text;
using TradingSystem;

namespace UnitTests.Bridge
{
    class RealBridge : Bridge
    {

        public bool addProduct(int productId, double price, int amount, Store store)
        {
            return interfaceClass.addNewProduct(store.name , productId, price, amount);
        }

        public bool addStore(string name)
        {
            return StoresServices.addStore(name, interfaceClass.getUsername);
            //   return interfaceClass.EstablishStore(name);
        }

        public double basketPrice(string storeName)
        {
            ShoppingBasket basket = interfaceClass.checkShoppingBasketDetails(storeName);
            return StoresServices.basketPrice(storeName, basket);
        }

        public Dictionary<string, Dictionary<int, int>> browseProducts(string name, string category, double minPrice, double maxPrice, string manufacturer)
        {
            return TradingSystem.interfaceClass.browseProducts(name, minPrice, maxPrice, category, manufacturer);
        }

        public Store browseStore(string name)
        {
            return TradingSystem.interfaceClass.browseStore(name);
        }

        public bool createNewItem(string name, string description, string category, string manafacturer)
        {
            ProductInfo item = new ProductInfo(name, description, category, manafacturer);
            return Products.Instance.addProduct(item);
        }

        public bool editItem(object todo)
        {
            throw new NotImplementedException();
        }

        public bool editManagerPermissions(string username, string store, List<string> premmisions)
        {
            return interfaceClass.editManagerPermissions(store, username, premmisions);
        }

        public Dictionary<int, int> GetBasket(string storename)
        {
            return interfaceClass.checkShoppingBasketDetails(storename).products;
        }

        public LinkedList<string> getEmployeesInfo(string storename)
        {
            return interfaceClass.getInfoEmployees(storename);
        }

        public LinkedList<string> getPurchHistory()
        {
            return interfaceClass.getPurchHistory();
        }



        public bool hireNewManager(string storeName, string username)
        {
            return interfaceClass.hireNewStoreManager(storeName, username);
        }

        public bool hireNewOwner(string storeName, string username, List<string> premmisions)
        {
            return interfaceClass.hireNewStoreOwner(storeName, username, premmisions);
        }

        public bool login(string username, string password)
        {
            return TradingSystem.interfaceClass.login(username, password);
        }

        public bool logout()
        {
            TradingSystem.interfaceClass.logout();
            return true;
     
        }

        public bool purchaseBasket(string storeName, string creditCardNumber, int cvv, string holderName, string experationDate)
        {
            CreditCardInfo card = new CreditCardInfo(creditCardNumber, cvv, holderName, experationDate);
            ShoppingBasket basket = interfaceClass.checkShoppingBasketDetails(storeName);
            return interfaceClass.purchase(storeName);
        }

        public bool register(string username, string password)
        {
            return TradingSystem.interfaceClass.register(username, password);
        }

        public bool removeProduct(int productId, Store store)
        {
            return interfaceClass.removeProduct(store.name, productId);
        }

        public bool removeProductsFromBasket(Dictionary<int, int> products, string storeName)
        {
          
            return interfaceClass.removeProduct(storeName, products);
        }

        public bool removeStore(string name)
        {
            throw new NotImplementedException();
        }

        public bool removeStoreManager(string storeName, string userName)
        {
            return interfaceClass.removeManager(storeName, userName);
        }

        public bool saveProduct(string storeName, Dictionary<int, int> Products)
        {
       
            return interfaceClass.saveProduct(storeName, Products);
        }

        public bool updateProduct(int productId, double price, int amount, Store store)
        {
            return interfaceClass.editProduct(store.name, productId, price);
        }
    }
}
