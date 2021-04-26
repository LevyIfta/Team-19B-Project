using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSystem.BuissnessLayer
{
    public interface PaymentMethod
    {
        bool pay(double price);
    }
}
