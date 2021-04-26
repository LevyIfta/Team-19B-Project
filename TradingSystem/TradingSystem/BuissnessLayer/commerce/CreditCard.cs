using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer.commerce
{
    public class CreditCard : PaymentMethod
    {
        public bool pay(double price) { return true; }
    }
}
