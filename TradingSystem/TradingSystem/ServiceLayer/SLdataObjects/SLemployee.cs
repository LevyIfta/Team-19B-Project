using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.ServiceLayer
{
    //TODO
    public class SLemployee
    {
        public string userName;
        public Dictionary<string, ICollection<string>> permissionsPerStore; //per store?

        private const int PARAMETER_COUNT = 2;

        public SLemployee(string userName, Dictionary<string, ICollection<string>> permissionsPerStore)
        {
            this.userName = userName;
            this.permissionsPerStore = permissionsPerStore;
        }

        public SLemployee(Object[] parameters)
        {
            this.userName = (string)parameters[0];
            this.permissionsPerStore = (Dictionary<string, ICollection<string>>)parameters[1];
        }
    }
}
