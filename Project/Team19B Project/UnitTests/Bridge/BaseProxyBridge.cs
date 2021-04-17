using System;
using System.Collections.Generic;
using System.Text;

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
        public abstract object browseProducts(string name, string category, double minPrice, double maxPrice, double storeRating);
        public abstract object browseStore(string name);
        public abstract bool saveProduct(object store, List<object> Products);
        public abstract List<object> getPurchHistory();
        public abstract object getStoreAndProductsInfo(object shop);
        public abstract bool removeProductsFromBasket(List<object> products);
        public abstract bool removeStoreManager(object todo);
        public abstract bool hireNewOwner(object todo);
        public abstract bool hireNewManager(object todo);
        public abstract object getEmployeesInfo(object todo);
        public abstract bool editManagerPermissions(object todo);
        public abstract double basketPrice(string storeName, object basket);
        public abstract bool addStore(string name);
        public abstract bool addProduct(int productId, int amount);
        public abstract bool addProduct(int productId, double price);
        public abstract bool addProduct(int productId, double price, int amount);
        public abstract bool removeProduct(int productId);
        public abstract bool updateProduct(int productId, double price, int amount);
        public abstract object getStore(string name);
        public abstract bool removeStore(string name);
        public abstract bool createNewItem(object todo);
        public abstract bool editItem(object todo);
        public abstract bool purchaseBasket(object basket, object creditCard);
    }
}
