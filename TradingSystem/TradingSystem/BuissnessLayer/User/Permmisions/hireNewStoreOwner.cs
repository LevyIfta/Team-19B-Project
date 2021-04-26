using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.commerce;

namespace TradingSystem.BuissnessLayer.User.Permmisions
{
    public class hireNewStoreOwner : aPermission 
    {
        public hireNewStoreOwner(string storeName, string sponser) : base(storeName, sponser) { }
        public override object todo(PersmissionsTypes func, object[] args)
        {//string storeName, string username, aPermission Permissions
            if (func == PersmissionsTypes.HireNewStoreOwner && this.store.Equals((string)args[0]))
            {
                if (Stores.searchStore((string)args[0]).isOwner((string)args[1]))
                    return false;
                if (Stores.searchStore((string)args[0]).isManager((string)args[1]))
                    Stores.searchStore((string)args[0]).removeManager(((Member)UserServices.getUser((string)args[1])));
                Stores.searchStore((string)args[0]).addOwner(((Member)UserServices.getUser((string)args[1])));
                ((Member)UserServices.getUser((string)args[1])).addPermission((aPermission)args[2]);
                return true;
            }
            return base.todo(func, args);
        }
    }
}
