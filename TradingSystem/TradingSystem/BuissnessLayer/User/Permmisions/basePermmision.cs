using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer.Permissions;

namespace TradingSystem.BuissnessLayer.User.Permmisions
{
    public class basePermmision : aPermission
    {
        public basePermmision(string storeName, string sponser) : base(storeName, sponser) { }

        public override ICollection<aPermissionData> toDataObject(string owner = "")
        {
            return next.toDataObject(owner);
        }
    }
}
