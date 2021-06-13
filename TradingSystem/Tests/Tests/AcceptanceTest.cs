using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Tests.Bridge;
using TradingSystem.BuissnessLayer;
using TradingSystem.BuissnessLayer.commerce;
using TradingSystem.BuissnessLayer.User.Permmisions;
using TradingSystem.ServiceLayer;
using System.Threading;
using System;



namespace Tests
{
    [TestClass]
    public class UserAcceptanceTest
    {
        private static Bridge.Bridge bridge;
        [ClassInitialize]
        public static void classInit(TestContext context)
        {
            bridge = Driver.getBridge();
        }
        
        [TestMethod]
        public void AcceptanceRegisterTestGood()
        {
            string[] u1 = UserServices.register("newUser1", "newPassword1");
            Assert.IsTrue(u1[0].Equals("true"), "faild to register as a valid user");
            string[] u2 = UserServices.login("newUser1", "newPassword1");
            Assert.IsTrue(u2[0].Equals("true"), "user was not properly saved");
            UserController.logout();
            string[] u3 = UserServices.login("newUser1", "fakePassword1");
            Assert.IsTrue(u3[0].Equals("false"), "wrong password found as exist");
            UserController.logout();

            string[] u4 = UserServices.register("newUser2", "newPassword1");
            Assert.IsTrue(u4[0].Equals("true"), "faild to register as a valid user(existing password)");
            string[] u5 = UserServices.login("newUser2", "newPassword1");
            Assert.IsTrue(u5[0].Equals("true"), "user was not properly saved (existing password)");
            UserController.logout();
        }

        [TestMethod]
        public void AcceptanceRegisterTestBad()
        {
            UserServices.register("user1", "Password1");
            string[] u1 = UserServices.register("user1", "Password1");
            Assert.IsFalse(u1[0].Equals("true"), "managed to register as an alread existing user (same password)");
            string[] u2 = UserServices.register("user1", "Password2");
            Assert.IsFalse(u2[0].Equals("true"), "managed to register with an existig username (different passwords)");
            string[] u3 = UserServices.login("user1", "Password2");
            Assert.IsTrue(u3[0].Equals("false"), "exisint user with different passworrd considerd to exist");
            UserController.logout();

            string[] u4 = UserServices.register("badbadBADUSERwithBadWord!@$%$$$$_8", "OKpassword");
            Assert.IsFalse(u4[0].Equals("true"), "managed to register with bad username");
            string[] u5 = UserServices.login("badbadBADUSERwithBadWord!@$%$$$$_8", "OKpassword");
            Assert.IsFalse(u5[0].Equals("true"), "invlid user saved (bad username)");
            UserController.logout();

            string[] u6 = UserServices.register("okUser", "badpassword^^^^^^^^^^^^^^^^^^^^^6^^#@#$%^");
            Assert.IsFalse(u6[0].Equals("true"), "managed to register with bad password");
            string[] u7 = UserServices.login("okUser", "badpassword^^^^^^^^^^^^^^^^^^^^^6^^#@#$%^");
            Assert.IsFalse(u7[0].Equals("true"), "invlid user saved (bad password)");
            UserController.logout();

            string[] u8 = UserServices.register("badbadBADUSERwithBadWord!@$%$$$$_8", "badpassword^^^#@#$%^");
            Assert.IsFalse(u8[0].Equals("true"), "managed to register with bad username and bad password");
            string[] u9 = UserServices.login("badbadBADUSERwithBadWord!@$%$$$$_8", "badpassword^^^#@#$%^");
            Assert.IsFalse(u8[0].Equals("true"), "invlid user saved (bad username and bad passsword)");
            UserController.logout();
        }

        [TestMethod]
        public void AcceptanceEstablishStoreTestGood()
        {
            string storename1 = "estg_store_1";
            string user1 = "estg_user_1";
            UserController.register(user1, "qweE1", 99, "male", "estb 1");
            UserController.login(user1, "qweE1");
            bool res = UserController.EstablishStore(user1, storename1);
            Assert.IsTrue(res);
            Assert.IsNotNull(StoreController.searchStore(storename1));
            SLstore store = StoreController.searchStore(storename1);
            Assert.IsTrue(store.founderName.Equals(user1));
            UserController.logout();

        }

        [TestMethod]
        public void AcceptanceEstablishStoreTestBad()
        {
            string storename1 = "estb_store_1", storename2 = "estb_store_2";
            string user1 = "estb_user_1", user2 = "estb_user_2";
            UserController.register(user1, "qweE1", 99, "male", "estb 1");
            UserController.register(user2, "qweE1", 99, "male", "estb 2");
            UserController.login(user1, "qweE1");
            UserController.EstablishStore(user1, storename1);
            Assert.IsFalse(UserController.EstablishStore(user1, storename1));
            UserController.logout();
            UserController.login(user2, "qweE1");
            Assert.IsFalse(UserController.EstablishStore(user2, storename1));
            UserController.logout();
            Assert.IsFalse(UserServices.EstablishStore("notRealUserName", storename2));
        }

        [TestMethod]
        public void AcceptanceEditManagerPermissionsTestGood()
        {
            string storename = "emptg_store_1", user1 = "emptg_user_1", user2 = "emptg_user_1";
            UserController.register(user1, "qweE1", 99, "male", "emptg 1");
            UserController.register(user2, "qweE1", 99, "male", "emptg 2");
            UserController.login(user1, "qweE1");

            Assert.IsTrue(UserServices.isUserOnline(user1));

            UserController.EstablishStore(user1, storename);

            Assert.IsNotNull(Stores.searchStore(storename));

            UserController.hireNewStoreManager(user1, storename, user2);

            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            permissionList.Add("EditProduct");
            UserController.editManagerPermissions(user1, storename, user2, permissionList);
            var temp = ((Member)UserServices.getUser(user1)).GetPermissions(storename);
            Assert.IsTrue(temp.Contains(PersmissionsTypes.AddProduct));
            Assert.IsTrue(temp.Contains(PersmissionsTypes.EditProduct));

            Assert.IsTrue(UserController.addNewProduct(user1, storename, "bamba", 5.9, 5, "snacks", "osem"));
            Assert.IsTrue(UserController.editProduct(user1, storename, "bamba", 5.5, "osem"));

            UserController.logout();
        }

