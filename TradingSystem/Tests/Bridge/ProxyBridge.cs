using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Bridge
{
    class ProxyBridge : Bridge
    {
        private Bridge realBridge;
        public ProxyBridge(Bridge real)
        {
            this.realBridge = real;
        }

        public bool login(string username, string password)
        {
            if (realBridge != null)
                return this.realBridge.login(username, password);
            if (username == "good" && password != "bad")
                return true;
            return false;
        }
    }
}
