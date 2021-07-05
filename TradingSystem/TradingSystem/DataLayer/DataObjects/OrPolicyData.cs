using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.commerce.Rules;

namespace TradingSystem.DataLayer
{
    public class OrPolicyData : iPolicyData
    {
        [Key, Column(Order = 0)]
        public Guid id { get; set; }
        [Key, Column(Order = 1)]
        public string storeName { get; set; }

        public OrPolicyData(List<Guid> policies)
        {
            this.policiesData = policies;
        }

        public OrPolicyData() { }

        public override iPolicy toObject()
        {
            return new OrPolicy(this);
        }
    }
}
