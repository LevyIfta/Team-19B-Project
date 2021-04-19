using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{//string storeName, string username, List<string> pre
    internal class hireNewStoreOwner : iDecorator
    {
       public object todo(LinkedList<object> list){
            string storeName = (string)list.First();
            list.RemoveFirst();
            string username = (string)list.First();
            if (StoresServices.getStore(storeName).isOwner(username))
                return false;
           //foreach(string temp in (List<string>)list.Last()){
                DataClass.getUser(username).GetUserDetails().addPremission(storeName, (List<string>)list.Last());
            
            StoresServices.getStore(storeName).addOwner(username);
            return true;
        }
        public string functionName(){ return "hireNewStoreOwner";}
    }
}
