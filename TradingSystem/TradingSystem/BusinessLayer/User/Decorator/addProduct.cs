using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{//string storeName,int productId, double price, int amount
    internal class addProduct : iDecorator
    {
        public object todo(LinkedList<object> list){
            string storeName = (string)list.First();
            list.RemoveFirst();
            int productId = (int)list.First();
            list.RemoveFirst();
            return (StoresServices.getStore(storeName)).addProduct(productId, (double)list.First(), (int)list.Last());
        }
        public string functionName(){ return "addProduct";}
    }
}
