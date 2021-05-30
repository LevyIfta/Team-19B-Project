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

        public override string[] purchase(string creditNumber, string validity, string cvv)
        { // only Immediate and Offer
            ICollection<string> list = new List<string>();
            foreach (ShoppingBasket basket in getMyCart().baskets)
            {
                string[] ans = basket.store.executePurchase(basket, creditNumber, validity, cvv);
                if (ans == null || ans[0].Equals("false"))
                    return null;
                list.Add(ans[1]);
            }
            string[] arr = new string[list.Count];
            int i = 0;
            foreach (string receipt in list)
            {
                arr[i] = receipt;
                i++;
            }
            return arr;
        }



    }
}
