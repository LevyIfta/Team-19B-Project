using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer.Permissions;

namespace TradingSystem.BuissnessLayer.User.Permmisions
{
    public class editManagerPermissions : aPermission
    {
        public editManagerPermissions(string storeName, string sponser) : base(storeName, sponser) { }

        public override ICollection<aPermissionData> toDataObject()
        {
            aPermissionData me = new editManagerPermissionsData(store, sponser);
            if (next == null)
                return new List<aPermissionData> { me };
            else
            {
                ICollection<aPermissionData> ans = next.toDataObject();
                ans.Add(me);
                return ans;
            }
        }
        public override object todo(PersmissionsTypes func, object[] args)
        {// string storeName, string username, string userSponser, aPermission Permissions
            if (func == PersmissionsTypes.EditManagerPermissions && this.store.Equals((string)args[0]))
            {
                return ((Member)UserServices.getUser((string)args[1])).editPermission((string)args[0], (string)args[2],(aPermission)args[3]);
            }
            return base.todo(func, args);
        }
        public override PersmissionsTypes who()
        {
            return PersmissionsTypes.EditManagerPermissions;
        }
    }
}
