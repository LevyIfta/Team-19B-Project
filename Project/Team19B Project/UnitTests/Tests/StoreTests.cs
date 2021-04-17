using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            bridge.hireNewManager(null);//todo hire manager to store 3
            bridge.logout();

        }
        [ClassCleanup]
        public void classCleanup()
        {

        }


        [TestInitialize]
        public void testInit()
        {

        }
        [TestCleanup]
        public void testCleanup()
        {
            bridge.logout();
        }

        [TestMethod]
        public void addProductTest()
        {
            //todo
            Assert.Fail();
        }

        [TestMethod]
        public void basketPriceTest()
        {
            //todo
            Assert.Fail();
        }

        [TestMethod]
        public void productsInfoTests()
        {
            //todo
            Assert.Fail();
        }

        [TestMethod]
        public void storeTests()
        {
            //todo
            Assert.Fail();
        }

        [TestMethod]
        public void purchaseTest()
        {
            //todo
            Assert.Fail();
        }
    }
}
