using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Bridge
{
    class Driver
    {

        public static Bridge getBridge()
        {
            return new ProxyBridge(new RealBridge());
        }
    }
}
