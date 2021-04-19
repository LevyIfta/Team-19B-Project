using System;
using System.Collections.Generic;
using System.Text;
using TradingSystem;

namespace UnitTests.Bridge
{
    interface Bridge
    {
        //user
        bool login(string username, string password);
        bool register(string username, string password);
        bool logout(); //this is missing. todo
        Dictionary<string, Dictionary<int, int>> browseProducts(string name, string category, double minPrice, double maxPrice, string manufacturer); //todo // discuss the signature with stores. they also need to implement this
        Store browseStore(string name); //what does this do? what does it retrive? todo
        bool saveProduct(string storeName, Dictionary<int, int> Products); //save to what? to shopping basket? todo
        Dictionary<int,int> GetBasket(string storename);

        //member
        LinkedList<string> getPurchHistory(); //todo
        //guest

      
        bool removeProductsFromBasket(Dictionary<int, int> products, string storeName); //what about which store? todo

        //admin
        //todo //there is nothinngggg

        //manager
        bool removeStoreManager(string storeName, string userName);
   
        bool hireNewOwner(string storeName, string username, List<string> premmisions); //todo
        bool hireNewManager(string storeName, string username); //todo
        LinkedList<string> getEmployeesInfo(string storeName); //todo
        bool editManagerPermissions(string username, string store, List<string> premmisions); //todo




        //store
        double basketPrice(string storeName);  //todo //isnt basket linked to store anyway? why do you need storename? why not implement in the basket class?
        bool addStore(string name);  //todo //i feel like this is oversimplified. jsut review it to make sure name is everything you need


        bool addProduct(int productId, double price, int amount, Store store); //add item to store todo //i guess this is to add brand new item to the store
        bool removeProduct(int productId, Store store); //remove item from store todo
        bool updateProduct(int productId, double price, int amount, Store store); //edit item in store todo//might want to add a func just to reduce the stock

        
        bool removeStore(string name); //todo todo!!! // this affect alot of users, so make sure to talk about this with the user team
        bool createNewItem(string name, string description, string category, string manafacturer); //create new item info todo
        bool editItem(Object todo); //edit item info todo
        
        bool purchaseBasket(string storeName, string creditCardNumber, int cvv, string holderName, string experationDate); //todo 
        


    }
}
