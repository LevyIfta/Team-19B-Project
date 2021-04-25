using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer
{
    public class ShoppingBasket
    {
        public ICollection<Product> products { get; set; }
        public Store store { get; set; }


        public double checkPrice()
        {
            return store.calcPrice(products);
        }

        public Reciept purchase(PaymentMethod payment)
        {
            return store.executePurchase(this.products);
        }
    }
}
