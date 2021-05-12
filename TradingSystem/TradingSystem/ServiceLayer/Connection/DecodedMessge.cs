using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.ServiceLayer
{
    public enum msgType
    {
        FUNC,
        OBJ,
        ALARM
    }
    public struct DecodedMessge
    {
        public msgType type;
        public string name;
        public string[] param_list;
    }
}
