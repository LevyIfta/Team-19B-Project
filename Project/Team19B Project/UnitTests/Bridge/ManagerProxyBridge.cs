using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Bridge
{
    abstract class ManagerProxyBridge : UserProxyBridge
    {
        public ManagerProxyBridge(Bridge realBridge) : base(realBridge)
        {

        }

        public override bool editManagerPermissions(string username, string store, List<string> premmisions)
        {
            if (RealBridge != null)
                return RealBridge.editManagerPermissions(username, store, premmisions);
            throw new NotImplementedException();
        }

        public override LinkedList<string> getEmployeesInfo(string storename)
        {
            if (RealBridge != null)
                return RealBridge.getEmployeesInfo(storename);
            throw new NotImplementedException();
        }

        public override bool hireNewManager(string storeName, string username)
        {
            if (RealBridge != null)
                return RealBridge.hireNewManager(storeName, username);
            throw new NotImplementedException();
        }

        public override bool hireNewOwner(string storeName, string username, List<string> premmisions)
        {
            if (RealBridge != null)
                return RealBridge.hireNewOwner(storeName, username, premmisions);
            throw new NotImplementedException();
        }


        public override bool removeStoreManager(string storeName, string userName)
        {
            if (RealBridge != null)
                return RealBridge.removeStoreManager(storeName, userName);
            throw new NotImplementedException();
        }


    }
}
