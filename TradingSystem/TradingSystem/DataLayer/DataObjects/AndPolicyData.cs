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
    class AndPolicyData : iPolicyData
    {
        [Key, Column(Order = 0)]
        public Guid id { get; set; }
        [Key, Column(Order = 1)]
        public string storeName { get; set; }

        public AndPolicyData(List<Guid> policies)
        {
            this.policiesData = policies;
        }

        public AndPolicyData() { }

        public override iPolicy toObject()
        {
            return new AndPolicy(this);
        }
    }
}
