using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.commerce;
using TradingSystem.DataLayer.Permissions;

namespace TradingSystem.BuissnessLayer.User.Permmisions
{
    public class editProduct : aPermission
    {
        public editProduct(string storeName, string sponser) : base(storeName, sponser) { }
        public override ICollection<aPermissionData> toDataObject()
        {
            aPermissionData me = new editProductPermissionData(store, sponser);
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
        {// string storeName,int productId, double price, string manufacturer
            if (func == PersmissionsTypes.EditProduct && this.store.Equals((string)args[0]))
            {
                if (Stores.searchStore((string)args[0]).isProductExist((string)args[1], (string)args[3]))
                {
                    Stores.searchStore((string)args[0]).editPrice((string)args[1], (string)args[3], (double)args[2]);
                    return true;
                }
                return false;
            }
            return base.todo(func, args);
        }
        public override PersmissionsTypes who()
        {
            return PersmissionsTypes.EditProduct;
        }
    }
}
