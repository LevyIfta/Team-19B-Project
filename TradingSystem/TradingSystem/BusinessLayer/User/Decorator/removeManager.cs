using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
    internal class removeManager : iDecorator
    {
        public object todo(LinkedList<object> list){
            string storeName = (string)list.First();
            string username = (string)list.Last();
            DataClass.getUser(username).GetUserDetails().removePremission(storeName);
            StoresServices.getStore(storeName).removeManager(username);
            return true;
        }
        public string functionName(){ return "removeManager";}
    }
}
