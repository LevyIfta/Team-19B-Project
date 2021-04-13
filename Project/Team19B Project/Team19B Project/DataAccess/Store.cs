using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team19B_Project.DataAccess
{
    public class Store
    {
        public string name { get; set; }
        public Dictionary<int, int> products { get; set; } // a dictionary of - <product_id, amount>

        public Store(string name)
        {
            this.name = name;
            this.products = new Dictionary<int, int>();
        }

        
    }
}
