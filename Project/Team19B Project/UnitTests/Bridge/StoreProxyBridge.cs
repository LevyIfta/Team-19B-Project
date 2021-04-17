using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Bridge
{
    class StoreProxyBridge : ManagerProxyBridge
    {
        public StoreProxyBridge(Bridge realBridge) : base(realBridge) 
        {
      
        }
        public override bool addProduct(int productId, double price)
        {
            if (RealBridge != null)
                return RealBridge.addProduct(productId, price);
            throw new NotImplementedException();
        }

        public override bool addProduct(int productId, double price, int amount)
        {
            if (RealBridge != null)
                return RealBridge.addProduct(productId, price, amount);
            throw new NotImplementedException();
        }

        public override bool addProduct(int productId, int amount)
        {
            if (RealBridge != null)
                return RealBridge.addProduct(productId, amount);
            throw new NotImplementedException();
        }

        public override bool addStore(string name)
        {
            if (RealBridge != null)
                return RealBridge.addStore(name);
            throw new NotImplementedException();
        }

        public override double basketPrice(string storeName, object basket)
        {
            if (RealBridge != null)
                return RealBridge.basketPrice(storeName, basket);
            throw new NotImplementedException();
        }

        public override bool createNewItem(object todo)
        {
            if (RealBridge != null)
                return RealBridge.createNewItem(todo);
            throw new NotImplementedException();
        }

        public override bool editItem(object todo)
        {
            if (RealBridge != null)
                return RealBridge.editItem(todo);
            throw new NotImplementedException();
        }

        public override object getStore(string name)
        {
            if (RealBridge != null)
                return RealBridge.getStore(name);
            throw new NotImplementedException();
        }

        public override bool purchaseBasket(object basket, object creditCard)
        {
            if (RealBridge != null)
                return RealBridge.purchaseBasket(basket, creditCard);
            throw new NotImplementedException();
        }

        public override bool removeProduct(int productId)
        {
            if (RealBridge != null)
                return RealBridge.removeProduct(productId);
            throw new NotImplementedException();
        }

        public override bool removeStore(string name)
        {
            if (RealBridge != null)
                return RealBridge.removeStore(name);
            throw new NotImplementedException();
        }

        public override bool updateProduct(int productId, double price, int amount)
        {
            if (RealBridge != null)
                return RealBridge.updateProduct(productId, price, amount);
            throw new NotImplementedException();
        }
    }
}
