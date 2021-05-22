using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.commerce;
using TradingSystem.DataLayer;

namespace TradingSystem.BuissnessLayer
{
    public class Admin : Member
    {
        public Admin(MemberData member) : base(member)
        {

        }

        public Admin(string username, string password) : base(username, password)
        {
        }

        public ICollection<Receipt> getAllReceipts()
        {
            ICollection<Receipt> list = new List<Receipt>();
            foreach (Store store in Stores.getAllStores())
            {
                list = mergeList(list, store.getAllReceipts());
            }
            return list;
        }
        private ICollection<Receipt> mergeList(ICollection<Receipt> list1, ICollection<Receipt> list2)
        {
            foreach(Receipt receipt in list2)
            {
                list1.Add(receipt);
            }
            return list1;
        }
    }
}
