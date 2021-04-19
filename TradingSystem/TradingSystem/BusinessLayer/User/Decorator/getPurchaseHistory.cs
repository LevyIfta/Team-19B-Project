using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
    internal class getPurchaseHistory : iDecorator
    {
        public object todo(LinkedList<object> list){
            return PurchaseReceipts.Instance.getStoresReceipts((string)list.First());
        }
        public string functionName(){ return "getPurchaseHistory";}
    }
}
