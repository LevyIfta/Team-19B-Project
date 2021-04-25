using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer
{
    public class Receipt
    {
        public ShoppingBasket basket { get; set; }
        public Store store { get { return basket.store; } private set {  } }
        public string userName { get; set; }
        public double price { get; set; }
        public DateTime date { get; set; }
        public Object Discount { get; set; } //todo
        public PurchasePolicy purchasePolicy { get; set; } 
    }
}
