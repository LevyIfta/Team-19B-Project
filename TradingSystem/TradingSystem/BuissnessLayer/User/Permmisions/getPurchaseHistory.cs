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
    public class getPurchaseHistory : aPermission
    {
        public getPurchaseHistory(string storeName, string sponser) : base(storeName, sponser) { }
        public override ICollection<aPermissionData> toDataObject(string owner = "")
        {
            aPermissionData me = DataAccess.getPremmisionGPH(owner, this.store);
            if (me == null)
                me = new getPurchaseHistoryPermissionData(store, sponser);
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
            if (func == PersmissionsTypes.GetPurchaseHistory && this.store.Equals((string)args[0]))
            {
                return Stores.searchStore((string)args[0]).getAllReceipts();
            }
            return base.todo(func, args);
        }
        public override PersmissionsTypes who()
        {
            return PersmissionsTypes.GetPurchaseHistory;
        }
    }
}
