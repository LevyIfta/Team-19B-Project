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
        protected BasketData(ICollection<ProductData> products)
        {
            
            this.products = products;
        }

        protected BasketData()
        {
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
        public BasketInCart(ICollection<ProductData> products) : base(products)
        {
        }

      
        [Key, Column(Order = 0)]
        public virtual string storeName { get; set; }
        public virtual StoreData store { get; set; }

        [Key, Column(Order = 1)]
        public virtual string userName { get; set; }
        public virtual MemberData user { get; set; }


        public BasketInCart():base()
        {
        }

        public BasketInCart(StoreData store, MemberData user, ICollection<ProductData> products) : base(products)
        {
            this.store = store ?? throw new ArgumentNullException(nameof(store));
            this.user = user ?? throw new ArgumentNullException(nameof(user));
            this.storeName = this.store.storeName;
            this.userName = this.user.userName;
        }


        //EQUALS
        public override bool Equals(object obj)
        {
            return false;
        }

        public bool Equals(BasketInCart other)
        {
            return this.userName.Equals(other.userName) && this.storeName.Equals(other.storeName);
        }
    }

    public class BasketInRecipt : BasketData
    {
       
        //[ForeignKey("ReceiptData")]
        public virtual ReceiptData recipt { get; set; }

        [Key]
        public virtual int reciptID { get; set; }


        public BasketInRecipt(ICollection<ProductData> products, ReceiptData recipt) : base(products)
        {
            this.recipt = recipt;
            this.reciptID = recipt.receiptID;
        }

        public BasketInRecipt() :base()
        {
        }

        public BasketInRecipt(ReceiptData recipt, int reciptID, ICollection<ProductData> products) : base(products)
        {
            this.recipt = recipt ?? throw new ArgumentNullException(nameof(recipt));
            this.reciptID = reciptID;
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
