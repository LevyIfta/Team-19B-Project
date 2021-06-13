using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    public abstract class BasketData
    {
        //FIELDS
        

        public virtual ICollection<ProductData> products { get; set; }

        //CONSTRUCTORS
        protected BasketData(ICollection<ProductData> product)
        {
            
            this.products = product;
        }

        //EQUALS
        public override bool Equals(object obj)
        {
            return false;
        }

        public bool Equals(BasketData other)
        {
            return false;
        }
    }

    public class BasketInCart : BasketData
    {
        [Key]
        [Column(Order = 1)]
        public virtual StoreData storeName { get; set; }
        [Key]
        [Column(Order = 2)]
        public virtual MemberData useName { get; }

        public BasketInCart(StoreData storeName, MemberData useName, ICollection<ProductData> product) : base(product)
        {
            this.storeName = storeName;
            this.useName = useName;
        }

        //EQUALS
        public override bool Equals(object obj)
        {
            return false;
        }

        public bool Equals(BasketInCart other)
        {
            return this.useName.Equals(other.useName) && this.storeName.Equals(other.storeName);
        }
    }

    public class BasketInRecipt : BasketData
    {
        [Key]
        public virtual ReceiptData recipt { get; set; }

        public BasketInRecipt(ICollection<ProductData> product, ReceiptData receipt) : base(product)
        {
            this.recipt = recipt;
        }

        //EQUALS
        public override bool Equals(object obj)
        {
            return false;
        }

        public bool Equals(BasketInRecipt other)
        {
            return this.recipt.Equals(other.recipt);
        }
    }
}
