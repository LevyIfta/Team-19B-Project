using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.ServiceLayer
{
    class SLstore
    {
        public string storeName;
        public ICollection<SLreceipt> receipts;
        public ICollection<SLproduct> inventory;
        public ICollection<string> ownerNames;
        public ICollection<string> managerNames;
        public string founderName;

        private const int PARAMETER_COUNT = 6;

        public SLstore(string storeName, ICollection<SLreceipt> receipts, ICollection<SLproduct> inventory, ICollection<string> ownerNames, ICollection<string> managerNames, string founderName)
        {
            this.storeName = storeName;
            this.receipts = receipts;
            this.inventory = inventory;
            this.ownerNames = ownerNames;
            this.managerNames = managerNames;
            this.founderName = founderName;
        }

        public SLstore(Object[] parameters)
        {
            this.storeName = (string)parameters[0];
            this.receipts = (ICollection<SLreceipt>)parameters[1];
            this.inventory = (ICollection<SLproduct>)parameters[2];
            this.ownerNames = (ICollection<string>)parameters[3];
            this.managerNames = (ICollection<string>)parameters[4];
            this.founderName = (string)parameters[5];
        }
    }
}
