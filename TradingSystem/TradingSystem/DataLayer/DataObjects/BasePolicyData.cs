using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer;
using TradingSystem.BuissnessLayer.commerce;
using TradingSystem.BuissnessLayer.commerce.Rules;

namespace TradingSystem.DataLayer
{
    public class BasePolicyData : iPolicyData
    {
        
        [Key, Column(Order = 0)]
        public Guid id { get; set; }
        [Key, Column(Order = 1)]
        public string storeName { get; set; }

        [NotMapped]
        public Func<Product, aUser, bool> predicate { get; set; }
        [NotMapped]
        public Func<Product, bool> isRelevant;
        public bool Default { get; set; }
        public BasePolicyData(Guid id,  Func<Product, aUser, bool> predicate, Func<Product, bool> isRelevant, bool Default, string storeName):base()
        {
            this.id = id;
            this.predicate = predicate;
            this.isRelevant = isRelevant;
            this.Default = Default;
            this.storeName = storeName;
        }

        public BasePolicyData() { }

        public override iPolicy toObject()
        {
            return new BasePolicy(this);
        }
    }
}
