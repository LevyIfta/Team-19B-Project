using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.User.Permmisions;


namespace TradingSystem.BuissnessLayer
{
    public class Guest : aUser
    {
        private static int guestCount = 0;
        public override string getUserName()
        {
            guestCount++;
            return "guest" + guestCount;
        }
        public override object todo(PersmissionsTypes func, object[] args)
        {
            return null; 
        }
        public override bool EstablishStore(string storeName)
        {
            return false;
        }

        public override bool purchase(PaymentMethod payment)
        { // only Immediate and Offer
            throw new NotImplementedException();
        }



    }
}
