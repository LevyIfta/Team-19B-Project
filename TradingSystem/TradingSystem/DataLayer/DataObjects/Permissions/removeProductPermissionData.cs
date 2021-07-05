using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer.Permissions
{
    public class removeProductPermissionData : aPermissionData
    {
        public removeProductPermissionData(MemberData myOwner, string store, string sponser) : this(store, sponser)
        {
            this.myOwner = myOwner;
            this.myOwnerName = myOwner.userName;
        }

        public removeProductPermissionData(string store, string sponser) : base(store, sponser)
        {

        }
    }
}
