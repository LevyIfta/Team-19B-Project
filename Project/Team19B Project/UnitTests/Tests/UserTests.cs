using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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
            bridge.register("userAct", "passwordAct0");
            bridge.login("userAct", "passwordAct0");
            bridge.addStore("store");
            bridge.createNewItem("bamba", "n", "b", "dd");
            bridge.addProduct(1, 1.1, 5, bridge.browseStore("store"));


        }
        [ClassCleanup]
        public static void classCleanup()
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

        /*
        [TestMethod]
        public void browseProductsTest()
        {
            
            
            Dictionary<string, Dictionary<int, int>> ret = bridge.browseProducts("good", "bamba", 1.1, 2.5, "honda");
            Assert.IsTrue(ret != null, "failed to retrive product");
            Assert.IsTrue(ret.Count > 0, "failed to retrive product");
            ret = bridge.browseProducts("bad", "nope", 1.1, 2, "nisan");

            Assert.IsFalse(ret != null && ret.Count > 0, "managed to retrive bad product");
            ret = bridge.browseProducts("good2", "__&^#", 3, -10, "poopfactory");
            Assert.IsFalse(ret != null && ret.Count > 0, "managed to retrive bad product (should fail)");
            
        }*/


        [TestMethod]
        public void saveProductTest()
        {
            Dictionary<int, int> dir = new Dictionary<int, int>();
            dir.Add(1, 1);
            dir.Add(2, 2);
            Assert.IsTrue(bridge.saveProduct("store", dir), "failed to save items to basket");
            Assert.IsTrue(bridge.GetBasket("store")[1] == 1, "failed to retrive basket");
            Dictionary<int, int> dir2 = new Dictionary<int, int>();
            dir2.Add(1, 1);
            Assert.IsTrue(bridge.removeProductsFromBasket(dir2, "store"), "failed to remove product from basket");
            Assert.IsFalse(bridge.GetBasket("store").ContainsKey(1), "remove didnt remove the product");



        }








    }
}
