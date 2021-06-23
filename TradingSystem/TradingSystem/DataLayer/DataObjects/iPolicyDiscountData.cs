using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TradingSystem.DataLayer
{
    public class iPolicyDiscountData
    {
        [Key]
        public int id { get; set; }


        /*
        public iPolicyDiscountData()
        {
        }

        public iPolicyDiscountData(ICollection<ReceiptData> recipts)
        {
            this.recipts = recipts ?? throw new ArgumentNullException(nameof(recipts));
        }
        public Guid discountid { get; set; } = Guid.NewGuid();
        public ICollection<ReceiptData> recipts { get; set; } = new List<ReceiptData>();*/
    }
}