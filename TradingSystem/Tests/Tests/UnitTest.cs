using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using Tests.Bridge;
using TradingSystem.BuissnessLayer;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class UserAccessUnitTest
    {
        private static Bridge.Bridge bridge;
        [ClassInitialize]
        public static void classInit(TestContext context)
        {
            bridge = Driver.getBridge();
            bridge.register("user1", "Password1");
            bridge.register("user2", "Password2");

        }
        [TestMethod]
        public void loginTestGood()
        {
            Assert.AreSame(bridge.getUserName(), "guest", "default user is nt guest");
            Assert.IsTrue(bridge.login("user1", "Password1"));
            Assert.IsTrue(bridge.isUserLoggedIn("user1"), "user is not considerd logged in");
            Assert.AreSame(bridge.getUserName(), "user1", "wrong logged user");

            bridge.logout(); //clean up

        }

        [TestMethod]
        public void loginTestBad()
        {
            Assert.IsFalse(bridge.login("fakeUser", "fakePassword"), "managed to login as fake user");
            Assert.AreEqual<string>(bridge.getUserName(), "guest", "the logged user changed (fake user)");

            Assert.IsFalse(bridge.login("user1", "fakePassword"), "managed to login with wrong password");
            Assert.AreEqual<string>(bridge.getUserName(), "guest", "the logged user changed (wrong password)");

            Assert.IsFalse(bridge.login("fakeUser", "Password1"), "managed to login with fake userbane");
            Assert.AreEqual<string>(bridge.getUserName(), "guest", "the logged user changed (fake username)");
        }


        [TestMethod]
        public void registerTestGood()
        {
            Assert.IsTrue(bridge.register("newUser1", "newPassword1"), "faild to register as a valid user");
            Assert.IsTrue(bridge.isUserExist("newUser1", "newPassword1"), "user was not properly saved");
            Assert.IsFalse(bridge.isUserExist("newUser1", "fakePassword1"), "wrong password found as exist");

            Assert.IsTrue(bridge.register("newUser2", "newPassword1"), "faild to register as a valid user(existing password)");
            Assert.IsTrue(bridge.isUserExist("newUser2", "newPassword1"), "user was not properly saved (existing password)");

        }

        [TestMethod]
        public void registerTestBad()
        {
            Assert.IsFalse(bridge.register("user1", "Password1"), "managed to register as an alread existing user (same password)");
            Assert.IsFalse(bridge.register("user1", "Password2"), "managed to register with an existig username (different passwords)");
            Assert.IsFalse(bridge.isUserExist("user1", "Password2"), "exisint user with different passworrd considerd to exist");

            Assert.IsFalse(bridge.register("badbadBADUSERwithBadWord!@$%$$$$_8", "OKpassword"), "managed to register with bad username");
            Assert.IsFalse(bridge.isUserExist("badbadBADUSERwithBadWord!@$%$$$$_8", "OKpassword"), "invlid user saved (bad username)");

            Assert.IsFalse(bridge.register("okUser", "badpassword^^^#@#$%^"), "managed to register with bad password");
            Assert.IsFalse(bridge.isUserExist("okUser", "badpassword^^^#@#$%^"), "invlid user saved (bad password)");


            Assert.IsFalse(bridge.register("badbadBADUSERwithBadWord!@$%$$$$_8", "badpassword^^^#@#$%^"), "managed to register with bad username and bad password");
            Assert.IsFalse(bridge.isUserExist("badbadBADUSERwithBadWord!@$%$$$$_8", "badpassword^^^#@#$%^"), "invlid user saved (bad username and bad passsword)");
        }

        [TestMethod]
        public void logoutTest()
        {
            Assert.IsTrue(bridge.login("user2", "Password2"), "failed login");
            Assert.AreEqual<string>(bridge.getUserName(), "user2", "failed login (login didnt change the logged user properly)");

            bridge.logout();

            Assert.AreEqual<string>(bridge.getUserName(), "guest", "logout did not change the logged user to guest");

            bridge.logout();
            Assert.AreEqual<string>(bridge.getUserName(), "guest", "2nd logout changed something");

        }

 
    }

    [TestClass]
    public class BasketTests
    {
        private static Bridge.Bridge bridge;

        [ClassInitialize]
        public static void classInit(TestContext context)
        {
            bridge = Driver.getBridge();
            bridge.register("basket1", "Basket1");
            bridge.login("basket1", "Basket1");
            bridge.openStore("basketStore1");
            bridge.openStore("basketStore2");
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
            basket.store = bridge.getStore("basketStore");
            bridge.addProducts(basket);


        }

        [ClassCleanup]
        public static void classCleanUp()
        {
            bridge.logout();    
        }

        [TestMethod]
        public void saveProductTest()
        {
            //setup
            ProductInfo newInfo3 = new ProductInfo();
            newInfo3.name = "item3";
            ProductInfo newInfo4 = new ProductInfo();
            newInfo4.name = "item4";
            Product items3 = new Product(), items4 = new Product();
            items3.amount = 5; items3.info = newInfo3;
            items4.amount = 2; items4.info = newInfo4;

            ShoppingBasket basket = new ShoppingBasket();
            basket.products.Add(items3);
            basket.products.Add(items4);
            basket.store = bridge.getStore("basketStore");
            bridge.addProducts(basket);

            ShoppingBasket saveBasket = bridge.getBasket("basketStore");
            Assert.IsTrue(saveBasket.products.Contains(items3), "failed to save one of the items");
            Assert.IsTrue(saveBasket.products.Contains(items4), "failed to save one of the items");
            Assert.IsFalse(saveBasket.products.Contains(null), "saved a null item");

        }

        [TestMethod]
        public void removeProductTest()
        {
            //setup
            ProductInfo newInfo1 = new ProductInfo();
            newInfo1.name = "item1";
            ProductInfo newInfo2 = new ProductInfo();
            newInfo2.name = "item2";
            Product items1 = new Product(), items2 = new Product();
            items1.amount = 2; items1.info = newInfo1;
            items2.amount = 2; items2.info = newInfo2;

            ShoppingBasket basket = new ShoppingBasket();
            basket.products.Add(items1);
            basket.products.Add(items2);
            basket.store = bridge.getStore("basketStore");
            bridge.removeProducts(basket);

            ShoppingBasket saveBasket = bridge.getBasket("basketStore");
            Assert.IsTrue(saveBasket.products.Contains(items1), "removed an item with non-0 amount");
            Assert.AreSame(bridge.getProductAmount(saveBasket, newInfo1), 3, "didnt change the amount properly");
            Assert.IsFalse(saveBasket.products.Contains(items2), "failed to delete an item with amount of 0");
            Assert.IsFalse(saveBasket.products.Contains(null), "saved a null item");


            //bad
            bridge.removeProducts(basket); //2nd removal
            Assert.IsTrue(saveBasket.products.Contains(items1), "illigal removal worked");
            Assert.AreSame(bridge.getProductAmount(saveBasket, newInfo1), 3, "illigal removal worked");
            Assert.IsFalse(saveBasket.products.Contains(items2), "illigal removal worked");
            Assert.IsFalse(saveBasket.products.Contains(null), "saved a null item (illgal removal)");

        }







    }


    [TestClass]
    public class StoreTests
    {
        private static ProductInfo prod1;
        private static ProductInfo prod2;
        private static Bridge.Bridge bridge;
        [ClassInitialize]
        public static void classInit(TestContext context)
        {
            bridge = Driver.getBridge();
            bridge.register("store1", "Store1");
            bridge.register("store2", "Store2");
            bridge.login("store1", "Store1");
            bridge.openStore("Store1");
            ProductInfo newInfo1 = new ProductInfo();
            newInfo1.name = "item1";
            ProductInfo newInfo2 = new ProductInfo();
            newInfo2.name = "item2";

            prod1 = newInfo1;
            prod2 = newInfo2;

            Product items1 = new Product(), items2 = new Product();
            items1.amount = 2; items1.info = newInfo1;
            items2.amount = 2; items2.info = newInfo2;
            ShoppingBasket basket = new ShoppingBasket();
            basket.store = bridge.getStore("Store1");
            basket.products.Add(items1);
            basket.products.Add(items2);
  
            bridge.addInventory(basket);



        }

        [ClassCleanup]
        public static void classCleanUp()
        {
            bridge.logout();
        }


        [TestMethod]
        public void createStoreTestGood()
        {
            Store store = bridge.getStore("store2");
            Assert.AreSame(store, null, "store found before  being founded");
            bridge.openStore("store2");
            store = bridge.getStore("store2");

            Assert.AreSame(store.name, "store2", "store has wrong name");
        }
        [TestMethod]
        public void createStoreTestBad()
        {
            bridge.logout();
            bridge.login("store2", "Store2");
            Assert.IsFalse(bridge.openStore("store1"), "manage to open a tore with an existing name");

            Store store = bridge.getStore("store1");
            Assert.AreNotEqual(store.founder.getUserName(), "store2", "store founder changed");
            Assert.AreEqual(store.founder.getUserName(), "store1", "store founder changed");

            bridge.logout();
            bridge.login("store1", "Store1");
        }

        [TestMethod]
        public void addInventoryTest()
        {
            //setup
            ProductInfo newInfo3 = new ProductInfo();
            newInfo3.name = "item3";
            ProductInfo newInfo4 = new ProductInfo();
            newInfo4.name = "item4";
            Product items3 = new Product(), items4 = new Product();
            items3.amount = 5; items3.info = newInfo3;
            items4.amount = 2; items4.info = newInfo4;

            ShoppingBasket basket = new ShoppingBasket();
            basket.products.Add(items3);
            basket.products.Add(items4);
            Store store = bridge.getStore("store1");
            basket.store = store;
            ICollection<Product> inventory = store.inventory;
            int count = inventory.Count;
            bridge.addInventory(basket);

          
            
            Assert.IsTrue(inventory.Contains(items3), "failed to save one of the items");
            Assert.IsTrue(inventory.Contains(items4), "failed to save one of the items");
            Assert.IsFalse(inventory.Contains(null), "saved a null item");
            Assert.AreSame(inventory.Count, count+2, "count update wrong");
            foreach (Product item in store.inventory)
            {
                if (item.info.Equals(newInfo3))
                    Assert.AreEqual(item.amount, 5, "failed to update the item amount properly");
                if (item.info.Equals(newInfo4))
                    Assert.AreEqual(item.amount, 2, "failed to update the item amount properly");
            }

            count += 2;
            bridge.addInventory(basket);
            Assert.AreSame(inventory.Count, count, "count update wrong(add more of exising product");
            foreach (Product item in store.inventory)
            {
                if (item.info.Equals(newInfo3))
                    Assert.AreEqual(item.amount, 10, "failed to update the item amount properly(2nd add)");
                if (item.info.Equals(newInfo4))
                    Assert.AreEqual(item.amount, 4, "failed to update the item amount properly(2nd add)");
            }


        }

        [TestMethod]
        public void removeInventoryTest()
        {
            //setup

            Product items1 = new Product(), items2 = new Product();
            items1.amount = 1; items1.info = prod1;
            items2.amount = 2; items2.info = prod2;

            ShoppingBasket basket = new ShoppingBasket();
            basket.products.Add(items1);
            basket.products.Add(items2);
            Store store = bridge.getStore("store1");
            basket.store = store;
            ICollection<Product> inventory = store.inventory;
            int count = inventory.Count;
            bridge.removeInventory(basket);



            Assert.IsTrue(inventory.Contains(items1), "removed an item with remaining amount");
            Assert.IsFalse(inventory.Contains(items2), "kept an item with 0 amount");
            Assert.IsFalse(inventory.Contains(null), "saved a null item");
            Assert.AreSame(inventory.Count, count - 1, "count update wrong");
            foreach (Product item in store.inventory)
            {
                if (item.info.Equals(prod1))
                    Assert.AreEqual(item.amount, 1, "failed to update the item amount properly");

            }
       

        }

    }

    [TestClass]
    class ReciptTests
    {
        private static Bridge.Bridge bridge;
        [ClassInitialize]
        public static void classInit(TestContext context)
        {
            bridge = Driver.getBridge();
            bridge.register("recipt1", "Recipt1");
            bridge.register("recipt2", "Recipt2");
            bridge.login("recipt2", "Recipt2");
            bridge.openStore("StoreRecipt1");
            bridge.openStore("StoreRecipt2");
            ProductInfo newInfo1 = new ProductInfo();
            newInfo1.name = "item1";
            ProductInfo newInfo2 = new ProductInfo();
            newInfo2.name = "item2";
            
            Product items1 = new Product(), items2 = new Product();
            items1.amount = 2; items1.info = newInfo1;
            items2.amount = 2; items2.info = newInfo2;
            ShoppingBasket basket = new ShoppingBasket();
            basket.store = bridge.getStore("StoreRecipt1");
            basket.products.Add(items1);
            basket.products.Add(items2);

            bridge.addInventory(basket);
            bridge.logout();
            bridge.login("recipt1", "Recipt1");
            bridge.addProducts(basket);
            bridge.purchase();


        }

        [ClassCleanup]
        public static void classCleanUp()
        {
            bridge.logout();
        }

        [TestMethod]
        public void userReciptTestGood()
        {
            Reciept reciept = bridge.GetRecieptByUser("StoreRecipt1", "recipt1", new System.DateTime());
            Assert.AreEqual(reciept.userName, "recipt1", "the username is wrong");
            Assert.AreEqual(reciept.store.name, "StoreRecipt1", "the store name is wrong");
            Assert.AreEqual(reciept.basket.products.Count, 2, "saved wrong product list");
        }


        [TestMethod]
        public void userReciptTestBad()
        {
            bridge.logout();
            bridge.login("recipt2", "Recipt2");
            
            Reciept reciept = bridge.GetRecieptByUser("StoreRecipt1", "recipt2", new System.DateTime());
            Assert.IsNull(reciept, "managed to get recpit from wong user");
            //clean up
            bridge.logout();
            bridge.login("recipt1", "Recipt1");

        }

        [TestMethod]
        public void storeReciptTestGood()
        {
            bridge.logout();
            bridge.login("recipt2", "Recipt2");

            Reciept reciept = bridge.GetRecieptByStore("StoreRecipt1", "recipt1", new System.DateTime());
            Assert.AreEqual(reciept.userName, "recipt1", "the username is wrong");
            Assert.AreEqual(reciept.store.name, "StoreRecipt1", "the store name is wrong");
            Assert.AreEqual(reciept.basket.products.Count, 2, "saved wrong product list");
            //clean up
            bridge.logout();
            bridge.login("recipt1", "Recipt1");

        }


        [TestMethod]
        public void storeReciptTestBad()
        {
            bridge.logout();
            bridge.login("recipt2", "Recipt2");


            Reciept reciept = bridge.GetRecieptByUser("StoreRecipt2", "recipt1", new System.DateTime());
            Assert.IsNull(reciept, "managed to get recpit from wrong store");
            //clean up
            bridge.logout();
            bridge.login("recipt1", "Recipt1");

        }

    }

}