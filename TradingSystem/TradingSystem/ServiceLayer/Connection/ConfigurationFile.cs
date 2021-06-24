using System;
using System.Collections.Generic;
using System.Text;
using TradingSystem.BuissnessLayer;
using TradingSystem.BuissnessLayer.commerce;
using TradingSystem.ServiceLayer;
using PaymentSystem;
using SupplySystem;

namespace TradingSystem.ServiceLayer.Connection
{
    class ConfigurationFile
    {
        public static VerificationReal payment;
        public static SupplyReal supply;
        public static string DBName = "TradingSystem.db";
        public static void Configure()
        {
            payment = new VerificationReal("https://cs-bgu-wsep.herokuapp.com/");
            supply = new SupplyReal("https://cs-bgu-wsep.herokuapp.com/");
            UserServices.register("admin", "adminPass123", 45, "male", "address1");
            DBName = "TradingSystem.db";
        }
    }
}
