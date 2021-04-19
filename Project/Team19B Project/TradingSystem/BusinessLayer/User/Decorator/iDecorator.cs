using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
    internal interface iDecorator 
    {
        
        object todo(LinkedList<object> args);
        string functionName();
    }
}
