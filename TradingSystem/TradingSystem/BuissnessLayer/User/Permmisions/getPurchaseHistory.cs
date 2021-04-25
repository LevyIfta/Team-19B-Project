using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.commerce;

namespace TradingSystem.BuissnessLayer.User.Permmisions
{
    public class getPurchaseHistory : aPermission
    {
        public getPurchaseHistory(string storeName, string sponser) : base(storeName, sponser) { }
        public override object todo(PersmissionsTypes func, object[] args)
        { // string storeName
            if (func == PersmissionsTypes.GetPurchaseHistory && this.store.Equals((string)args[0]))
            {
                return Stores.searchStore((string)args[0]).getAllReceipts();
            }
            return base.todo(func, args);
        }
    }
}
