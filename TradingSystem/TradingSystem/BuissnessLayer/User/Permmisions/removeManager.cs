using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.commerce;

namespace TradingSystem.BuissnessLayer.User.Permmisions
{
    public class removeManager : aPermission
    {
        public removeManager(string storeName, string sponser) : base(storeName, sponser) { }
        public override object todo(PersmissionsTypes func, object[] args)
        { // string storeName, string username, string userSponser
            if (func == PersmissionsTypes.RemoveManager && this.store.Equals((string)args[0]))
            {
                if (!((Member)UserServices.getUser((string)args[1])).removePermission((string)args[0], (string)args[2]))
                    return false;
                Stores.searchStore((string)args[0]).removeManager(((Member)UserServices.getUser((string)args[1])));
                return true;
            }
            return base.todo(func, args);
        }
        public override PersmissionsTypes who()
        {
            return PersmissionsTypes.RemoveManager;
        }
    }
}
