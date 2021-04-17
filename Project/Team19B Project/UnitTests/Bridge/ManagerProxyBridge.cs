using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Bridge
{
    abstract class ManagerProxyBridge : UserProxyBridge
    {

        public override bool editManagerPermissions(object todo)
        {
            if (RealBridge != null)
                return RealBridge.editManagerPermissions(todo);
            throw new NotImplementedException();
        }

        public override object getEmployeesInfo(object todo)
        {
            if (RealBridge != null)
                return RealBridge.getEmployeesInfo(todo);
            throw new NotImplementedException();
        }

        public override bool hireNewManager(object todo)
        {
            if (RealBridge != null)
                return RealBridge.hireNewManager(todo);
            throw new NotImplementedException();
        }

        public override bool hireNewOwner(object todo)
        {
            if (RealBridge != null)
                return RealBridge.hireNewOwner(todo);
            throw new NotImplementedException();
        }


        public override bool removeStoreManager(object todo)
        {
            if (RealBridge != null)
                return RealBridge.removeStoreManager(todo);
            throw new NotImplementedException();
        }


    }
}
