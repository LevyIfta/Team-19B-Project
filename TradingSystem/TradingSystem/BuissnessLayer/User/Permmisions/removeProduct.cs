using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.commerce;

namespace TradingSystem.BuissnessLayer.User.Permmisions
{
    class removeProduct : aPermission
    {
        public removeProduct(string storeName, string sponser) : base(storeName, sponser) { }
        public override object todo(PersmissionsTypes func, object[] args)
        {//string storeName,string productId,string manufacturer
            if (func == PersmissionsTypes.RemoveProduct && this.store.Equals((string)args[0]))
            {
                Stores.searchStore((string)args[0]).removeProduct((string)args[1], (string)args[2]);
                return true;
            }
            return base.todo(func, args);
        }
    }
}
