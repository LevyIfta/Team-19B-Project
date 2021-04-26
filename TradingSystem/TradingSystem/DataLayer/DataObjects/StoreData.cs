using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    public class StoreData
    {
        //FIELDS
        public string storeName { get; set; }
        public string founder { get; set; }

        //CONSTRUCTORS
        public StoreData(string storeName, string founder)
        {
            this.storeName = storeName;
            this.founder = founder;
        }

        //EQUALS
        public override bool Equals(object obj)
        {
            return false;
        }

        public bool Equals(StoreData other)
        {
            return this.storeName.Equals(other.storeName) & this.founder.Equals(other.founder);
        }
    }
}