using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Tests.Bridge;
using TradingSystem.BuissnessLayer;
using TradingSystem.BuissnessLayer.commerce;
using TradingSystem.ServiceLayer;

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
            bridge.register("Owner", "Pass1234");
            bridge.login("Owner", "Pass1234");
            bridge.openStore("Store1");
            ProductInfo newInfo1 = ProductInfo.getProductInfo("item1", "food", "manu1"); //new ProductInfo("item1", "food", "manu1");
            ProductInfo newInfo2 = ProductInfo.getProductInfo("item2", "food", "manu1");
            Product items1 = new Product(newInfo1, 10, 10.9), items2 = new Product(newInfo2, 20, 4.9);
            Store store1 = bridge.getStore("Store1");
            ShoppingBasket basket = new ShoppingBasket(store1, (Member)bridge.getUser());
            basket.products.Add(items1);
            basket.products.Add(items2);
            bridge.addInventory(basket);
            bridge.logout();
        }
        [TestMethod]
        public void TestAll()
        {
            TestShopMenagement();
            PurchaseTestGood();
            PurchaseTestBad();
        }

        public void TestShopMenagement()
        {
            Assert.IsFalse(bridge.isUserLoggedIn("Owner"), "logout failed");
            Assert.IsTrue(bridge.isStoreExist("Store1"), "store was not created");
            Assert.IsTrue(bridge.isItemAtStore("Store1", "item1", "manu1"), "failed loading products to inventory");
            Assert.IsTrue(bridge.isItemAtStore("Store1", "item2", "manu1"), "failed loading products to inventory");
            Assert.IsTrue(bridge.getProductAmount("Store1", "item1", "manu1") == 10, "did not add correct amount of the item");
            Assert.IsTrue(bridge.getProductAmount("Store1", "item2", "manu1") == 20, "did not add correct amount of the item");
        }

        //register user with valid details
        //logs in to the user's account
        //adds a product to the user's basket and purchases it
        public void PurchaseTestGood()
        {
            //Member member2 = new Member("regular", "justsomemember12");
            bridge.register("Member1", "Pass2345");
            bridge.login("Member1", "Pass2345");
            Dictionary<string, int> d = new Dictionary<string, int>();
            d["item1"] = 10;
            d["item2"] = 10;
            bridge.saveProducts(bridge.getUserName(), "Store1", "manu1", d);
            double amount = bridge.checkPrice(bridge.getUserName());
            string[] ans = bridge.purchase("111111111111", "11/22", "123");
            Store store1 = bridge.getStore("Store1");
            bridge.logout();
            //Store store2 = bridge.getStore("Store1");
            Assert.IsNotNull(ans, "empty receipt");
            if(ans != null)
            {
                Assert.IsTrue(ans[0].Equals("true"), "error in purchse");
                if(ans[0].Equals("true"))
                {
                    string[] arr = ans[1].Split('$');
                    Assert.IsTrue(arr[1].Equals("Store1"), "different store");
                    Assert.IsTrue(arr[2].Equals(amount + ""), "different amount");
                }
                
            }
            Assert.IsFalse(bridge.isUserLoggedIn("Member1"), "user should not be logged in");
            Assert.IsTrue(bridge.getProductAmount("Store1", "item2", "manu1") == 10, "did not remove correct amount of items from inventory");
        }

        public void PurchaseTestBad()
        {
            bridge.register("Member2", "Pass2345");
            bridge.login("Member2", "Pass2345");
            Dictionary<string, int> d = new Dictionary<string, int>();
            d["item1"] = 10;
            d["item2"] = 10;
            Store store1 = bridge.getStore("Store1");
            bridge.saveProducts(bridge.getUserName(), "Store1", "manu1", d);
            /*
            ICollection<SLreceipt> ans = bridge.purchase("Immediate");
            bridge.logout();
            Assert.IsTrue(ans == null, "receipt should be empty");
            Assert.IsFalse(bridge.isUserLoggedIn("Member2"), "user should not be logged in");
            Assert.IsTrue(bridge.getProductAmount("Store1", "item2", "manu1") == 10, "should not change the inventory if the purchase is invalid");//should not process the purchase because there are not enough of the product in the store
            */
        }
    }

}
