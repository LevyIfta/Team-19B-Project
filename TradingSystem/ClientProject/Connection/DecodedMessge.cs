using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientProject.Connection
{
    enum msgType
    {
        FUNC,
        OBJ,
        ALARM
    }
    struct DecodedMessge
    {
        public msgType type;
        public string name;
        public string[] param_list;
    }
}
