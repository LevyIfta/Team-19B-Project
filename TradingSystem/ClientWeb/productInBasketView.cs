using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWeb
{
    class productInBasketView
    {
        public Dictionary<string, object> Custom { get; set; }

        public productInBasketView() 
        {
            Custom = new Dictionary<string, object>();
        }
    }
}
