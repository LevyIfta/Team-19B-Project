using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.commerce;

namespace TradingSystem.BuissnessLayer.User.Permmisions
{
    public class getInfoEmployees : aPermission
    {
        public getInfoEmployees(string storeName, string sponser) : base(storeName, sponser) { }
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
