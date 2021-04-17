using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTests.Bridge;

namespace UnitTests
{
    [TestClass]
    public class UserTests
    {
        private static Bridge.Bridge bridge { get; set; }
        [ClassInitialize]
        public static void classInit(TestContext context)
        {
            bridge = Bridge.Driver.GetBridge();
            bridge.register("userAct", "passwordAct");
            bridge.login("userAct", "passwordAct");
        }
        [ClassCleanup]
        public void classCleanup()
        {
            bridge.logout();
        }


        [TestInitialize]
        public void testInit()
        {
            
        }
        [TestCleanup]
        public void testCleanup()
        {
            
        }


        /// <summary>
        /// used for proxy. todo
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        private bool isProper(object product)
        {
            if ((string)product == "good" || (string)product == "good2")
                return true;
            else
                return false;
        }
        [TestMethod]
        public void browseProductsTest()
        {
            object ret = bridge.browseProducts("good", "bamba", 1.1, 2.5, 5.0);
            Assert.IsTrue(isProper(ret), "failed to retrive product");
            ret = bridge.browseProducts("bad", "nope", 1.1, 2, 1);
            Assert.IsFalse(isProper(ret), "managed to retrive bad product");
            ret = bridge.browseProducts("good2", "__&^#", 3, -10, 9999);
            Assert.IsFalse(isProper(ret), "managed to retrive bad product (should fail)");

        }

        [TestMethod]
        public void browseStoreTest()
        {
            Assert.IsTrue(isProper(bridge.browseStore("good")), "failed to browse store");
            Assert.IsFalse(isProper(bridge.browseStore("bad")), "managed to browsr bad store ");
        }

        [TestMethod]
        public void saveProductTest()
        {
            //todo
            Assert.Fail();
        }

        [TestMethod]
        public void getPurchaseHistoryTest()
        {
            //todo
            Assert.Fail();
        }

        [TestMethod]
        public void getStoreAndProductsInfoTest()
        {
            //todo
            Assert.Fail();
        }

        [TestMethod]
        public void removeProductsFromBasketTest()
        {
            //todo
            Assert.Fail();
        }








    }
}
