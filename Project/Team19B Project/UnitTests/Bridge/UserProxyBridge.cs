using System;
using System.Collections.Generic;
using System.Text;
using TradingSystem;

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


        public override Dictionary<string, Dictionary<int, int>> browseProducts(string name, string category, double minPrice, double maxPrice, string manufacturer)
        {
            if (RealBridge != null)
                return RealBridge.browseProducts(name, category, minPrice, maxPrice, manufacturer);
            if (name == "good")
                return null;
            if (name == "good2")
                return null;
            return null;

        }

        public override Store browseStore(string name)
        {
            if (RealBridge != null)
                return RealBridge.browseStore(name);
            if (name == "good")
                return null;
            return null;
        }

        public override bool saveProduct(string storeName, Dictionary<int, int> Products)
        {
            if (RealBridge != null)
                return RealBridge.saveProduct(storeName, Products);
            throw new NotImplementedException();
        }

        public override LinkedList<string> getPurchHistory()
        {
            if (RealBridge != null)
                return RealBridge.getPurchHistory();
            throw new NotImplementedException();
        }

        public override bool removeProductsFromBasket(Dictionary<int, int> products, string storeName)
        {
            if (RealBridge != null)
                return RealBridge.removeProductsFromBasket(products, storeName);
            throw new NotImplementedException();
        }
        public override Dictionary<int, int> GetBasket(string storename)
        {
            if (RealBridge != null)
                return RealBridge.GetBasket(storename);
            Dictionary<int, int> dic = new Dictionary<int, int>();
            dic.Add(1, 1);
            dic.Add(3, 5);
            return dic;
        }



    }
}
