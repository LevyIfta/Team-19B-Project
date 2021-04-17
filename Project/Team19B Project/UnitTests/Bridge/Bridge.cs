using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Bridge
{
    interface Bridge
    {
        //user
        bool login(string username, string password);
        bool register(string username, string password);
        bool logout(); //this is missing. todo
        Object browseProducts(string name, string category, double minPrice, double maxPrice, double storeRating); //todo // discuss the signature with stores. they also need to implement this
        Object browseStore(string name); //what does this do? what does it retrive? todo
        bool saveProduct(Object store, List<Object> Products); //save to what? to shopping basket? todo

        //member
        List<Object> getPurchHistory(); //todo
        //guest

        Object getStoreAndProductsInfo(Object shop); //what is this func?. todo
        bool removeProductsFromBasket(List<Object> products); //what about which store? todo

        //admin
        //todo //there is nothinngggg

        //manager
        bool removeStoreManager(Object todo); //todo
   
        bool hireNewOwner(Object todo); //todo
        bool hireNewManager(Object todo); //todo
        Object getEmployeesInfo(Object todo); //todo
        bool editManagerPermissions(Object todo); //todo




        //store
        double basketPrice(string storeName, Object basket);  //todo //isnt basket linked to store anyway? why do you need storename? why not implement in the basket class?
        bool addStore(string name);  //todo //i feel like this is oversimplified. jsut review it to make sure name is everything you need


        bool addProduct(int productId, int amount); //add item to store. todo//i guess this is to add stock?  
        bool addProduct(int productId, double price); //add item to store. todo// doesnt make sense that this exist. when will this fuc be used? todo
        bool addProduct(int productId, double price, int amount); //add item to store todo //i guess this is to add brand new item to the store
        bool removeProduct(int productId); //remove item from store todo
        bool updateProduct(int productId, double price, int amount); //edit item in store todo//might want to add a func just to reduce the stock

        Object getStore(string name); // just utility for testing. 
        bool removeStore(string name); //todo todo!!! // this affect alot of users, so make sure to talk about this with the user team
        bool createNewItem(Object todo); //create new item info todo
        bool editItem(Object todo); //edit item info todo
        
        bool purchaseBasket(Object basket, Object creditCard); //todo 
        


    }
}