        [TestMethod]
        public void AcceptanceHireNewStoreOwnerTestGood()
        {
            string storename = "hnsotg_store_1", user1 = "hnsotg_user_1", user2 = "hnsotg_user_2";
            UserController.register(user1, "qweE1", 99, "male", "hnsotg 1");
            UserController.register(user2, "qweE1", 99, "male", "hnsotg 2");
            UserController.login(user1, "qweE1");
            UserController.EstablishStore(user1, storename);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            permissionList.Add("HireNewStoreManager");
            UserController.hireNewStoreOwner(user1, storename, user2, permissionList);
            var ma = UserServices.getUser(user2).GetAllPermissions();
            Assert.IsTrue(ma[storename].Contains("AddProduct"));
            Assert.IsTrue(ma[storename].Contains("HireNewStoreManager"));
            Assert.IsTrue(ma[storename].Count == 2);
            UserController.logout();
        }

        [TestMethod]
        public void AcceptanceRemoveManagerTestGood()//removeMenager doesn't update list of employees.
        {
            string storename = "rmtgstore1", user1 = "rmtgser1", user2 = "rmtguser2";
            UserController.register(user1, "qweE1", 99, "male", "rmtg 1");
            UserController.register(user2, "qweE1", 99, "male", "rmtg 2");
            UserController.login(user1, "qweE1");
            Assert.IsTrue(UserController.EstablishStore(user1, storename));
            UserController.hireNewStoreManager(user1, storename, user2);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            UserController.editManagerPermissions(user1, storename, user2, permissionList);
            int staffSize = UserController.getInfoEmployees(user1, storename).Count;
            UserController.removeManager(user1, storename, user2);

            Assert.IsNull(UserServices.getUser(user2).GetPermissions(storename));
            Assert.AreEqual(UserController.getInfoEmployees(user1, storename).Count, staffSize - 1);
            UserController.logout();
        }

        [TestMethod]
        public void AcceptanceRemoveOwnerTestGood()
        {
            string storename = "rotg_store_14", user1 = "rotg_user_111", user2 = "rotg_user_222";
            UserController.register(user1, "qweE1", 99, "male", "rotg 1");
            UserController.register(user2, "qweE1", 99, "male", "rotg 2");
            UserController.login(user1, "qweE1");
            UserController.EstablishStore(user1, storename);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            UserController.hireNewStoreOwner(user1, storename, user2, permissionList);
            int staffSize = UserController.getInfoEmployees(user1, storename).Count;
            UserController.removeOwner(user1, storename, user2);
            Assert.IsTrue(UserServices.getUser(user1).GetAllPermissions().Keys.Count == 1);
            Assert.IsFalse(StoreController.searchStore(storename).ownerNames.Contains(user2));
            UserController.logout();
        }

        [TestMethod]
        public void AcceptanceRemoveOwnerTestDeep()
        {
            string storename = "rotd_store_1", user1 = "rotd_user_1", user2 = "rotd_user_2", user3 = "rotd_user_3";
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            permissionList.Add("HireNewStoreManager");
            UserController.register(user1, "qweE1", 99, "male", "rotb 1");
            UserController.register(user2, "qweE1", 99, "male", "rotb 2");
            UserController.register(user3, "qweE1", 99, "male", "rotb 3");
            UserController.login(user1, "qweE1");
            UserController.EstablishStore(user1, storename);
            UserController.hireNewStoreOwner(user1, storename, user2, permissionList);
            UserController.logout();
            UserController.login(user2, "qweE1");
            UserController.hireNewStoreManager(user2, storename, user3);
            UserController.logout();
            UserController.login(user1, "qweE1");
            Assert.IsFalse(UserController.removeOwner(user3, storename, user1));
            Assert.IsTrue(UserController.removeOwner(user1, storename, user2));
            Assert.IsTrue(UserController.getInfoEmployees(user1, storename).Count == 1);
            Assert.IsFalse(StoreController.searchStore(storename).ownerNames.Contains(user2));
            Assert.IsFalse(StoreController.searchStore(storename).managerNames.Contains(user3));
            UserController.logout();
        }


        [TestMethod]
        public void TestShopMenagement()
        {
            string username = "sm_owner"; string password = "qweE1"; string storeName = "sm_store1";
            UserController.register(username, password, 99, "female", "address1");
            UserController.login(username, password);
            UserController.EstablishStore(username, storeName);
            ProductInfo newInfo1 = ProductInfo.getProductInfo("smitem1", "smfood", "smmenu1"); //new ProductInfo("item1", "food", "manu1");
            ProductInfo newInfo2 = ProductInfo.getProductInfo("item2", "food", "menu1");
            Store store1 = Stores.searchStore(storeName);
            Stores.searchStore(storeName).addProduct(newInfo1.name, newInfo1.category, newInfo1.manufacturer);
            Stores.searchStore(storeName).addProduct(newInfo2.name, newInfo2.category, newInfo2.manufacturer);
            Stores.searchStore(storeName).supply(newInfo1.name, newInfo1.manufacturer, 10);
            Stores.searchStore(storeName).supply(newInfo2.name, newInfo2.manufacturer, 20);
        
            UserController.logout();

            Assert.IsFalse(UserServices.isUserOnline(username), "logout failed");
            Assert.IsNotNull(Stores.searchStore(storeName), "store was not created");
            Assert.IsTrue(Stores.searchStore(storeName).isProductExist(newInfo1.name, newInfo1.manufacturer), "failed loading products to inventory");
            Assert.IsTrue(Stores.searchStore(storeName).isProductExist(newInfo2.name, newInfo2.manufacturer), "failed loading products to inventory");
            Assert.AreEqual(Stores.searchStore(storeName).searchProduct(newInfo1.name, newInfo1.manufacturer).amount, 10, "did not add correct amount of the item");
            Assert.AreEqual(Stores.searchStore(storeName).searchProduct(newInfo2.name, newInfo2.manufacturer).amount, 20, "did not add correct amount of the item");
        }
    }


