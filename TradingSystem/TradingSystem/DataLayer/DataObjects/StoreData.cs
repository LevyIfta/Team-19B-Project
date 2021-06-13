using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    public class StoreData
    {
        //FIELDS
        [Key]
        public string storeName { get; set; }
        public virtual MemberData founder { get; set; }
        public virtual ICollection<ReceiptData> receipts { get; set; }
        public virtual ICollection<ProductData> inventory { get; private set; }
        public virtual ICollection<MemberData> owners { get;  set; }
        public virtual ICollection< MemberData> managers { get;  set; }

     

        public virtual ICollection<iPolicyDiscountData> discountPolicies { get; set; }
        public virtual ICollection<iPolicyData> purchasePolicies { get; set; }

        //CONSTRUCTORS
        public StoreData(string storeName, MemberData founder, ICollection<ReceiptData> receipts, ICollection<ProductData> inventory, ICollection<MemberData> owners, ICollection<MemberData> managers, ICollection<iPolicyDiscountData> discountPolicies, ICollection<iPolicyData> purchasePolicies)
        {
            this.storeName = storeName ?? throw new ArgumentNullException(nameof(storeName));
            this.founder = founder ?? throw new ArgumentNullException(nameof(founder));
            this.receipts = receipts ?? throw new ArgumentNullException(nameof(receipts));
            this.inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            this.owners = owners ?? throw new ArgumentNullException(nameof(owners));
            this.managers = managers ?? throw new ArgumentNullException(nameof(managers));
            this.discountPolicies = discountPolicies ?? throw new ArgumentNullException(nameof(discountPolicies));
            this.purchasePolicies = purchasePolicies ?? throw new ArgumentNullException(nameof(purchasePolicies));
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