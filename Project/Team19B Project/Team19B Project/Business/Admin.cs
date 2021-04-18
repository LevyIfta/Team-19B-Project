using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
    class Admin : Decorator
    {
        public override object browseStore(string name)
        {
            throw new NotImplementedException();
        }

        public override void EstablishStore()
        {
            throw new NotImplementedException();
        }
        
        public override void register(string username, string password)
        {
            throw new NotImplementedException();
        }

    }
}