    [TestClass]
    public class StoreAcceptanceTest
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
            string username = "ShopOwner11", pass = "123xX321";
            UserServices.register(username, pass, 99, "male", "address1");
            // login
            UserServices.login(username, pass);
            Member owner = (Member)UserServices.getUser(username);
            // init names
            store1Name = "store1_";
            store2Name = "store2_";
            // establish the stroes
            owner.EstablishStore(store1Name);
            owner.EstablishStore(store2Name);
            // init product infos
            product1 = ProductInfo.getProductInfo("Batman Costume", "clothing", "FairytaleLand");
            product2 = ProductInfo.getProductInfo("Wireless keyboard", "computer accessories", "Logitech");
            UserController.logout();
        }

        [TestMethod]
        public void AcceptancePurchaseGood()
        {
            string username = "Member1-ptg"; string password = "Pass2345";
            //Member member2 = new Member("regular", "justsomemember12");
            UserController.register(username, password, 12, "f", "gvfdg");
            UserController.login("Member1-ptg", "Pass2345");

            Stores.searchStore(store1Name).addProduct(product1.name, product1.category, product1.manufacturer);
            Stores.searchStore(store1Name).addProduct(product2.name, product2.category, product2.manufacturer);
            Stores.searchStore(store1Name).supply(product1.name, product1.manufacturer, 10);
            Stores.searchStore(store1Name).supply(product2.name, product2.manufacturer, 20);
            
            string[] ans = UserController.purchase(username, "111111111111", "11/22", "123");
            Store store1 = Stores.searchStore(store1Name);
            UserController.logout();
            //Store store2 = bridge.getStore("Store1");
            Assert.IsNull(ans, ans.Length+"");
            if (ans != null)
            {
                Assert.IsFalse(ans[0].Equals("false"), "error in purchse");
                if (ans[0].Equals("true"))
                {
                    string[] arr = ans[1].Split('$');
                    Assert.IsTrue(arr[1].Equals("Store1"), "different store");
                }
            }
        }

        [TestMethod]
        public void createStoreTestGood()
        {
            string storeOwnerName = "StoreOwner-cstg", storeOwnerPass = "123Xx456";
            string storeName = null;
            UserController.register(storeOwnerName, storeOwnerPass, 99, "nale", "address1");
            UserServices.login(storeOwnerName, storeOwnerPass);
            aUser storeOwner = UserServices.getUser(storeOwnerName);
            // try to create a store  with an empty name
            
            Assert.IsNull(Stores.searchStore(storeName), "found a store with a null name");
            UserController.logout();
        }

        [TestMethod]
        public void PurchaseTestBad()
        {
            bridge.register("Member2-ptb", "Pass2345", 12, "f", "gvfdg");
            bridge.login("Member2-ptb", "Pass2345");
            Dictionary<string, int> d = new Dictionary<string, int>();
            d["item1"] = 10;
            d["item2"] = 10;
            Store store1 = bridge.getStore("Store1-ptb");
            bridge.saveProducts(bridge.getUserName(), "Store1-ptb", "manu1", d);

            var ans = bridge.purchase("111111111111", "11/22", "123");
            bridge.logout();
            Assert.IsTrue(ans[0].Equals("false"), "receipt should be empty");
            Assert.IsTrue(ans[1].Equals("not enough items in stock"), "receipt should be empty");
            Assert.IsFalse(bridge.isUserLoggedIn("Member2"), "user should not be logged in");
            Assert.IsTrue(bridge.getProductAmount("Store1", "item2", "manu1") == 10, "should not change the inventory if the purchase is invalid");//should not process the purchase because there are not enough of the product in the store
        }

        [TestMethod]
        public void parallelPurchase()
        {
            // register twice and login from two different users
            bool user1reg = UserServices.register("AliKB", "123xX456", 12, "m", "some address")[0].Equals("true");
            bool user2reg = UserServices.register("Bader", "456xX789", 12, "m", "some address")[0].Equals("true");

            // login
            UserServices.login("AliKB", "123xX456");
            aUser user1 = UserServices.getUser("AliKB");
            UserServices.login("Bader", "456xX789");
            aUser user2 = UserServices.getUser("Bader");
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
            ICollection<Receipt> receipts1 = new LinkedList<Receipt>();
            ICollection<Receipt> receipts2 = null;

            string[] receipts1String = null, receipts2String = null;
            bool flag = true;

            Thread purchase1 = new Thread(() =>
            {
                Thread.Sleep(2000);
                receipts1String = user1.purchase("111111111111", "11/22", "123");
            }),
                purchase2 = new Thread(() =>
                {
                    Thread.Sleep(2000);
                    receipts2String = user2.purchase("111111111111", "11/22", "123");
                }),
                assertThread = new Thread(() =>
                {
                    Thread.Sleep(3000);
                    // convert the receipts string arrays to receipts collections
                    receipts1 = receipts1String[0] == "false" ? null : convertReceiptsArray(receipts1String);
                    receipts2 = receipts2String[0] == "false" ? null : convertReceiptsArray(receipts2String);

                    flag &= (receipts1 != null && (receipts1.Count > 0 & receipts2 == null)) | (receipts2 != null && (receipts2.Count > 0 & receipts1 == null));

                    // check for amount in the store
                    flag &= aliShop.searchProduct("Bamba", "Osem").amount == 8;
                });

            purchase1.Start();
            purchase2.Start();
            assertThread.Start();

            Thread.Sleep(3500);
            Assert.IsTrue(flag);

            UserServices.logout("AliKB");
        }


        private ICollection<Receipt> convertReceiptsArray(string[] receiptsString)
        {
            ICollection<Receipt> receipts = new LinkedList<Receipt>();
            // receiptsString[0] contains the answer
            for (int i = 1; i < receiptsString.Length; i++)
                receipts.Add(convertReceipt(receiptsString[i]));

            return receipts;
        }

        private Receipt convertReceipt(string receiptString)
        {
            Receipt receipt = new Receipt();

            string[] splitReceipt = receiptString.Split('$');
            // username&storename$price$date$receiptId$<products>
            receipt.username = splitReceipt[0];
            receipt.store = Stores.searchStore(splitReceipt[1]);
            receipt.price = double.Parse(splitReceipt[2]);
            receipt.date = Convert.ToDateTime(splitReceipt[3]);
            receipt.receiptId = int.Parse(splitReceipt[4]);
            // the products - todo


            return receipt;
        }

    }

    [TestClass]
    public class AcceptanceBasketTests
    {
        [TestMethod]
        public void AcceptanceSaveProductTest()
        {
            string username = "AlIbAd";
            string pass = "123xX4";
            string storeName = "asp_store_1";

            UserServices.register(username, pass);
            string[] user = UserServices.login(username, pass);
            // open a new store
            Stores.addStore(storeName, (Member)UserServices.getUser(username));
            //setup
            ProductInfo newInfo3 = ProductInfo.getProductInfo("item3", "cat", "man");

            ProductInfo newInfo4 = ProductInfo.getProductInfo("item4", "cat", "man");

            Product items3 = new Product(newInfo3, 5, 5), items4 = new Product(newInfo4, 2, 5);


            ShoppingBasket basket = new ShoppingBasket(Stores.searchStore(storeName), (Member)UserServices.getUser(username));
            basket.addProduct(items3);
            basket.addProduct(items4);

            Assert.IsTrue(basket.products.Contains(items3), "failed to save one of the items");
            Assert.IsTrue(basket.products.Contains(items4), "failed to save one of the items");
            Assert.IsFalse(basket.products.Contains(null), "saved a null item");
        }
    }

    [TestClass]
    public class AcceptanceZReciptTests
    {
        aUser user1, user2;
        string username1 = "AliKB12", username2 = "Bader12", pass1 = "123xX456", pass2 = "456xX789";
        string storeName;
        private static ProductInfo prod1, prod2;

        [TestMethod]
        public void TestReceipt()
        {
            UserController.register(username1, pass1, 25, "Male", "Be'er Sheva");
            UserController.register(username2, pass2, 30, "Female", "Tel Aviv");
            // login
            UserController.login(username1, pass1);
            user1 = UserServices.getUser(username1);
            UserController.login(username2, pass2);
            user2 = UserServices.getUser(username2);
            // establish a new store
            storeName = "Makolet";
            Stores.addStore(storeName, (Member)user1);
            Store aliShop = Stores.searchStore(storeName);
            // add products to the strore
            aliShop.addProduct("Bamba", "Food", "Osem");
            // set the price of the product
            aliShop.editPrice("Bamba", "Osem", 3);
            // supply 
            aliShop.supply("Bamba", "Osem", 30);
            // add products to shopping carts
            user1.getCart().getBasket(aliShop).addProduct(new Product(ProductInfo.getProductInfo("Bamba", "Food", "Osem"), 12, 0));
            user2.getCart().getBasket(aliShop).addProduct(new Product(ProductInfo.getProductInfo("Bamba", "Food", "Osem"), 18, 0));
            // purchase
            string[] receipts1 = user1.purchase("111111111111", "11/22", "123");
            string[] receipts2 = user2.purchase("111111111111", "11/22", "123");

            userReciptTestGood();
            userReciptTestBad();
            storeReciptsTest();
            adminReceiptsGood();
        }


        public void userReciptTestGood()
        {
            bool user1HasReceipt = false, user2HasReceipt = false;
            // check if the receipts that the user holds contain the receipt from the previous purchase
            ICollection<Receipt> u1Receipts = ((Member)user1).reciepts;
            ICollection<Receipt> u2Receipts = ((Member)user2).reciepts;
            // check for first user
            foreach (Receipt receipt in u1Receipts)
            {
                Assert.AreEqual(receipt.username, username1, "the username is wrong");
                if (receipt.store.name.Equals(storeName) & receipt.actualProducts.Count == 1)
                {
                    LinkedList<Receipt> rAsList = new LinkedList<Receipt>(u1Receipts);
                    LinkedList<Product> products = new LinkedList<Product>(rAsList.First.Value.actualProducts);
                    if (products.First.Value.info.Equals(ProductInfo.getProductInfo("Bamba", "Food", "Osem")) & products.First.Value.amount == 18)
                    {
                        user1HasReceipt = true;
                    }
                }
            }
            // check for user 2
            foreach (Receipt receipt in u2Receipts)
            {
                Assert.AreEqual(receipt.username, username2, "the username is wrong");
                if (receipt.store.name.Equals(storeName) & receipt.actualProducts.Count == 1)
                {
                    LinkedList<Receipt> rAsList = new LinkedList<Receipt>(u2Receipts);
                    LinkedList<Product> products = new LinkedList<Product>(rAsList.First.Value.actualProducts);
                    if (products.First.Value.info.Equals(ProductInfo.getProductInfo("Bamba", "Food", "Osem")))
                    {
                        user2HasReceipt = true;
                    }
                }
            }

            Assert.IsTrue(user1HasReceipt);
            Assert.IsTrue(user2HasReceipt);
        }


        public void userReciptTestBad()
        {
            // check if one of the users got the wrong receipt
            ICollection<Receipt> u1Receipts = ((Member)user1).reciepts;
            ICollection<Receipt> u2Receipts = ((Member)user2).reciepts;

            foreach (Receipt receipt in u1Receipts)
                Assert.AreNotEqual(receipt.username, username2, "user1 got user2's receipt");

            foreach (Receipt receipt in u2Receipts)
                Assert.AreNotEqual(receipt.username, username1, "user2 got user1's receipt");
        }

        public void storeReciptsTest()
        {
            bool user1HasReceipt = false, user2HasReceipt = false;

            ICollection<Receipt> u1Receipts = ((Member)user1).reciepts;
            ICollection<Receipt> u2Receipts = ((Member)user2).reciepts;

            Store store = Stores.searchStore(storeName);
            ICollection<Receipt> storeReceipts = store.getAllReceipts();
            // check if the store has both receipts
            foreach (Receipt receipt in storeReceipts)
            {
                Assert.AreEqual(receipt.store.name, storeName, "wrong store name. expected: " + store.name + ", actual: " + receipt.store.name);
                Assert.AreEqual(receipt.actualProducts.Count, 1);
                Assert.IsTrue(receipt.username.Equals(username1) | receipt.username.Equals(username2));

                if (receipt.username.Equals(username1))
                {
                    LinkedList<Receipt> rAsList = new LinkedList<Receipt>(u1Receipts);
                    LinkedList<Product> products = new LinkedList<Product>(rAsList.First.Value.actualProducts);
                    if (products.First.Value.info.Equals(ProductInfo.getProductInfo("Bamba", "Food", "Osem")))
                    {
                        user1HasReceipt = true;
                    }
                }

                if (receipt.username.Equals(username2))
                {
                    LinkedList<Receipt> rAsList = new LinkedList<Receipt>(u1Receipts);
                    LinkedList<Product> products = new LinkedList<Product>(rAsList.First.Value.actualProducts);
                    if (products.First.Value.info.Equals(ProductInfo.getProductInfo("Bamba", "Food", "Osem")))
                    {
                        user2HasReceipt = true;
                    }
                }
            }

            Assert.IsTrue(user1HasReceipt);
            Assert.IsTrue(user2HasReceipt);
        }

        public void adminReceiptsGood()
        {
            // checks if the admin could fetch receipts
            // init usernames and passes
            string storeName1 = "store1_adminTest", storeName2 = "store2_adminTest";
            string ownerUsername = "ownerA2", ownerPass = "123Xx123";
            string buyerUsername = "noOne2", newPass = "123Xx321";
            //string adminUSername = "admin_test", adminPass = "123Xx123"; //
            // init products info
            string p1_name = "Bamba", p1_man = "Osem", p1_cat = "Food";
            string p2_name = "Jeans", p2_man = "Castro", p2_cat = "clothing";
            // register the users
            UserServices.register(ownerUsername, ownerPass, 12, "f", "dsgvgb");
            UserServices.register(buyerUsername, newPass, 12, "f", "dsgvgb");
            //UserServices.register(adminUSername, adminPass);

            UserServices.login(ownerUsername, ownerPass);
            aUser owner = UserServices.getUser(ownerUsername);
            // establish two stores
            Stores.addStore(storeName1, (Member)owner);
            Stores.addStore(storeName2, (Member)owner);

            Store store1 = Stores.searchStore(storeName1);
            Store store2 = Stores.searchStore(storeName2);
            // add products to the strores
            store1.addProduct(p1_name, p1_cat, p1_man);
            store2.addProduct(p2_name, p2_cat, p2_man);
            // set the price of the products
            store1.editPrice(p1_name, p1_man, 3);
            store2.editPrice(p2_name, p2_man, 4);
            // supply 
            store1.supply(p1_name, p1_man, 20);
            store2.supply(p2_name, p2_man, 30);

            UserServices.login(buyerUsername, newPass);
            aUser client = UserServices.getUser(buyerUsername);

            // add the product to the basket
            client.getCart().getBasket(store1).addProduct(new Product(ProductInfo.getProductInfo(p1_name, p1_man, p1_cat), 12, 0));
            client.getCart().getBasket(store2).addProduct(new Product(ProductInfo.getProductInfo(p2_name, p2_man, p2_cat), 24, 0));
            // purchase
            string[] receipts1 = UserController.purchase(buyerUsername, "111111111111", "11/22", "123");

            Admin admin = (Admin)(UserServices.getAdmin());
            ICollection<Receipt> adminReceiptsCol = admin.getAllReceipts();
            LinkedList<Receipt> adminReceipts = new LinkedList<Receipt>(adminReceiptsCol);

            bool hasFirst = false, hasSecond = true;

            foreach (Receipt receipt in adminReceipts)
            {
                LinkedList<Product> products = new LinkedList<Product>(adminReceipts.First.Value.actualProducts);
                if (products.First.Value.info.Equals(ProductInfo.getProductInfo(p1_name, p1_cat, p1_man)))
                {
                    hasFirst = true;
                }

                if (products.First.Value.info.Equals(ProductInfo.getProductInfo(p2_name, p2_cat, p2_man)))
                {
                    hasSecond = false;
                }
            }

            Assert.IsTrue(hasFirst);
            Assert.IsTrue(hasSecond);
        }

        private ICollection<Receipt> convertReceiptsArray(string[] receiptsString)
        {
            ICollection<Receipt> receipts = new LinkedList<Receipt>();
            // receiptsString[0] contains the answer
            for (int i = 1; i < receiptsString.Length; i++)
                receipts.Add(convertReceipt(receiptsString[i]));

            return receipts;
        }

        private Receipt convertReceipt(string receiptString)
        {
            Receipt receipt = new Receipt();

            string[] splitReceipt = receiptString.Split('$');
            // username&storename$price$date$receiptId$<products>
            receipt.username = splitReceipt[0];
            receipt.store = Stores.searchStore(splitReceipt[1]);
            receipt.price = double.Parse(splitReceipt[2]);
            receipt.date = Convert.ToDateTime(splitReceipt[3]);
            receipt.receiptId = int.Parse(splitReceipt[4]);
            // the products - todo


            return receipt;
        }
    }

    [TestClass]
    public class AcceptancePermissionsTests
    {
        private static string storeName1;
        private static string ownerName1; private static string ownerPassword1;
        private static double age1; private static string gender1; private static string address1;
        private static string storeName2;
        private static string ownerName2; private static string ownerPassword2;
        private static double age2; private static string gender2; private static string address2;
        private static string hiredManagerName; private static string hiredManagerPassword;
        private static double age3; private static string gender3; private static string address3;

        private static ProductInfo p1;
        private static ProductInfo p2;
        private static ProductInfo p3;
        private static ProductInfo pToRemove;
        private static double price1;
        private static double price2;
        private static double price3;
        private static double priceToRemove;
        private static int amount1;
        private static int amount2;
        private static int amount3;
        private static int amountToRemove;


        [ClassInitialize]
        public static void classInit(TestContext context)
        {
            storeName1 = "bestStore";
            ownerName1 = "storeOwner111"; ownerPassword1 = "1A2b3C4d";
            age1 = 30.0; gender1 = "m"; address1 = "thisAddress";
            p1 = ProductInfo.getProductInfo("spoon", "category11", "spoonCompany");

            storeName2 = "secondBest";
            ownerName2 = "storeOwner222"; ownerPassword2 = "1a2B3c4D";
            age2 = 31; gender2 = "f"; address2 = "nowhere";

            hiredManagerName = "hiredManager1"; hiredManagerPassword = "hiRed11";
            age3 = 25; gender3 = "m"; address3 = "thePlace";

            p2 = ProductInfo.getProductInfo("fork", "category11", "forksForLife");
            p3 = ProductInfo.getProductInfo("knife", "category11", "company121");
            pToRemove = ProductInfo.getProductInfo("removethis", "category2", "company 211");
            price1 = 3.99;
            price2 = 3.99;
            price3 = 4.99;
            priceToRemove = 10.99;
            amount1 = 10;
            amount2 = 15;
            amount3 = 30;
            amountToRemove = 100;
            //registration of users
            UserServices.register(ownerName1, ownerPassword1, age1, gender1, address1);
            UserServices.register(ownerName2, ownerPassword2, age2, gender2, address2);
            UserServices.register(hiredManagerName, hiredManagerPassword, age3, gender3, address3);

            //login
            UserServices.login(ownerName1, ownerPassword1);
            Member owner1 = (Member)UserServices.getUser(ownerName1);
            Member owner2 = (Member)UserServices.getUser(ownerName2);
            //establish store
            owner1.EstablishStore(storeName1);
            owner2.EstablishStore(storeName2);
        }


        [TestMethod]
        public void addProductPermissionGood()
        {
            UserServices.login(ownerName1, ownerPassword1);
            Member owner1 = (Member)UserServices.getUser(ownerName1);

            bool passTest = owner1.addNewProduct(storeName1, p1.name, price1, amount1, p1.category, p1.manufacturer);
            passTest &= owner1.addNewProduct(storeName1, p2.name, price2, amount2, p2.category, p2.manufacturer);
            bool hasPermission = false;
            foreach (PersmissionsTypes p in owner1.GetPermissions(storeName1))
            {
                if (p == PersmissionsTypes.AddProduct)
                    hasPermission = true;
            }


            Assert.IsTrue(passTest & hasPermission);
            Assert.IsNotNull(Stores.searchStore(storeName1).searchProduct(p1.name, p1.manufacturer));
            int num = Stores.searchStore(storeName1).searchProduct(p2.name, p2.manufacturer).amount;
            Assert.IsTrue(num == amount2);

            UserServices.logout(ownerName1);
        }

        [TestMethod]
        public void editProductPermissionGood()
        {
            UserServices.login(ownerName1, ownerPassword1);
            Member owner1 = (Member)UserServices.getUser(ownerName1);

            bool passedPreConds = owner1.addNewProduct(storeName1, p3.name, price3, amount3, p3.category, p3.manufacturer);
            if (passedPreConds)
            {
                bool hasPermission = false;
                foreach (PersmissionsTypes p in owner1.GetPermissions(storeName1))
                {
                    if (p == PersmissionsTypes.EditProduct)
                        hasPermission = true;
                }

                Assert.IsTrue(hasPermission, "does not hvae permission.");

                double newPrice = 6.99;

                Assert.IsTrue(owner1.editProduct(storeName1, p3.name, newPrice, p3.manufacturer), "edit product did not execute successfully ");//fails because Store.Inventory never updates when using AddProduct.
                Assert.AreEqual(Stores.searchStore(storeName1).searchProduct(p3.name, p3.manufacturer).price, newPrice);
            }

            UserServices.logout(ownerName1);

        }

        [TestMethod]
        public void editProductPermissionBad()
        {
            UserServices.login(ownerName2, ownerPassword2);
            Member owner2 = (Member)UserServices.getUser(ownerName2);

            ProductInfo p5 = ProductInfo.getProductInfo("error", "none", "empty");
            double newPrice = 1.99;
            bool passTest = owner2.editProduct(storeName2, p5.name, newPrice, p5.manufacturer);

            Assert.IsFalse(passTest);//product does not exist in the inventory

            passTest = owner2.editProduct(storeName1, p1.name, newPrice, p1.manufacturer);

            Assert.IsFalse(passTest);//owner2 have no permissions over store "storeName1"
            Assert.AreEqual(Stores.searchStore(storeName1).searchProduct(p1.name, p1.manufacturer).price, price1);//price should not change in case of unauthorized use.

            UserServices.logout(ownerName2);
        }

        [TestMethod]
        public void removeProductPermissionGood()
        {
            UserServices.login(ownerName1, ownerPassword1);
            Member owner1 = (Member)UserServices.getUser(ownerName1);

            bool hasPermission = false;
            foreach (PersmissionsTypes p in owner1.GetPermissions(storeName1))
            {
                if (p == PersmissionsTypes.RemoveProduct)
                    hasPermission = true;
            }

            Assert.IsTrue(hasPermission);
            bool passTest = owner1.removeProduct(storeName1, pToRemove.name, pToRemove.manufacturer);

            Assert.IsTrue(passTest);
            Assert.IsNull(Stores.searchStore(storeName1).searchProduct(pToRemove.name, pToRemove.manufacturer));

            UserServices.logout(ownerName1);

        }

        [TestMethod]
        public void removeProductPermissionBad()
        {
            UserServices.login(ownerName2, ownerPassword2);
            Member owner2 = (Member)UserServices.getUser(ownerName2);

            ProductInfo p5 = ProductInfo.getProductInfo("error", "none", "empty");
            bool passTest = owner2.removeProduct(storeName1, p1.name, p1.manufacturer);

            Assert.IsFalse(passTest);//does not have the permission to do so.
            Assert.AreEqual(Stores.searchStore(storeName1).searchProduct(p1.name, p1.manufacturer).amount, amount1);

            UserServices.logout(ownerName2);

        }

        [TestMethod]
        public void HireNewStoreManagerGood()
        {
            UserServices.login(ownerName1, ownerPassword1);
            Member owner1 = (Member)UserServices.getUser(ownerName1);
            bool hasPermission = false;
            foreach (PersmissionsTypes p in owner1.GetPermissions(storeName1))
            {
                if (p == PersmissionsTypes.HireNewStoreManager)
                    hasPermission = true;
            }

            Assert.IsTrue(hasPermission);

            bool passTest = owner1.hireNewStoreManager(storeName1, hiredManagerName);

            Assert.IsTrue(passTest);
            Assert.IsTrue(Stores.searchStore(storeName1).isManager(hiredManagerName));

            UserServices.logout(ownerName1);

            UserServices.login(hiredManagerName, hiredManagerPassword);
            Member hiredManager = (Member)UserServices.getUser(hiredManagerName);

            hasPermission = false;
            bool noPermission = false;

            foreach (PersmissionsTypes p in hiredManager.GetPermissions(storeName1))
            {
                if (p == PersmissionsTypes.GetInfoEmployees)
                    hasPermission = true;
                if (p == PersmissionsTypes.AddProduct || p == PersmissionsTypes.HireNewStoreOwner)
                    noPermission = true;
            }
            Assert.IsTrue(hasPermission);
            Assert.IsFalse(noPermission);
        }

        [TestMethod]
        public void HireNewStoreManagerBad()
        {

        }

    }

    [TestClass]
    public class AcceptancePolicyTests
    {
        [TestMethod]
        public void ProductAgePolicyGood()
        {
            // init usernames and passes
            string storeName1 = "store_ProductAgePolicyGood";
            string ownerUsername = "owner_001", ownerPass = "123Xx123";
            string buyerUsername = "noOne_001", newPass = "123Xx321";
            // init products info
            string p1_name = "Bamba", p1_man = "Osem", p1_cat = "Food";
            // register the users
            UserServices.register(ownerUsername, ownerPass, 117, "male", "Moria");
            UserServices.register(buyerUsername, newPass, 18, "female", "TA"); // the buyer is 18 - he can buy

            UserServices.login(ownerUsername, ownerPass);
            aUser owner = UserServices.getUser(ownerUsername);
            // establish two stores
            Stores.addStore(storeName1, (Member)owner);

            Store store1 = Stores.searchStore(storeName1);
            // add products to the strores
            store1.addProduct(p1_name, p1_cat, p1_man);
            // set the price of the products
            store1.editPrice(p1_name, p1_man, 3);
            // supply 
            store1.supply(p1_name, p1_man, 20);

            store1.addAgePolicyByProduct(p1_name, p1_cat, p1_man, 18);

            UserServices.login(buyerUsername, newPass);
            aUser client = UserServices.getUser(buyerUsername);

            // add the product to the basket
            client.getCart().getBasket(store1).addProduct(new Product(ProductInfo.getProductInfo(p1_name, p1_cat, p1_man), 12, 0));

            // purchase
            string[] receipts1 = client.purchase("111111111111", "11/22", "123");

            Assert.IsTrue(receipts1[0].Equals("true") || !receipts1[1].Equals("Policy err"), "couldn't manage to buy bamba with age = 18");

            // check for amounts in the basket and in the store
            if (receipts1[0].Equals("true"))
            {
                Assert.AreEqual(client.getBasket(store1).products.Count, 0, "products' number is non zero.");
                Assert.AreEqual(store1.searchProduct(p1_name, p1_man).amount, 8, "expected 8 products after purchase.");
            }

        }

        [TestMethod]
        public void ProductAgePolicyBad()
        {// init usernames and passes
            string storeName1 = "store_ProductAgePolicyBad";
            string ownerUsername = "owner_002", ownerPass = "123Xx123";
            string buyerUsername = "noOne_002", newPass = "123Xx321";
            // init products info
            string p1_name = "Bamba", p1_man = "Osem", p1_cat = "Food";
            // register the users
            UserServices.register(ownerUsername, ownerPass, 117, "male", "Moria");
            UserServices.register(buyerUsername, newPass, 15, "female", "TA");

            UserServices.login(ownerUsername, ownerPass);
            aUser owner = UserServices.getUser(ownerUsername);
            // establish two stores
            Stores.addStore(storeName1, (Member)owner);

            Store store1 = Stores.searchStore(storeName1);
            // add products to the strores
            store1.addProduct(p1_name, p1_cat, p1_man);
            // set the price of the products
            store1.editPrice(p1_name, p1_man, 3);
            // supply 
            store1.supply(p1_name, p1_man, 20);

            store1.addAgePolicyByProduct(p1_name, p1_cat, p1_man, 18);

            UserController.logout();

            UserServices.login(buyerUsername, newPass);
            aUser client = UserServices.getUser(buyerUsername);

            // add the product to the basket
            client.getCart().getBasket(store1).addProduct(new Product(ProductInfo.getProductInfo(p1_name, p1_cat, p1_man), 12, 0));

            // purchase
            string[] receipts1 = client.purchase("111111111111", "11/22", "123");

            Assert.AreEqual(receipts1[0], "false", "managed to buy bamba with age = 15");
            Assert.AreEqual(receipts1[1], "Policy err", "the error isn't policy related");
        }

        [TestMethod]
        public void CategoryAgePolicyGood()
        {
            // init usernames and passes
            string storeName1 = "store_CategoryAgePolicyGood";
            string ownerUsername = "owner_003", ownerPass = "123Xx123";
            string buyerUsername = "noOne_003", newPass = "123Xx321";
            // init products info
            string p1_name = "Corona Beer", p1_man = "Corona", p1_cat = "alcohol";
            // register the users
            UserServices.register(ownerUsername, ownerPass, 117, "male", "Moria");
            UserServices.register(buyerUsername, newPass, 19, "female", "TA");

            UserServices.login(ownerUsername, ownerPass);
            aUser owner = UserServices.getUser(ownerUsername);
            // establish two stores
            Stores.addStore(storeName1, (Member)owner);

            Store store1 = Stores.searchStore(storeName1);
            // add products to the strores
            store1.addProduct(p1_name, p1_cat, p1_man);
            // set the price of the products
            store1.editPrice(p1_name, p1_man, 3);
            // supply 
            store1.supply(p1_name, p1_man, 20);

            store1.addAgePolicyByCategory(p1_cat, 18);

            UserServices.login(buyerUsername, newPass);
            aUser client = UserServices.getUser(buyerUsername);

            // add the product to the basket
            client.getCart().getBasket(store1).addProduct(new Product(ProductInfo.getProductInfo(p1_name, p1_cat, p1_man), 12, 0));

            // purchase
            string[] receipts1 = client.purchase("111111111111", "11/22", "123");

            Assert.IsTrue(receipts1[0].Equals("true") || !receipts1[1].Equals("Policy err"), "couldn't manage to buy an alcoholic drink with age = 18");

            // check for amounts in the basket and in the store
            if (receipts1[0].Equals("true"))
            {
                Assert.AreEqual(client.getBasket(store1).products.Count, 0, "products' number is non zero.");
                Assert.AreEqual(store1.searchProduct(p1_name, p1_man).amount, 8, "expected 8 products after purchase.");
            }
        }

        [TestMethod]
        public void CategoryAgePolicyBad()
        {
            // init usernames and passes
            string storeName1 = "store_CategoryAgePolicyGood";
            string ownerUsername = "owner_004", ownerPass = "123Xx123";
            string buyerUsername = "noOne_004", newPass = "123Xx321";
            // init products info
            string p1_name = "Corona Beer", p1_man = "Corona", p1_cat = "alcohol";
            // register the users
            UserServices.register(ownerUsername, ownerPass, 117, "male", "Moria");
            UserServices.register(buyerUsername, newPass, 15, "female", "TA");

            UserServices.login(ownerUsername, ownerPass);
            aUser owner = UserServices.getUser(ownerUsername);
            // establish two stores
            Stores.addStore(storeName1, (Member)owner);

            Store store1 = Stores.searchStore(storeName1);
            // add products to the strores
            store1.addProduct(p1_name, p1_cat, p1_man);
            // set the price of the products
            store1.editPrice(p1_name, p1_man, 3);
            // supply 
            store1.supply(p1_name, p1_man, 20);

            store1.addAgePolicyByCategory(p1_cat, 18);

            UserServices.login(buyerUsername, newPass);
            aUser client = UserServices.getUser(buyerUsername);

            // add the product to the basket
            client.getCart().getBasket(store1).addProduct(new Product(ProductInfo.getProductInfo(p1_name, p1_cat, p1_man), 12, 0));

            // purchase
            string[] receipts1 = client.purchase("111111111111", "11/22", "123");

            Assert.AreEqual(receipts1[0], "false", "managed to buy alcoholic drink with age = 15");
            Assert.AreEqual(receipts1[1], "Policy err", "the error isn't policy related");
        }

        /*
        [TestMethod]
        public void ProductDailyPolicyGood() { }

        [TestMethod]
        public void ProductDailyPolicyBad() { }

        [TestMethod]
        public void CategoryDailyPolicyGood() { }

        [TestMethod]
        public void CategoryDailyPolicyBad() { }
        */

        [TestMethod]
        public void ProductMaxAmountPolicyGood()
        {
            // init usernames and passes
            string storeName1 = "store_MaxAmountPolicyGood";
            string ownerUsername = "owner_005", ownerPass = "123Xx123";
            string buyerUsername = "noOne_005", newPass = "123Xx321";
            // init products info
            string p1_name = "iPhone 8", p1_man = "Apple", p1_cat = "phones";
            int maxAmount = 5;
            // register the users
            UserServices.register(ownerUsername, ownerPass, 117, "m", "Moria");
            UserServices.register(buyerUsername, newPass, 15, "f", "TA");

            UserServices.login(ownerUsername, ownerPass);
            aUser owner = UserServices.getUser(ownerUsername);
            // establish two stores
            Stores.addStore(storeName1, (Member)owner);

            Store store1 = Stores.searchStore(storeName1);
            // add products to the strores
            store1.addProduct(p1_name, p1_cat, p1_man);
            // set the price of the products
            store1.editPrice(p1_name, p1_man, 3);
            // supply 
            store1.supply(p1_name, p1_man, 20);

            store1.addMaxAmountPolicyByProduct(p1_name, p1_cat, p1_man, maxAmount);

            UserServices.login(buyerUsername, newPass);
            aUser client = UserServices.getUser(buyerUsername);

            // add the product to the basket
            client.getCart().getBasket(store1).addProduct(new Product(ProductInfo.getProductInfo(p1_name, p1_cat, p1_man), maxAmount - 2, 0));

            // purchase
            string[] receipts1 = client.purchase("111111111111", "11/22", "123");

            Assert.IsTrue(receipts1[0].Equals("true") || !receipts1[1].Equals("Policy err"), "couldn't manage to buy an iPhone with amount=3, maxAmount=5");

            // check for amounts in the basket and in the store
            if (receipts1[0].Equals("true"))
            {
                Assert.AreEqual(client.getBasket(store1).products.Count, 0, "products' number is non zero.");
                Assert.AreEqual(Stores.searchStore(storeName1).searchProduct(p1_name, p1_man).amount, 17, "expected 8 products after purchase.");
            }
        }

        [TestMethod]
        public void ProductMaxAmountPolicyBad()
        {
            // init usernames and passes
            string storeName1 = "store_MaxAmountPolicyGood";
            string ownerUsername = "owner_006", ownerPass = "123Xx123";
            string buyerUsername = "noOne_006", newPass = "123Xx321";
            // init products info
            string p1_name = "iPhone 8", p1_man = "Apple", p1_cat = "phones";
            int maxAmount = 5;
            // register the users
            UserServices.register(ownerUsername, ownerPass, 117, "m", "Moria");
            UserServices.register(buyerUsername, newPass, 15, "f", "TA");

            UserServices.login(ownerUsername, ownerPass);
            aUser owner = UserServices.getUser(ownerUsername);
            // establish two stores
            Stores.addStore(storeName1, (Member)owner);

            Store store1 = Stores.searchStore(storeName1);
            // add products to the strores
            store1.addProduct(p1_name, p1_cat, p1_man);
            // set the price of the products
            store1.editPrice(p1_name, p1_man, 3);
            // supply 
            store1.supply(p1_name, p1_man, 20);

            store1.addMaxAmountPolicyByProduct(p1_name, p1_cat, p1_man, maxAmount);

            UserServices.login(buyerUsername, newPass);
            aUser client = UserServices.getUser(buyerUsername);

            // add the product to the basket
            client.getCart().getBasket(store1).addProduct(new Product(ProductInfo.getProductInfo(p1_name, p1_cat, p1_man), maxAmount + 2, 0));

            // purchase
            string[] receipts1 = client.purchase("111111111111", "11/22", "123");

            Assert.AreEqual(receipts1[0], "false", "managed to buy an iPhone with: amount=7, maxAmount=5");
            Assert.AreEqual(receipts1[1], "Policy err", "the error isn't policy related");
        }

        [TestMethod]
        public void CategoryMaxAmountPolicyGood()
        {
            // init usernames and passes
            string storeName1 = "store_CategoryMaxAmountPolicyGood";
            string ownerUsername = "owner_007", ownerPass = "123Xx123";
            string buyerUsername = "noOne_007", newPass = "123Xx321";
            // init products info
            string p1_name = "iPhone 8", p1_man = "Apple", p1_cat = "phones";
            int maxAmount = 5;
            // register the users
            UserServices.register(ownerUsername, ownerPass, 117, "m", "Moria");
            UserServices.register(buyerUsername, newPass, 15, "f", "TA");

            UserServices.login(ownerUsername, ownerPass);
            aUser owner = UserServices.getUser(ownerUsername);
            // establish two stores
            Stores.addStore(storeName1, (Member)owner);

            Store store1 = Stores.searchStore(storeName1);
            // add products to the strores
            store1.addProduct(p1_name, p1_cat, p1_man);
            // set the price of the products
            store1.editPrice(p1_name, p1_man, 3);
            // supply 
            store1.supply(p1_name, p1_man, 20);

            store1.addMaxAmountPolicyByCategory(p1_cat, maxAmount);

            UserServices.login(buyerUsername, newPass);
            aUser client = UserServices.getUser(buyerUsername);

            // add the product to the basket
            client.getCart().getBasket(store1).addProduct(new Product(ProductInfo.getProductInfo(p1_name, p1_cat, p1_man), maxAmount - 2, 0));

            // purchase
            string[] receipts1 = client.purchase("111111111111", "11/22", "123");

            Assert.IsTrue(receipts1[0].Equals("true") || !receipts1[1].Equals("Policy err"), "couldn't manage to buy an iPhone with amount=3, maxAmount=5");

            // check for amounts in the basket and in the store
            if (receipts1[0].Equals("true"))
            {
                Assert.AreEqual(client.getBasket(store1).products.Count, 0, "products' number is non zero.");
                Assert.AreEqual(store1.searchProduct(p1_name, p1_man).amount, 8, "expected 8 products after purchase.");
            }
        }

        [TestMethod]
        public void CategoryMaxAmountPolicyBad()
        {
            // init usernames and passes
            string storeName1 = "store_CategoryMaxAmountPolicyGood";
            string ownerUsername = "owner_006", ownerPass = "123Xx123";
            string buyerUsername = "noOne_006", newPass = "123Xx321";
            // init products info
            string p1_name = "iPhone 8", p1_man = "Apple", p1_cat = "phones";
            int maxAmount = 5;
            // register the users
            UserServices.register(ownerUsername, ownerPass, 117, "m", "Moria");
            UserServices.register(buyerUsername, newPass, 15, "f", "TA");

            UserServices.login(ownerUsername, ownerPass);
            aUser owner = UserServices.getUser(ownerUsername);
            // establish two stores
            Stores.addStore(storeName1, (Member)owner);

            Store store1 = Stores.searchStore(storeName1);
            // add products to the strores
            store1.addProduct(p1_name, p1_cat, p1_man);
            // set the price of the products
            store1.editPrice(p1_name, p1_man, 3);
            // supply 
            store1.supply(p1_name, p1_man, 20);

            store1.addMaxAmountPolicyByCategory(p1_cat, maxAmount);

            UserServices.login(buyerUsername, newPass);
            aUser client = UserServices.getUser(buyerUsername);

            // add the product to the basket
            client.getCart().getBasket(store1).addProduct(new Product(ProductInfo.getProductInfo(p1_name, p1_cat, p1_man), maxAmount + 2, 0));

            // purchase
            string[] receipts1 = client.purchase("111111111111", "11/22", "123");

            Assert.AreEqual(receipts1[0], "false", "managed to buy an iPhone with: amount=7, maxAmount=5");
            Assert.AreEqual(receipts1[1], "Policy err", "the error isn't policy related");
        }



    }

}
