using System;
using System.Collections.Generic;
using System.Text;
using TradingSystem;

namespace UnitTests.Bridge
{
    abstract class BaseProxyBridge: Bridge
    {
        public Bridge RealBridge { protected get; set; }

        public BaseProxyBridge(Bridge realBridge)
        {
            this.RealBridge = realBridge;
        }

        public abstract bool login(string username, string password);
        public abstract bool register(string username, string password);
        public abstract bool logout();
        public abstract Dictionary<string, Dictionary<int, int>> browseProducts(string name, string category, double minPrice, double maxPrice, string manufacturer);
        public abstract Store browseStore(string name);
        public abstract bool saveProduct(string storeName, Dictionary<int, int> Products);
        public abstract LinkedList<string> getPurchHistory();
        public abstract bool removeProductsFromBasket(Dictionary<int, int> products, string storeName);
        public abstract bool removeStoreManager(string storeName, string userName);
        public abstract bool hireNewOwner(string storeName, string username, List<string> premmisions);
        public abstract bool hireNewManager(string storeName, string username);
        public abstract LinkedList<string> getEmployeesInfo(string storename);
        public abstract bool editManagerPermissions(string username, string store, List<string> premmisions);
        public abstract double basketPrice(string storeName);
        public abstract bool addStore(string name);

        public abstract bool addProduct(int productId, double price, int amount, Store store);
        public abstract bool removeProduct(int productId, Store store);
        public abstract bool updateProduct(int productId, double price, int amount, Store store);
       
        public abstract bool removeStore(string name);
        public abstract bool createNewItem(string name, string description, string category, string manafacturer);
        public abstract bool editItem(object todo);
        public abstract bool purchaseBasket(string storeName, string creditCardNumber, int cvv, string holderName, string experationDate);
        public abstract Dictionary<int,int> GetBasket(string storename);
    }
}
