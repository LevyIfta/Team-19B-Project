using System;
using System.Collections.Generic;
using System.Text;
using TradingSystem.BuissnessLayer;
using TradingSystem.BuissnessLayer.commerce;
using TradingSystem.ServiceLayer;
using PaymentSystem;
using SupplySystem;


namespace Tests.Bridge
{
    class ConfigurationFile
    {
        public static void Configure()
        {
            VerificationReal payment = new VerificationReal("https://cs-bgu-wsep.herokuapp.com/");
            SupplyReal supply = new SupplyReal("https://cs-bgu-wsep.herokuapp.com/");
            UserServices.register("admin", "adminPass123", 45, "male", "address1");
            //Connect to database...
        }
    }
}
