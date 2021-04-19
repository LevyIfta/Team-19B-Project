using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradingSystem;
using UnitTests.Bridge;

namespace UnitTests
{
    [TestClass]
    public class StoreTests
    {

        private static Bridge.Bridge bridge { get; set; }
        [ClassInitialize]
        public static void classInit(TestContext context)
        {
            bridge = Bridge.Driver.GetBridge();
            bridge.register("ownerST", "ownerPassST");
            bridge.register("managerST", "managerPassST");


            bridge.login("ownerST", "ownerPassST");
            bridge.addStore("store3");
            bridge.addStore("store4");

            bridge.hireNewManager("store3", "managerST");//todo hire manager to store 3
            bridge.logout();

        }
        [ClassCleanup]
        public static void classCleanup()
        {
            return;
        }


        [TestInitialize]
        public void testInit()
        {
            return;
        }
        [TestCleanup]
        public void testCleanup()
        {
            bridge.logout();
        }

        [TestMethod]
        public void productsInfoTests()
        {
            bridge.login("ownerST", "ownerPassST");
            Store store = bridge.browseStore("store3");
            Store store4 = bridge.browseStore("store4");
            Assert.IsTrue(bridge.createNewItem("rum", "booze", "booze", "boozemaker"), "failed to creater a new product");
            Assert.IsTrue(bridge.addProduct(1, 10, 20, store), "failed to addd product to store");
            Assert.IsTrue(bridge.updateProduct(1, 10, 15, store), "failed to update product at store");
            Assert.IsFalse(bridge.removeProduct(1, store4), "managed to remove product from a store it doesnt exist in");
            Assert.IsTrue(bridge.removeProduct(1, store), "failed to remove product from store");
            
            
        }

        [TestMethod]
        public void storeTests()
        {
            bridge.login("ownerST", "ownerPassST");
            Assert.IsTrue(bridge.addStore("storefortest"), "failed to creadt store");
            Assert.IsTrue(bridge.browseStore("storefortest") != null, "failed to find store");
        }

        [TestMethod]
        public void purchaseTest()
        {
            //todo
            Assert.Fail();
        }
    }
}
