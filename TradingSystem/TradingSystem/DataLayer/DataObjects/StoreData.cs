using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public virtual ICollection<ProductData> inventory { get; set; }
        public virtual ICollection<MemberData> owners { get;  set; }
        public virtual ICollection< MemberData> managers { get;  set; }
        public virtual ICollection<MessageData> messages { get; set; }
     
        [NotMapped]
        public virtual ICollection<iPolicyDiscountData> discountPolicies { get; set; }
        [NotMapped]
        public virtual ICollection<iPolicyData> purchasePolicies { get; set; }

        //CONSTRUCTORS
        public StoreData(string storeName, MemberData founder, ICollection<ReceiptData> receipts, ICollection<ProductData> inventory, ICollection<MemberData> owners, ICollection<MemberData> managers, ICollection<MessageData> messages, ICollection<iPolicyDiscountData> discountPolicies, ICollection<iPolicyData> purchasePolicies)
        {
            this.storeName = storeName ?? throw new ArgumentNullException(nameof(storeName));
            this.founder = founder ?? throw new ArgumentNullException(nameof(founder));
            this.receipts = receipts ?? throw new ArgumentNullException(nameof(receipts));
            this.inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            this.owners = owners ?? throw new ArgumentNullException(nameof(owners));
            this.managers = managers ?? throw new ArgumentNullException(nameof(managers));
            this.messages = messages ?? throw new ArgumentNullException(nameof(messages));
            this.discountPolicies = discountPolicies ?? throw new ArgumentNullException(nameof(discountPolicies));
            this.purchasePolicies = purchasePolicies ?? throw new ArgumentNullException(nameof(purchasePolicies));
        }

        public StoreData()
        {
            this.receipts = new ReceiptData[] { };
            this.inventory = new ProductData[] { };
            this.owners = new MemberData[] { };
            this.managers = new MemberData[] { };
            this.messages = new MessageData[] { };
            this.discountPolicies = new iPolicyDiscountData[] { };
            this.purchasePolicies = new iPolicyData[] { };

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