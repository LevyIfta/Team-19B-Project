using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TradingSystem.DataLayer
{
    public class iPolicyData
    {
        [Key]
        public int id { get; set; }

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