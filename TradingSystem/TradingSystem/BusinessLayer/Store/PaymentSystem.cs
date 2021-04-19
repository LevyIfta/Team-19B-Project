using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem
{
    public class PaymentSystem
    {
        public static bool pay(CreditCardInfo creditCard, double price, string storeOwner)
        {
            DirAppend.AddToLogger.AddToLogger("payment - from: " + creditCard.holderName + ", to: " + storeOwner + ", total: " + price + ".", "log");
            return true;
        }
    }
}
