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
    class removeProduct : aPermission
    {
        public removeProduct(string storeName, string sponser) : base(storeName, sponser) { }
        public override ICollection<aPermissionData> toDataObject(string owner = "")
        {
            aPermissionData me = DataAccess.getPremmisionRP(owner, this.store);
            if (me == null)
                me = new removeProductPermissionData(store, sponser);
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
        {//string storeName,string productId,string manufacturer
            if (func == PersmissionsTypes.RemoveProduct && this.store.Equals((string)args[0]))
            {
                Stores.searchStore((string)args[0]).removeProduct((string)args[1], (string)args[2]);
                return true;
            }
            return base.todo(func, args);
        }
        public override PersmissionsTypes who()
        {
            return PersmissionsTypes.RemoveProduct;
        }
    }
}
