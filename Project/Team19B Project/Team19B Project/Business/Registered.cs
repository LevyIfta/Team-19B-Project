using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
    class Registered : Member
    {
        public Registered() : base() { }
        public Registered(string username, string password) : base()
        {
            this.username = username;
            this.password = password;
        }
        public Registered(string username, string password, userDetails uDetails) : base()
        {
            this.username = username;
            this.password = password;
            this.uDetails = uDetails;

        }

        public override object browseStore(string name)
        {
            throw new NotImplementedException();
        }

        override
        public void EstablishStore(/*maybe needs details*/)
        {

        }

        public override void register(string username, string password)
        {
            
        }
    }
}
