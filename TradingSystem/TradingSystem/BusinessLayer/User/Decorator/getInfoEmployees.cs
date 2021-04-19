using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
    internal class getInfoEmployees : iDecorator
    {
        public object todo(LinkedList<object> list){
            string storeName = (string)list.First();
            LinkedList<string> list1 = StoresServices.getStore(storeName).getManagers();
            LinkedList<string> list2 = StoresServices.getStore(storeName).getOwners();
            foreach( string temp in list2)
            {
                list1.AddLast(temp);
            }
            return list1;
        }
        public string functionName(){ return "getInfoEmployees";}
    }
}
