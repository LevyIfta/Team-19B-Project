using System;
using System.Collections.Generic;
using System.Text;
using TradingSystem.BuissnessLayer;
using TradingSystem.BuissnessLayer.commerce;
using TradingSystem.ServiceLayer;
using TradingSystem.BuissnessLayer.User.Permmisions;

namespace Tests.Bridge
{
    static class initFile
    {
        public static void initializeSystem()
        {
            UserController.register("initUser1", "initPass1", 31, "female", "address1");
            UserController.register("initUser2", "initPass2", 30, "male", "address1");
            UserController.login("initUser1", "initPass1");
            UserController.EstablishStore("initUser1", "initStore1");
            UserController.hireNewStoreManager("initUser1", "initStore1", "initUser2");
            UserController.addNewProduct("initUser1", "initStore1", "initProduct1", 33.33, 15, "initCategory1", "initManufacturer1");
        }
    }
}
