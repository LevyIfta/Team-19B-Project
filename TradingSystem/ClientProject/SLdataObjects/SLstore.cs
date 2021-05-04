using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientProject
{
    public class SLstore
    {
        public string storeName { get; }
        public ICollection<SLreceipt> receipts { get; }
        public ICollection<SLproduct> inventory { get; }
        public ICollection<string> ownerNames { get; }
        public ICollection<string> managerNames { get; }
        public string founderName { get; }

        public const int PARAMETER_COUNT = 6;

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
