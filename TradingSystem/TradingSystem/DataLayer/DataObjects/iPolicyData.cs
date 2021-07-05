using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TradingSystem.BuissnessLayer;
using TradingSystem.BuissnessLayer.commerce;
using TradingSystem.BuissnessLayer.commerce.Rules;

namespace TradingSystem.DataLayer
{
    public abstract class iPolicyData
    {
        /*
        [Key, Column(Order =0)]
        public Guid id { get; set; }
        [Key, Column(Order = 1)]
        public string storeName { get; set; }*/

        [NotMapped]
        public List<Guid> policiesData { get; set; }
        public abstract iPolicy toObject();
        /*
        public iPolicyData(Guid id, List<iPolicyData> policiesData)
        {
            this.id = id;
            this.policiesData = policiesData;
        }*/
        /*
        public ICollection<ReceiptData> recipts { get; set; } = new List<ReceiptData>();
        public Guid policyid { get; set; } = Guid.NewGuid();
        public iPolicyData()
        {

        }

        public iPolicyData(ICollection<ReceiptData> recipts)
        {
            this.recipts = recipts ?? throw new ArgumentNullException(nameof(recipts));
        }*/
    }
}