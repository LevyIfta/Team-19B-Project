using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Bridge;
using TradingSystem.BuissnessLayer;
using TradingSystem.BuissnessLayer.commerce;
/*
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
            items1.amount = 10; items1.info = newInfo1;
            items2.amount = 20; items2.info = newInfo2;
            ShoppingBasket basket = new ShoppingBasket();
            basket.products.Add(items1);
            basket.products.Add(items2);
            basket.store = bridge.getStore("Store1");
            bridge.addInventory(basket);
            bridge.logout();
        }

        [TestMethod]
        public void TestShopMenagement()
        {
            Assert.isFalse(isUserLoggedIn("Owner1"), "logout failed");
            Assert.isTrue(isStoreExist("Store1"), "store was not created");
            Assert.isTrue(bridge.isProductAtStore("Store1", "item1"), "failed loading products to inventory");
            Assert.isTrue(bridge.isProductAtStore("Store1", "item2"), "failed loading products to inventory");
            Assert.AreSame(bridge.getProductAmount("Store1", "item1"), 10, "did not add correct amount of the item");
            Assert.AreSame(bridge.getProductAmount("Store1", "item2"), 20, "did not add correct amount of the item");
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
            ProductInfo newInfo1 = new ProductInfo();
            newInfo1.name = "item1";
            Product item1 = new Product();
            item1.info = newInfo1;
            item1.amount = 6;
            basketToBuy.products.Add(item1);
            bridge.purchase(basketToBuy);
            bridge.logout();


            Assert.isFalse(isUserLoggedIn("Member1"), "user should not be logged in");
            Assert.AreSame(getProductAmount("Store1", "item1"), 4, "did not remove correct amount of items from inventory");
        }

        [TestMethod]
        public void PurchaseTestBad()
        {
            bridge.register("Member2", "Pass2345");
            bridge.login("Member2", "Pass2345");
            ShoppingBasket basketToBuy = new ShoppingBasket();
            basket.store = bridge.getStore("Store1");
            ProductInfo newInfo1 = new ProductInfo();
            newInfo1.name = "item1";
            Product item1 = new Product();
            item1.info = newInfo1;
            item1.amount = 12;
            basketToBuy.products.Add(item1);
            bridge.purchase(basketToBuy);
            bridge.logout();
            Assert.isFalse(isUserLoggedIn("Member2"), "user should not be logged in");
            Assert.AreSame(getProductAmount("Store1", "item1"), 4, "should not change the inventory if the purchase is invalid");//should not process the purchase because there are not enough of the product in the store
        }
    }

}
*/