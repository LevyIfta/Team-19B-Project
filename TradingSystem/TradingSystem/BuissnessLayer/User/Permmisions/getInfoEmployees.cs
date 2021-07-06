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
    public class getInfoEmployees : aPermission
    {
        public getInfoEmployees(string storeName, string sponser) : base(storeName, sponser) { }
        public override ICollection<aPermissionData> toDataObject(string owner = "")
        {
            aPermissionData me = DataAccess.getPremmisionGIE(owner, this.store);
            if (me == null)
                me = new getInfoEmployeesPermissionData(store, sponser);
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
        { // string storeName
            if (func == PersmissionsTypes.GetInfoEmployees && this.store.Equals((string)args[0]))
            {
                ICollection<Member> list1 = Stores.searchStore((string)args[0]).getOwners();
                ICollection<Member> list2 = Stores.searchStore((string)args[0]).getManagers();
                foreach (Member temp in list2)
                {
                    list1.Add(temp);
                }
                return list1;
            }
            return base.todo(func, args);
        }
        public override PersmissionsTypes who()
        {
            return PersmissionsTypes.GetInfoEmployees;
        }
    }
}
