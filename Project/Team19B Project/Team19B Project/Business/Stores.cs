using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team19B_Project.Business
{
    public class Stores
    {
        // a singleton that contains all the stores
        public Dictionary<string, Store> stores { get; }
        public static Stores instance = null;

        private Stores()
        {
            this.stores = new Dictionary<string, Store>();
        }

        public static Stores Instance
        {
            get
            {
                if (instance == null)
                    instance = new Stores();
                return instance;
            }
        }
    }
}
