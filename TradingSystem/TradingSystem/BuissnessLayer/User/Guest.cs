using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer.User.Permmisions;


namespace TradingSystem.BuissnessLayer
{
    class Guest : aUser
    {
        // add static count for the name of the guest
        public override string getUserName()
        {
            return "guest";
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
