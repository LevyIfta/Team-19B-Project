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
    public class hireNewStoreManager : aPermission 
    {
        public hireNewStoreManager(string storeName, string sponser) : base(storeName, sponser) { }
        public override ICollection<aPermissionData> toDataObject(string owner = "")
        {
            aPermissionData me = DataAccess.getPremmisionHNM(owner, this.store);
            if (me == null)
                me = new hireNewStoreManagerPermissionData(store, sponser);
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
        {// string storeName,string username, string userSponser
            if (func == PersmissionsTypes.HireNewStoreManager && this.store.Equals((string)args[0]))
            {
                if (Stores.searchStore((string)args[0]).isManager((string)args[1]) || Stores.searchStore((string)args[0]).isOwner((string)args[1]))
                    return false;
                aPermission temp = new getInfoEmployees((string)args[0], (string)args[2]);
                ((Member)UserServices.getUser((string)args[1])).addPermission(temp);
                Stores.searchStore((string)args[0]).addManager(((Member)UserServices.getUser((string)args[1])));
                return true;
            }
            return base.todo(func, args);
        }// only - getInfoEmployees
        public override PersmissionsTypes who()
        {
            return PersmissionsTypes.HireNewStoreManager;
        }
    }
}
