using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team19B_Project.DataAccess
{
    public class ProductInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        
        public ProductInfo(string name, string description)
        {
            this.id = 0; // it doesn't matter because the id is assigned in other class - Products
            this.name = name;
            this.description = description;
        }
    }
}
