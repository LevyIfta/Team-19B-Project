using System;
using System.Collections.Generic;
using System.Text;
using TradingSystem;

namespace UnitTests.Bridge
{
    class StoreProxyBridge : ManagerProxyBridge
    {
        public StoreProxyBridge(Bridge realBridge) : base(realBridge) 
        {
      
        }
      
        public override bool addProduct(int productId, double price, int amount, Store store)
        {
            if (RealBridge != null)
                return RealBridge.addProduct( productId,  price,  amount,  store);
            throw new NotImplementedException();
        }

        public override bool addStore(string name)
        {
            if (RealBridge != null)
                return RealBridge.addStore(name);
            throw new NotImplementedException();
        }

        public override double basketPrice(string storeName)
        {
            if (RealBridge != null)
                return RealBridge.basketPrice(storeName);
            return 1.1;
        }

        public override bool createNewItem(string name, string description, string category, string manafacturer)
        {
            if (RealBridge != null)
                return RealBridge.createNewItem(name, description,  category,  manafacturer);
            throw new NotImplementedException();
        }

        public override bool editItem(object todo)
        {
            if (RealBridge != null)
                return RealBridge.editItem(todo);
            throw new NotImplementedException();
        }


        public override bool purchaseBasket(string storeName, string creditCardNumber, int cvv, string holderName, string experationDate)
        {
            if (RealBridge != null)
                return RealBridge.purchaseBasket(storeName, creditCardNumber, cvv, holderName, experationDate);
            throw new NotImplementedException();
        }

        public override bool removeProduct(int productId, Store store)
        {
            if (RealBridge != null)
                return RealBridge.removeProduct(productId, store);
            throw new NotImplementedException();
        }

        public override bool removeStore(string name)
        {
            if (RealBridge != null)
                return RealBridge.removeStore(name);
            throw new NotImplementedException();
        }



        public override bool updateProduct(int productId, double price, int amount, Store store)
        {
            if (RealBridge != null)
                return RealBridge.updateProduct(productId, price, amount, store);
            throw new NotImplementedException();
        }
    }
}
