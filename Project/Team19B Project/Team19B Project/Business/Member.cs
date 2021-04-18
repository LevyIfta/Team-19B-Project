using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
    abstract class Member : User
    {
        protected string username;
        protected string password;
        protected userDetails uDetails;
        public Member()
        {
            username = "";
            password = "";
            uDetails = null;
        }

        //public abstract void logout();
        public abstract void EstablishStore(/*maybe needs details*/);
        public List<object> getPurchHistory()
        {
            return null;
        }

    }
}
