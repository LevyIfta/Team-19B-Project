using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    class FeedbackData
    {       
        //FIELDS
        public string productName { get; set; }
        public string manufacturer { get; set; }
        public string userName { get; set; }
        public string comment { get; set; }

        //CONSTRUCTORS
        public FeedbackData(string productName, string manufacturer, string userName, string comment)
        {
            this.productName = productName;
            this.manufacturer = manufacturer;
            this.userName = userName;
            this.comment = comment;
        }

        //EQUALS
        public override bool Equals(object obj)
        {
            return false;
        }

        public bool Equals(FeedbackData other)
        {
            return this.productName.Equals(other.productName) & this.manufacturer.Equals(other.manufacturer) & this.manufacturer.Equals(other.manufacturer) & this.userName.Equals(other.userName) & this.comment.Equals(other.comment);
        }
    }
}
