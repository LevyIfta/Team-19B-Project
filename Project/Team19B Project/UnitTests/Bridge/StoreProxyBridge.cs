using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Bridge
{
    class StoreProxyBridge : ManagerProxyBridge
    {
        public override bool addProduct(int productId, double price)
        {
            if (RealBridge != null)
                return RealBridge.addProduct(productId, price);
            throw new NotImplementedException();
        }

        public override bool addProduct(int productId, double price, int amount)
        {
            throw new NotImplementedException();
        }

        public override bool addProducts(int productId, int amount)
        {
            throw new NotImplementedException();
        }

        public override bool addStore(string name)
        {
            throw new NotImplementedException();
        }

        public override double basketPrice(string storeName, object basket)
        {
            throw new NotImplementedException();
        }

        public override bool createNewItem(object todo)
        {
            throw new NotImplementedException();
        }

        public override bool editItem(object todo)
        {
            throw new NotImplementedException();
        }

        public override object getStore(string name)
        {
            throw new NotImplementedException();
        }

        public override bool purchaseBasket(object basket, object creditCard)
        {
            throw new NotImplementedException();
        }

        public override bool removeProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public override bool removeStore(string name)
        {
            throw new NotImplementedException();
        }

        public override bool updateProduct(int productId, double price, int amount)
        {
            throw new NotImplementedException();
        }
    }
}
