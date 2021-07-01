using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer.Permissions
{
    class getInfoEmployeesPermissionData : aPermissionData
    {
        public getInfoEmployeesPermissionData(MemberData myOwner, string store, string sponser) : this(store, sponser)
        {
            this.myOwner = myOwner;
            this.myOwnerName = myOwner.userName;
        }

        public getInfoEmployeesPermissionData(string store, string sponser) : base(store, sponser)
        {

        }
    }
}
