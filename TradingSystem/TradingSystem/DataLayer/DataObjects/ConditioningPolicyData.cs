using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.commerce;
using TradingSystem.BuissnessLayer.commerce.Rules;
using TradingSystem.BuissnessLayer.commerce.Rules.Policy;

namespace TradingSystem.DataLayer
{
    public class ConditioningPolicyData : iPolicyData
    {
        [Key, Column(Order = 0)]
        public Guid id { get; set; }
        [Key, Column(Order = 1)]
        public string storeName { get; set; }

        public Func<Product, bool> isRelevant;
        public bool Default;

        public ConditioningPolicyData(List<Guid> policies, bool Default)
        {
            this.policiesData = policies;
            this.Default = Default;
        }

        public ConditioningPolicyData() { }

        public override iPolicy toObject()
        {
            return new ConditioningPolicy(this);
        }
    }
}
