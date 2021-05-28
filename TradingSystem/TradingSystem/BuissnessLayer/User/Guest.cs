using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.User.Permmisions;
using TradingSystem.BuissnessLayer.commerce;


namespace TradingSystem.BuissnessLayer
{
    public class Guest : aUser
    {
        private static int guestCount = 0;
        public override string getUserName()
        {
            guestCount++;
            return "guest";//+ guestCount;
        }
        public override object todo(PersmissionsTypes func, object[] args)
        {
            return null; 
        }
        public override bool EstablishStore(string storeName)
        {
            return false;
        }

        public override ICollection<Receipt> purchase(PaymentMethod payment)
        { // only Immediate and Offer
            ICollection<Receipt> list = new List<Receipt>();
            foreach (ShoppingBasket basket in getMyCart().baskets)
            {
                Receipt receipt = null; // basket.store.executePurchase(basket, payment);
                if (receipt == null)
                    return null;
            }
            return list;
        }



    }
}
