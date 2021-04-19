using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
    internal class editManagerPermissions : iDecorator
    { // string storeName, string username, List<string> Permissions
        public object todo(LinkedList<object> list){
            string storeName = (string)list.First();
            list.RemoveFirst();
            DataClass.getUser((string)list.First()).GetUserDetails().editPremission(storeName, (List<string>)list.Last());
            return true;
        }
        public string functionName(){ return "editManagerPermissions";}
    }
}
