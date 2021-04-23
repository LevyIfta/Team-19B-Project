using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.DataLayer
{
    class ReceiptData
    {
        public ShoppingBasketData basket { get; set; }
        public StoreData store => this.basket.store;
        public string username => this.basket.owner.getUsername();
        public double price { get; set; }
        public DateTime date { get; set; }

        public ReceiptData(ShoppingBasketData basket, double price, DateTime date)
        {
            this.basket = basket;
            this.price = price;
            this.date = date;
        }


    }
}
