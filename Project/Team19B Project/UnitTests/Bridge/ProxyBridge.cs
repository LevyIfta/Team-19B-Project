using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Bridge
{
    class ProxyBridge : Bridge
    {
        public Bridge RealBridge { private get; set; }

        public ProxyBridge()
        {
            this.RealBridge = null;
        }

        public object buyBasket(object todo)
        {
            throw new NotImplementedException();
        }

        public object getReciept(object todo)
        {
            throw new NotImplementedException();
        }

        public bool login(string username, string password)
        {
            if (RealBridge != null)
                return RealBridge.login(username, password);
            if (username == "falseUser" || password == "falsePassword") 
                return false;
            return true;
        }

        public bool promote(object todo)
        {
            throw new NotImplementedException();
        }

        public bool register(string username, string password)
        {
            if (RealBridge != null)
                return RealBridge.register(username, password);
            if (username == "@u@" || username.Length > 10 || password.Length > 15 || password.Contains('\n') )
                return false;
            return true;
        }

        public object retriveBasket(object todo)
        {
            throw new NotImplementedException();
        }

        public object searchItem(object todo)
        {
            throw new NotImplementedException();
        }
    }
}
