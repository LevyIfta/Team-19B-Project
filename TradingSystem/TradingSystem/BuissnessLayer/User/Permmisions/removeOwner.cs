using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.commerce;
using TradingSystem.DataLayer.ORM;
using TradingSystem.DataLayer.Permissions;

namespace TradingSystem.BuissnessLayer.User.Permmisions
{
    class removeOwner : aPermission
    {
        public removeOwner(string storeName, string sponser) : base(storeName, sponser) { }
        public override ICollection<aPermissionData> toDataObject(string owner = "")
        {
            aPermissionData me = DataAccess.getPremmisionRO(owner, this.store);
            if (me == null)
                me = new removeOwnerPermissionData(store, sponser);
            if (next == null)
                return new List<aPermissionData> { me };
            else
            {
                ICollection<aPermissionData> ans = next.toDataObject(owner);
                ans.Add(me);
                return ans;
            }
        }

        public override object todo(PersmissionsTypes func, object[] args)
        {//string storeName 0,string username 1, string sponser 2
            if (func == PersmissionsTypes.RemoveOwner && this.store.Equals((string)args[0]))
            {
                if (!Stores.searchStore((string)args[0]).isOwner((string)args[1]))
                    return false;
                Stores.searchStore((string)args[0]).removeOwner(((Member)UserServices.getUser((string)args[1])));
                ((Member)UserServices.getUser((string)args[1])).removePermission((string)args[0], (string)args[2]);
                UserServices.removeEmployeesPermission((string)args[0], (string)args[1]);
                return true;
            }
            return base.todo(func, args);
        }
        public override PersmissionsTypes who()
        {
            return PersmissionsTypes.RemoveOwner;
        }
    }
}
