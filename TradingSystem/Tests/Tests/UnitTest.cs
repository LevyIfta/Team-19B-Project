using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using Tests.Bridge;
using TradingSystem.BuissnessLayer;
using System.Linq;
using TradingSystem.BuissnessLayer.commerce;

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
            bridge.logout();

            Assert.AreEqual(bridge.getUserName(), "guest", "default user is nt guest");
            Assert.IsTrue(bridge.login("user1", "Password1"));
            Assert.IsTrue(bridge.isUserLoggedIn("user1"), "user is not considerd logged in");
            Assert.AreEqual(bridge.getUserName(), "user1", "wrong logged user");

            bridge.logout(); //clean up

        }

        [TestMethod]
        public void loginTestBad()
        {
            // init usernames and passes
            string username1 = "AbD1", username2 = "QwEr2", fake1 = "fake1", fake2 = "fake2";
            string pass1 = "123Xx456", pass2 = "465Ss789", fakePass1 = "111", fakePass2 = "789";
            // register
            UserServices.register(username1, pass1);
            UserServices.register(username2, pass2);
            // try to login twice
            aUser u1 = UserServices.login(username1, pass1);
            Assert.IsNull(UserServices.login(username1, pass1));
            // try to login with fake user
            Assert.IsNull(UserServices.login(fake1, fakePass1), "managed to login as fake user");
            // fake pass
            Assert.IsNull(UserServices.login(username2, fakePass2), "managed to login with wrong password");

            Assert.IsNull(UserServices.login(fake1, pass1), "managed to login with fake username");
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

            Assert.IsFalse(bridge.register("okUser", "badpassword^^^^^^^^^^^^^^^^^^^^^6^^#@#$%^"), "managed to register with bad password");
            Assert.IsFalse(bridge.isUserExist("okUser", "badpassword^^^^^^^^^^^^^^^^^^^^^6^^#@#$%^"), "invlid user saved (bad password)");


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

        [ClassCleanup]
        public static void classCleanUp()
        {
            bridge.logout();
        }


    }

    [TestClass]
    public class BasketTests
    {
        private static Bridge.Bridge bridge;
        private static ProductInfo prod1, prod2;


        [ClassInitialize]
        public static void classInit(TestContext context)
        {
            bridge = Driver.getBridge();
            bridge.register("basket1", "Basket1");
            bridge.login("basket1", "Basket1");
            bridge.openStore("basketStore1");
            bridge.openStore("basketStore2");
            ProductInfo newInfo1 = ProductInfo.getProductInfo("item1", "cat", "man");
            prod1 = newInfo1;
            ProductInfo newInfo2 =  ProductInfo.getProductInfo("item2", "ca1", "man1");
            prod2 = newInfo2;

            Product items1 = new Product(newInfo1, 2, 5), items2 = new Product(newInfo2, 2, 5);


            ShoppingBasket basket = new ShoppingBasket(bridge.getStore("basketStore1"), (Member)bridge.getUser());
            basket.products.Add(items1);
            basket.products.Add(items2);
            bridge.addProducts(basket);

            bridge.logout();
        }

        [ClassCleanup]
        public static void classCleanUp()
        {
            bridge.logout();    
        }

        [TestMethod]
        public void saveProductTest()
        {
            string username = "AlIbAd";
            string pass = "123xX4";
            string storeName = "new_store_1";

            UserServices.register(username, pass);
            aUser user = UserServices.login(username, pass);
            // open a new store
            Stores.addStore(storeName, (Member)user);
            //setup
            ProductInfo newInfo3 =  ProductInfo.getProductInfo("item3", "cat", "man");

            ProductInfo newInfo4 =  ProductInfo.getProductInfo("item4", "cat", "man");

            Product items3 = new Product(newInfo3, 5, 5), items4 = new Product(newInfo4, 2, 5);


            ShoppingBasket basket = new ShoppingBasket(Stores.searchStore(storeName), (Member)user);
            basket.addProduct(items3);
            basket.addProduct(items4);
            
            Assert.IsTrue(basket.products.Contains(items3), "failed to save one of the items");
            Assert.IsTrue(basket.products.Contains(items4), "failed to save one of the items");
            Assert.IsFalse(basket.products.Contains(null), "saved a null item");
            //bridge.logout();
        }

        /*
        [TestMethod]
        public void removeProductTest()
        {
            //setup
   
            Product items1 = new Product(prod1, 1, 5), items2 = new Product(prod2, 2, 5);
            
            ShoppingBasket basket = new ShoppingBasket(bridge.getStore("basketStore1"), (Member)bridge.getUser());
            basket.products.Add(items1);
            basket.products.Add(items2);
            
            bridge.removeProducts(basket);

            ShoppingBasket saveBasket = bridge.getBasket("basketStore");
            Assert.IsTrue(saveBasket.products.Contains(items1), "removed an item with non-0 amount");
            Assert.AreSame(bridge.getProductAmount(saveBasket, prod1), 1, "didnt change the amount properly");
            Assert.IsFalse(saveBasket.products.Contains(items2), "failed to delete an item with amount of 0");
            Assert.IsFalse(saveBasket.products.Contains(null), "saved a null item");


            //bad
            bridge.removeProducts(basket); //2nd removal
            Assert.IsTrue(saveBasket.products.Contains(items1), "illigal removal worked");
            Assert.AreSame(bridge.getProductAmount(saveBasket, prod1), 1, "illigal removal worked");
            Assert.IsFalse(saveBasket.products.Contains(items2), "illigal removal worked");
            Assert.IsFalse(saveBasket.products.Contains(null), "saved a null item (illgal removal)");

        }
        */

    }


    [TestClass]
    public class StoreTests
    {
        private static ProductInfo prod1;
        private static ProductInfo prod2;
        private static Bridge.Bridge bridge;
        private static ProductInfo product1;
        private static ProductInfo product2;
        private static string store1Name;
        private static string store2Name;
        [ClassInitialize]
        public static void classInit(TestContext context)
        {
            bridge = Driver.getBridge();
            bridge.register("store1", "Store1");
            bridge.register("store2", "Store2");
            bridge.login("store1", "Store1");
            bridge.openStore("Store1");

            store1Name = "store1_";
            store2Name = "store2_";
            bridge.openStore(store1Name);
            bridge.openStore(store2Name);
            // add some produts
            product1 = ProductInfo.getProductInfo("Batman Costume", "clothing", "FairytaleLand");
            product2 = ProductInfo.getProductInfo("Wireless keyboard", "computer accessories", "Logitech");

            ProductInfo newInfo1 = ProductInfo.getProductInfo("item1", "cat", "man");

            ProductInfo newInfo2 = ProductInfo.getProductInfo("item2", "cat2", "man2");


            prod1 = newInfo1;
            prod2 = newInfo2;


            Product items1 = new Product(prod1, 2, 5), items2 = new Product(prod2, 2, 5);


            ShoppingBasket basket = new ShoppingBasket(bridge.getStore("Store1"), (Member)bridge.getUser());
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
            string storeOwnerName = "StoreOwner_", storeOwnerPass = "123Xx456";
            string storeName = null;
            bool ownerReg = UserServices.register(storeOwnerName, storeOwnerPass);
            aUser storeOwner = UserServices.login(storeOwnerName, storeOwnerPass);
            // try to create a store  with an empty name
            Assert.IsFalse(Stores.addStore(storeName, (Member)storeOwner), "managed to create a store with a null name");
            Assert.IsNull(Stores.searchStore(storeName), "found a store with a null name");
        }
        [TestMethod]
        public void createStoreTestBad()
        {
            string storeOwnerName = "StoreOwner", storeOwnerPass = "123Xx456";
            string storeName = "createStoreTestBad_store1";
            bool ownerReg = UserServices.register(storeOwnerName, storeOwnerPass);
            aUser storeOwner = UserServices.login(storeOwnerName, storeOwnerPass);
            Stores.addStore(storeName, (Member)storeOwner);
            Store store = Stores.searchStore(storeName);

            Assert.IsFalse(Stores.addStore(storeName, (Member)storeOwner), "manage to open a store with an existing name");
            Assert.AreEqual(store.founder.getUserName(), storeOwnerName, "store founder changed");
        }

        [TestMethod]
        public void searchStoreTest()
        {
            string storeOwnerName = "StoreOwner_1", storeOwnerPass = "123Xx456";
            string storeName = "searchStoreTestGood_store1";
            bool ownerReg = UserServices.register(storeOwnerName, storeOwnerPass);
            aUser storeOwner = UserServices.login(storeOwnerName, storeOwnerPass);
            Stores.addStore(storeName, (Member)storeOwner);
            Store store = Stores.searchStore(storeName);

            Assert.IsNotNull(store, "could not find an existing store: " + storeName);
            Assert.IsNull(Stores.searchStore("this store does not exist"));
        }
        
        /*
        [TestMethod]
        public void addInventoryTest()
        {
            string storeOwnerName = "StoreOwner", storeOwnerPass = "123Xx456";
            bool ownerReg = UserServices.register(storeOwnerName, storeOwnerPass);
            aUser storeOwner = UserServices.login(storeOwnerName, storeOwnerPass);
            Stores.addStore("store1", (Member)storeOwner);
            Store store = Stores.searchStore("store1");

            //setup
            ProductInfo newInfo3 = ProductInfo.getProductInfo("item3", "cat", "manInv");
            ProductInfo newInfo4 = ProductInfo.getProductInfo("item4", "cat", "manInv");

            Product items3 = new Product(newInfo3, 5, 5), items4 = new Product(newInfo4, 2, 5);
            
            ShoppingBasket basket = new ShoppingBasket(store, (Member)bridge.getUser());
            basket.products.Add(items3);
            basket.products.Add(items4);

            ICollection<Product> inventory = store.inventory;
            bridge.addInventory(basket);
            int count = inventory.Count;
            Assert.IsTrue(bridge.isProductExist("item3", "manInv"), "new product doesnt exist");


            Assert.IsTrue(inventory.Contains(items3), "failed to save one of the items");
            Assert.IsTrue(inventory.Contains(items4), "failed to save one of the items");
            Assert.IsFalse(inventory.Contains(null), "saved a null item");
            //Assert.AreSame(inventory.Count, count + 2, "count update wrong");
            foreach (Product item in store.inventory)
            {
                if (item.info.Equals(newInfo3))
                    Assert.AreEqual(item.amount, 5, "failed to update the item amount properly");
                if (item.info.Equals(newInfo4))
                    Assert.AreEqual(item.amount, 2, "failed to update the item amount properly");
            }

            count += 2;
            bridge.addInventory(basket);
            Assert.AreEqual(inventory.Count, count, "count update wrong(add more of exising product");
            
            foreach (Product item in store.inventory)
            {
                if (item.info.Equals(newInfo3))
                    Assert.AreEqual(item.amount, 10, "failed to update the item amount properly(2nd add)");
                if (item.info.Equals(newInfo4))
                    Assert.AreEqual(item.amount, 4, "failed to update the item amount properly(2nd add)");
            }
            


        }
        */
        
        [TestMethod]
        public void parallelPurchase()
        {
            // register twice and login from two different users
            bool user1reg = UserServices.register("AliKB", "123xX456");
            bool user2reg = UserServices.register("Bader", "456xX789");
            // login
            aUser user1 = UserServices.login("AliKB", "123xX456");
            aUser user2 = UserServices.login("Bader", "456xX789");
            // establish a new store
            string storeName = "Ali Shop";
            Stores.addStore(storeName, (Member)user1);
            Store aliShop = Stores.searchStore(storeName);
            // add products to the strore
            aliShop.addProduct("Bamba", "Food", "Osem");
            // set the price of the product
            aliShop.editPrice("Bamba", "Osem", 3);
            // supply 
            aliShop.supply("Bamba", "Osem", 20);
            // try to buy more than 20 bamba in total
            user1.getCart().getBasket(aliShop).addProduct(new Product(ProductInfo.getProductInfo("Bamba", "Food", "Osem"), 12, 0));
            user2.getCart().getBasket(aliShop).addProduct(new Product(ProductInfo.getProductInfo("Bamba", "Food", "Osem"), 12, 0));
            // purchase
            ICollection<Receipt> receipts1 = user1.purchase(new CreditCard());
            ICollection<Receipt> receipts2 = user2.purchase(new CreditCard());

            Assert.IsTrue((receipts1 != null && (receipts1.Count > 0 & receipts2 == null)) | (receipts2 != null && (receipts2.Count > 0 & receipts1 == null)));
            // check for amount
            Assert.IsTrue(aliShop.searchProduct("Bamba", "Osem").amount == 8);
        }

        [TestMethod]
        public void purchaseTestGood()
        {
            // register
            string username1 = "StoreOwner1";
            string pass1 = "123xX456";
            // register and login
            bool ownerReg = UserServices.register(username1, pass1);
            aUser storeOwner = UserServices.login(username1, pass1);
            // establish a new store
            string storeName = "Ali Shop2_purchaseTestGood";
            Stores.addStore(storeName, (Member)storeOwner);
            Store aliShop = Stores.searchStore(storeName);
            // add products to the strore
            aliShop.addProduct("Bamba", "Food", "Osem");
            // set the price of the product
            aliShop.editPrice("Bamba", "Osem", 3);
            // supply 
            aliShop.supply("Bamba", "Osem", 20);
            // register and login
            string username = "AliKSB";
            string pass = "123xX456";

            bool user1reg = UserServices.register(username, pass);
            aUser user1 = UserServices.login(username, pass);
            // try to buy 12 bamba - less that overall
            user1.getCart().getBasket(aliShop).addProduct(new Product(ProductInfo.getProductInfo("Bamba", "Food", "Osem"), 12, 0));
            // purchase
            ICollection<Receipt> receipts1 = user1.purchase(new CreditCard());
            // check for the amounts
            Assert.AreEqual(aliShop.searchProduct("Bamba", "Osem").amount, 8);
        }

        [TestMethod]
        public void purchaseTestBad()
        {
            // register
            string ownerUsername = "AliBB", ownerPass = "123Xx123";
            bool reg = UserServices.register(ownerUsername, ownerPass);
            string storeName = "Ali Shop3";

            aUser owner = UserServices.login(ownerUsername, ownerPass);
            Stores.addStore(storeName, (Member)owner);
            // establish a new store

            
            Store aliShop = bridge.getStore(storeName);
            // add products to the strore
            aliShop.addProduct("Bamba", "Food", "Osem");
            // set the price of the product
            aliShop.editPrice("Bamba", "Osem", 3);
            // supply 
            aliShop.supply("Bamba", "Osem", 20);
            // register and login
            string username = "AliKSBa";
            string pass = "123xX456";

            bool user1reg = UserServices.register(username, pass);
            aUser user1 = UserServices.login(username, pass);
            // try to buy 22 bamba - more that overall
            user1.getCart().getBasket(aliShop).addProduct(new Product(ProductInfo.getProductInfo("Bamba", "Food", "Osem"), 22, 0));
            // purchase
            ICollection<Receipt> receipts1 = user1.purchase(new CreditCard());
            // check for the amounts
            Assert.AreEqual(aliShop.searchProduct("Bamba", "Osem").amount, 20);
        }

        [TestMethod]
        public void addProductTestGood()
        {
            // add new ProductInfo
            ProductInfo p = ProductInfo.getProductInfo("productX2", "categoryX2", "ManufacturerX2");
            ProductInfo p1 = ProductInfo.getProductInfo("productY2", "categoryY2", "ManufacturerY2");
            ProductInfo p2 = ProductInfo.getProductInfo("productZ2", "categoryZ2", "ManufacturerZ2");
            // add products to stores
            Stores.searchStore(store1Name).addProduct(p1.name, p1.category, p1.manufacturer);
            Stores.searchStore(store1Name).addProduct(p2.name, p2.category, p2.manufacturer);
            Stores.searchStore(store2Name).addProduct(p1.name, p1.category, p1.manufacturer);
            // assert that the products exist
            Assert.IsTrue(Stores.searchStore(store1Name).isProductExist(p1.name, p1.manufacturer));
            Assert.IsTrue(Stores.searchStore(store1Name).isProductExist(p2.name, p2.manufacturer));
            Assert.IsTrue(Stores.searchStore(store2Name).isProductExist(p1.name, p1.manufacturer));
            Assert.IsFalse(Stores.searchStore(store2Name).isProductExist(p2.name, p2.manufacturer));
            // clean the stores
            Stores.searchStore(store1Name).removeProduct(p1.name, p1.manufacturer);
            Stores.searchStore(store1Name).removeProduct(p2.name, p2.manufacturer);
            Stores.searchStore(store2Name).removeProduct(p1.name, p1.manufacturer);
            // assert that the products were removed
            Assert.IsFalse(Stores.searchStore(store1Name).isProductExist(p1.name, p1.manufacturer));
            Assert.IsFalse(Stores.searchStore(store1Name).isProductExist(p2.name, p2.manufacturer));
            Assert.IsFalse(Stores.searchStore(store2Name).isProductExist(p1.name, p1.manufacturer));
        }

        [TestMethod]
        public void supplyTestGood()
        {
            // add the products to the stores
            Stores.searchStore(store1Name).addProduct(product1.name, product1.category, product1.manufacturer);
            Stores.searchStore(store1Name).addProduct(product2.name, product2.category, product2.manufacturer);
            Stores.searchStore(store2Name).addProduct(product1.name, product1.category, product1.manufacturer);
            Stores.searchStore(store2Name).addProduct(product2.name, product2.category, product2.manufacturer);
            // supply store1
            Stores.searchStore(store1Name).supply(product1.name, product1.manufacturer, 25);
            Stores.searchStore(store1Name).supply(product2.name, product2.manufacturer, 30);
            // supply store2
            Stores.searchStore(store2Name).supply(product1.name, product1.manufacturer, 10);
            Stores.searchStore(store2Name).supply(product2.name, product2.manufacturer, 40);
            // check amounts
            // store1
            Assert.AreEqual(Stores.searchStore(store1Name).searchProduct(product1.name, product1.manufacturer).amount, 25);
            Assert.AreEqual(Stores.searchStore(store1Name).searchProduct(product2.name, product2.manufacturer).amount, 30);
            // store2
            Assert.AreEqual(Stores.searchStore(store2Name).searchProduct(product1.name, product1.manufacturer).amount, 10);
            Assert.AreEqual(Stores.searchStore(store2Name).searchProduct(product2.name, product2.manufacturer).amount, 40);
        }

        [TestMethod]
        public void supplyTestBad()
        {
            // add new ProductInfo
            ProductInfo p = ProductInfo.getProductInfo("productX", "categoryX", "ManufacturerX");
            ProductInfo p1 = ProductInfo.getProductInfo("productY", "categoryY", "ManufacturerY");
            ProductInfo p2 = ProductInfo.getProductInfo("productZ", "categoryZ", "ManufacturerZ");
            // add the products to the stores
            Stores.searchStore(store1Name).addProduct(p1.name, p1.category, p1.manufacturer);
            Stores.searchStore(store1Name).addProduct(p2.name, p2.category, p2.manufacturer);
            Stores.searchStore(store2Name).addProduct(p1.name, p1.category, p1.manufacturer);
            Stores.searchStore(store2Name).addProduct(p2.name, p2.category, p2.manufacturer);
            // supply stores with illegal amoounts
            Assert.IsFalse(Stores.searchStore(store1Name).supply(p1.name, p1.manufacturer, -5));
            Assert.IsFalse(Stores.searchStore(store1Name).supply(p2.name, p2.manufacturer, -3));
            Assert.IsFalse(Stores.searchStore(store2Name).supply(p1.name, p1.manufacturer, -9));
            Assert.IsFalse(Stores.searchStore(store2Name).supply(p2.name, p2.manufacturer, -6));
            // supply stores with illegal product info
            Assert.IsFalse(Stores.searchStore(store1Name).supply(p.name, p.manufacturer, 30));
            Assert.IsFalse(Stores.searchStore(store2Name).supply(p.name, p.manufacturer, 40));
            Assert.IsFalse(Stores.searchStore(store1Name).supply(p1.name, p.manufacturer, 30)); // wrong manufacturer
            Assert.IsFalse(Stores.searchStore(store2Name).supply(p.name, p1.manufacturer, 40)); // wrong name
            // check amounts
            // store1
            // wrong amounts
            Assert.AreEqual(Stores.searchStore(store1Name).searchProduct(p1.name, p1.manufacturer).amount, 0);
            Assert.AreEqual(Stores.searchStore(store1Name).searchProduct(p2.name, p2.manufacturer).amount, 0);
            // wrong info
            Assert.IsNull(Stores.searchStore(store1Name).searchProduct(p.name, p.manufacturer)); // wrong name & manufacturer
            Assert.IsNull(Stores.searchStore(store1Name).searchProduct(p1.name, p.manufacturer)); // wrong manufacturer
            // store2
            // wrong amounts
            Assert.AreEqual(Stores.searchStore(store2Name).searchProduct(p1.name, p1.manufacturer).amount, 0);
            Assert.AreEqual(Stores.searchStore(store2Name).searchProduct(p2.name, p2.manufacturer).amount, 0);
            // wrong info
            Assert.IsNull(Stores.searchStore(store2Name).searchProduct(p.name, p.manufacturer)); // wrong name & manufacturer
            Assert.IsNull(Stores.searchStore(store2Name).searchProduct(p.name, p1.manufacturer)); // wrong name
        }
        
    }

    [TestClass]
    public class StoresTest
    {
        private static Bridge.Bridge bridge;
        private static ProductInfo product1;
        private static ProductInfo product2;
        private static string store1Name;
        private static string store2Name;
        [ClassInitialize]
        public static void classInit(TestContext context)
        {
            bridge = Driver.getBridge();
            // establish stores
            store1Name = "store1";
            store2Name = "store2";
            bridge.openStore(store1Name);
            bridge.openStore(store2Name);
            // add some produts
            product1 = ProductInfo.getProductInfo("Batman Costume", "clothing", "FairytaleLand");
            product2 = ProductInfo.getProductInfo("Wireless keyboard", "computer accessories", "Logitech");
        }

        [ClassCleanup]
        public static void classCleanUp()
        {
            bridge.logout();
        }
        
        
    }

    [TestClass]
    class ReciptTests
    {
        private static Bridge.Bridge bridge;
        private static ProductInfo prod1, prod2;
        [ClassInitialize]
        public static void classInit(TestContext context)
        {
            bridge = Driver.getBridge();
            bridge.register("recipt1", "Recipt1");
            bridge.register("recipt2", "Recipt2");
            bridge.login("recipt2", "Recipt2");
            bridge.openStore("StoreRecipt1");
            bridge.openStore("StoreRecipt2");
            ProductInfo newInfo1 =  ProductInfo.getProductInfo("item1", "cat", "man");

            ProductInfo newInfo2 =  ProductInfo.getProductInfo("item2", "cat2", "man2");


            prod1 = newInfo1;
            prod2 = newInfo2;


            Product items1 = new Product(prod1, 2, 5), items2 = new Product(prod2, 2, 5);


            ShoppingBasket basket = new ShoppingBasket(bridge.getStore("StoreRecipt1"), (Member)bridge.getUser());
            basket.products.Add(items1);
            basket.products.Add(items2);

            bridge.addInventory(basket);


            bridge.logout();
            bridge.login("recipt1", "Recipt1");
            bridge.addProducts(basket);
            bridge.purchase("Credit");


        }

        [ClassCleanup]
        public static void classCleanUp()
        {
            bridge.logout();
        }

        [TestMethod]
        public void userReciptTestGood()
        {
            Receipt reciept = bridge.GetRecieptByUser("StoreRecipt1", "recipt1", new System.DateTime());
            Assert.AreEqual(reciept.username, "recipt1", "the username is wrong");
            Assert.AreEqual(reciept.store.name, "StoreRecipt1", "the store name is wrong");
            Assert.AreEqual(reciept.actualProducts.Count, 2, "saved wrong product list");
        }


        [TestMethod]
        public void userReciptTestBad()
        {
            bridge.logout();
            bridge.login("recipt2", "Recipt2");

            Receipt reciept = bridge.GetRecieptByUser("StoreRecipt1", "recipt2", new System.DateTime());
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

            Receipt reciept = bridge.GetRecieptByStore("StoreRecipt1", "recipt1", new System.DateTime());
            Assert.AreEqual(reciept.username, "recipt1", "the username is wrong");
            Assert.AreEqual(reciept.store.name, "StoreRecipt1", "the store name is wrong");
            Assert.AreEqual(reciept.actualProducts.Count, 2, "saved wrong product list");
            //clean up
            bridge.logout();
            bridge.login("recipt1", "Recipt1");

        }


        [TestMethod]
        public void storeReciptTestBad()
        {
            bridge.logout();
            bridge.login("recipt2", "Recipt2");


            Receipt reciept = bridge.GetRecieptByUser("StoreRecipt2", "recipt1", new System.DateTime());
            Assert.IsNull(reciept, "managed to get recpit from wrong store");
            //clean up
            bridge.logout();
            bridge.login("recipt1", "Recipt1");

        }

    }

}
