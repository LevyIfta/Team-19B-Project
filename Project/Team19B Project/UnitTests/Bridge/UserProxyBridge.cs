using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Bridge
{
    abstract class UserProxyBridge : BaseProxyBridge
    {
        public UserProxyBridge(Bridge realBridge) : base(realBridge)
        {

        }

        public override bool login(string username, string password)
        {
            if (RealBridge != null)
                return RealBridge.login(username, password);
            if (username == "falseUser" || password == "falsePassword")
                return false;
            return true;
        }

        public override bool register(string username, string password)
        {
            if (RealBridge != null)
                return RealBridge.register(username, password);
            if (username == "@u@" || username.Length > 10 || password.Length > 15 || password.Contains('\n'))
                return false;
            return true;
        }

        public override bool logout()
        {
            if (RealBridge != null)
                return RealBridge.logout();
            return true;
        }


        public override object browseProducts(string name, string category, double minPrice, double maxPrice, double storeRating)
        {
            if (RealBridge != null)
                return RealBridge.browseProducts(name, category, minPrice, maxPrice, storeRating);
            if (name == "good")
                return "good";
            if (name == "good2")
                return "good2";
            return "bad";

        }

        public override object browseStore(string name)
        {
            if (RealBridge != null)
                return RealBridge.browseStore(name);
            if (name == "good")
                return "good";
            return "bad";
        }

        public override bool saveProduct(object store, List<object> Products)
        {
            if (RealBridge != null)
                return RealBridge.saveProduct(store, Products);
            throw new NotImplementedException();
        }

        public override List<object> getPurchHistory()
        {
            if (RealBridge != null)
                return RealBridge.getPurchHistory();
            throw new NotImplementedException();
        }

        public override object getStoreAndProductsInfo(object shop)
        {
            if (RealBridge != null)
                return RealBridge.getStoreAndProductsInfo(shop);
            throw new NotImplementedException();
        }

        public override bool removeProductsFromBasket(List<object> products)
        {
            if (RealBridge != null)
                return RealBridge.removeProductsFromBasket(products);
            throw new NotImplementedException();
        }

      
    }
}
