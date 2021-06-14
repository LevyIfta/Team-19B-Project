using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    public class FeedbackData
    {       
        //FIELDS
        
        [Key, Column(Order=0)]
        public virtual int productID { get; set; }
        public virtual ProductInfoData product { get; set; }
        
        [Key, Column(Order = 1)]
        public virtual string userName { get; set; }
        public virtual MemberData user { get; set; }
        public string manufacturer { get; set; }
        public string comment { get; set; }

        //CONSTRUCTORS
        public FeedbackData(ProductInfoData product, string manufacturer, MemberData user, string comment)
        {
            this.product = product;
            this.productID = product.productID;
            this.manufacturer = manufacturer;
            this.user = user;
            this.userName = user.userName;
            this.comment = comment;
        }

        public FeedbackData()
        {
        }

        //EQUALS
        public override bool Equals(object obj)
        {
            return false;
        }

        public bool Equals(FeedbackData other)
        {
            return this.product.Equals(other.product) & this.manufacturer.Equals(other.manufacturer) & this.manufacturer.Equals(other.manufacturer) & this.user.Equals(other.user) & this.comment.Equals(other.comment);
        }
    }
}
