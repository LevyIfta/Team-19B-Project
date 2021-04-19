using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{// string storeName,string username
    internal class hireNewStoreManager : iDecorator
    {
        public object todo(LinkedList<object> list){
            string storeName = (string)list.First();
            string username = (string)list.Last();
            if (StoresServices.getStore(storeName).isOwner(username) && StoresServices.getStore(storeName).isManager(username))
                return false;
            List<string> list1 = new List<string>();
            list1.Add("getInfoEmployees");
            DataClass.getUser(username).GetUserDetails().addPremission(storeName, list1);
            StoresServices.getStore(storeName).addManager(username);
            return true;
             
        }
        public string functionName(){ return "hireNewStoreManager";}
    }
}
