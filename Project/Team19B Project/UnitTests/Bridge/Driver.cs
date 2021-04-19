using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Bridge
{
    class Driver
    {
        public static Bridge GetBridge()
        {
            return new StoreProxyBridge(new RealBridge());
        }
    }
}
