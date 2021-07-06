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
    public class addProduct : aPermission
    {
        public addProduct(string storeName, string sponser) : base(storeName, sponser) { }

        public override ICollection<aPermissionData> toDataObject(string owner = "")
        {


            aPermissionData me = DataAccess.getPremmisionAP(owner, this.store);
            if (me == null)
                me = new addProductPermissionData(store, sponser);
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
        {//string storeName 0,string productName 1, double price 2, int amount 3, string category 4, string manufacturer 5
            if (func == PersmissionsTypes.AddProduct && this.store.Equals((string)args[0]))
            {
                if(Stores.searchStore((string)args[0]).isProductExist((string)args[1], (string)args[5]))
                {
                    Stores.searchStore((string)args[0]).supply((string)args[1], (string)args[5], (int)args[3]);
                }
                else
                {
                    Stores.searchStore((string)args[0]).addProduct((string)args[1], (string)args[4], (string)args[5]);
                    Stores.searchStore((string)args[0]).supply((string)args[1], (string)args[5], (int)args[3]);
                }
                Stores.searchStore((string)args[0]).editPrice((string)args[1], (string)args[5], (double)args[2]);
                return true;
            }
            return base.todo(func, args);
        }
        public override PersmissionsTypes who()
        {
            return PersmissionsTypes.AddProduct;
        }
    }
}
