using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{// string storeName,int productId, double price
    internal class editProduct : iDecorator
    {
        public object todo(LinkedList<object> list){
            string storeName = (string)list.First();
            list.RemoveFirst();
            return (StoresServices.getStore(storeName)).editPrice((int)list.First(), (double)list.Last());
        }
        public string functionName(){ return "editProduct";}
    }
}
