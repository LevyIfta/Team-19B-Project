using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Bridge;
using TradingSystem.BuissnessLayer;

namespace Tests
{
    [TestClass]
    public class AcceptanceTest
    {
        private static Bridge.Bridge bridge;
        [ClassInitialize]
        public static void classInit(TestContext context)
        {
            bridge = Driver.getBridge();
            bridge.register("Owner1", "Pass1234");
            bridge.login("Owner1", "Pass1234");
            bridge.openStore("Store1");
            ProductInfo newInfo1 = new ProductInfo();
            newInfo1.name = "item1";
            ProductInfo newInfo2 = new ProductInfo();
            newInfo2.name = "item2";
            Product items1 = new Product(), items2 = new Product();
            items1.amount = 5; items1.info = newInfo1;
            items2.amount = 2; items2.info = newInfo2;
            ShoppingBasket basket = new ShoppingBasket();
            basket.products.Add(items1);
            basket.products.Add(items2);
            basket.store = bridge.getStore("Store1");
            bridge.addInventory(basket);
            bridge.logout();
        }
        
        //register user with valid details
        //logs in to the user's account
        //adds a product to the user's basket and purchases it
        [TestMethod]
        public void PurchaseTestGood()
        {
            bridge.register("Member1", "Pass2345");
            bridge.login("Member1", "Pass2345");
            ShoppingBasket basketToBuy = new ShoppingBasket();
            basket.store = bridge.getStore("Store1");
            basketToBuy.products.Add(items1)
        }
    }

}
