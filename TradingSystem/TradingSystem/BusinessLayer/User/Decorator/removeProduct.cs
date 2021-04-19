using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
    internal class removeProduct : iDecorator
    {
        public object todo(LinkedList<object> list){
            return (StoresServices.getStore((string)list.First())).removeProduct((int)list.Last());
        }

        public string functionName(){ return "removeProduct";}
    }
}
