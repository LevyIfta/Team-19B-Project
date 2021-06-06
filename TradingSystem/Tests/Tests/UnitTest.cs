using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using Tests.Bridge;
using TradingSystem.BuissnessLayer;
using System.Linq;
using TradingSystem.BuissnessLayer.commerce;
using TradingSystem.ServiceLayer;
using TradingSystem.BuissnessLayer.User.Permmisions;
using System.Threading;
using System;

namespace Tests
{
    [TestClass]
    public class UserAccessUnitTest
    {
        [ClassInitialize]
        public static void classInit(TestContext context)
        {
            /*
            bridge = Driver.getBridge();
            bridge.register("user1", "Password1");
            bridge.register("user2", "Password2");
            */
        }

        [TestMethod]
        public void loginTestGood()
        {
            // , fake1 = "fake1", fake2 = "fake2" ' , fakePass1 = "111", fakePass2 = "789"
            // init usernames and passes
            UserServices.logout("AbD1");
            UserServices.logout("QwEr2");
            string username1 = "AbD1", username2 = "QwEr2";
            string pass1 = "123Xx456", pass2 = "465Ss789";
            // register
            UserController.register(username1, pass1);
            UserController.register(username2, pass2);
            // try to login twice
            aUser u = UserServices.getUser(username1);
            //string[] usr = UserServices.login(username1, pass1);
            //Assert.IsTrue(UserController.login(username1, pass1));
            string[] usr2 = UserServices.login(username2, pass2);
            Assert.IsTrue(usr2[0].Equals("true"));
            UserServices.logout("QwEr2");
            UserServices.logout("AbD1");

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
            string[] u1 = UserServices.login(username1, pass1);
            string[] u2 = UserServices.login(username1, pass1);
            Assert.IsTrue(u2[0].Equals("false"));
            // try to login with fake user
            string[] u3 = UserServices.login(fake1, fakePass1);
            Assert.IsTrue(u3[0].Equals("false"), "managed to login as fake user");
            UserController.logout();
            // fake pass
            string[] u4 = UserServices.login(username2, fakePass2);
            Assert.IsTrue(u4[0].Equals("false"), "managed to login with wrong password");
            UserController.logout();

            string[] u5 = UserServices.login(fake1, pass1);
            Assert.IsTrue(u5[0].Equals("false"), "managed to login with fake username");
            UserController.logout();
        }

        [TestMethod]
        public void registerTestGood()
        {
            string[] u1 = UserServices.register("newUser1", "newPassword1");
            Assert.IsTrue(u1[0].Equals("true"), "faild to register as a valid user");
            string[] u2 = UserServices.login("newUser1", "newPassword1");
            Assert.IsTrue(u1[0].Equals("true"), "user was not properly saved");
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
        public void registerTestBad()
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
        public void logoutTest()
        {
            string username = "newUser1_", pass = "newPas00s";
            UserServices.register(username, pass);
            string[] u = UserServices.login(username, pass);
            string[] u1 = UserServices.login(username, pass);
            Assert.IsFalse(u1[0].Equals("true")); // user should be logged in
            UserServices.logout(username);
            string[] u3 = UserServices.login(username, pass);
            Assert.IsTrue(u3[0].Equals("true"));
            UserController.logout();
        }

        [TestMethod]
        public void adminTestBad()
        {
            string username = "admin", pass = "Admin1";
            string[] user = UserServices.login(username, pass);
        }

        [TestMethod]
        public void establishStoreTestGood()
        {
            string storename1 = "estg_store_1";
            string user1 = "estg_user_1";
            UserController.register(user1, user1, 99, "male", "estg 1");
            Assert.IsTrue(UserController.EstablishStore(user1, storename1));
            Assert.IsNotNull(StoreController.searchStore(storename1));
            SLstore store = StoreController.searchStore(storename1);
            Assert.IsTrue(store.founderName.Equals(user1));
        }

        [TestMethod]
        public void establishStoreTestBad()
        {
            string storename1 = "estb_store_1", storename2 = "estb_store_2";
            string user1 = "estb_user_1", user2 = "estb_user_2";
            UserController.register(user1, user1, 99, "male", "estb 1");
            UserController.register(user2, user2, 99, "male", "estb 2");
            UserController.EstablishStore(user1, storename1);
            Assert.IsFalse(UserController.EstablishStore(user1,storename1));
            Assert.IsFalse(UserController.EstablishStore(user2, storename1));
            Assert.IsFalse(UserServices.EstablishStore("notRealUserName", storename2));
        }

        [TestMethod]
        public void editManagerPermissionsTestGood()
        {
            string storename = "emptg_store_1", user1 = "emptg_user_1", user2 = "emptg_user_1";
            UserController.register(user1, user1, 99, "male", "emptg 1");
            UserController.register(user2, user2, 99, "male", "emptg 2");
            UserController.EstablishStore(user1, storename);
            UserController.hireNewStoreManager(user1, storename, user2);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            permissionList.Add("EditProduct");
            UserController.editManagerPermissions(user1, storename, user2, permissionList);
            Assert.IsTrue(UserController.addNewProduct(user1,storename, "bamba", 5.9, 5, "snacks", "osem"));
            Assert.IsTrue(UserController.editProduct(user1,storename, "bamba", 5.5, "osem"));
        }

        [TestMethod]
        public void editManagerPermissionsTestBad1()
        {//edit permissions of unhired member
            string storename = "emptb1_store_1", user1 = "emptb1_user_1", user2 = "emptb1_user_2";
            UserController.register(user1, user1, 99, "male", "emptb 1");
            UserController.EstablishStore(user1, storename);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            permissionList.Add("EditProduct");
            UserController.editManagerPermissions(user1, storename, user2, permissionList);
        }

        [TestMethod]
        public void editManagerPermissionsTestBad2()
        {//edit permission of manager hired by different owner
            string storename = "emptb2_store_1", owner1 = "emptb2_user_1", owner2 = "emptb2_user_2", manager = "emptb2_user_3";
            UserController.register(owner1, owner1, 99, "male", "emptb 2");
            UserController.register(owner2, owner2, 99, "male", "emptb 3");
            UserController.register(manager, manager, 99, "male", "emptb 4");
            UserController.EstablishStore(owner1, storename);
            UserController.hireNewStoreManager(owner1, storename, manager);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            permissionList.Add("EditProduct");
            permissionList.Add("EditManagerPermissions");
            UserController.hireNewStoreOwner(owner1, storename, owner2, permissionList);
            Assert.IsFalse(UserController.editManagerPermissions(owner2, storename, manager, permissionList));
        }

        [TestMethod]
        public void editManagerPermissionsTestBad3()
        {//edit permissions of manager in different store
            string storename = "emptb2_store_1", wrongstore = "wrongstore", user1 = "emptb3_user_1", user2 = "emptb3_user_2";
            UserController.register(user1, user1, 99, "male", "emptb 5");
            UserController.register(user2, user2, 99, "male", "emptb 6");
            UserController.EstablishStore(user1, storename);
            UserController.hireNewStoreManager(user1, storename, user2);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            permissionList.Add("EditProduct");
            Assert.IsFalse(UserController.editManagerPermissions(user1,wrongstore, user2, permissionList));
        }

        [TestMethod]
        public void ownerGetInfoEmployeesTestGood()
        {
            string storename = "ogietg_store_1", user1 = "ogietg_user_1", user2 = "ogietg_user_2";
            UserController.register(user1, user1, 99, "male", "ogietg 1");
            UserController.register(user2, user2, 99, "male", "ogietg 2");
            UserController.EstablishStore(user1, storename);
            UserController.hireNewStoreManager(user1, storename, user2);
            Assert.IsTrue(UserController.getInfoEmployees(user2,storename).ElementAt(0).userName.Equals(user2));
        }

        [TestMethod]
        public void ownerGetInfoEmployeesTestBad()
        {
            string storename = "ogietb_store_1", wrongstore = "wrongstore", user1 = "ogietb_user_1", user2 = "ogietb_user_2";
            UserController.register(user1, user1, 99, "male", "ogietb 1");
            UserController.register(user2, user2, 99, "male", "ogietb 2");
            UserController.EstablishStore(user1, storename);
            Assert.IsNull(UserController.getInfoEmployees(user1, storename)); //owner not his own employee
            UserController.hireNewStoreManager(user1, storename, user2);
            Assert.IsNull(UserController.getInfoEmployees(user1, wrongstore)); //store doesnt exist
            Assert.IsNull(UserController.getInfoEmployees(user2,storename)); //no permissions

        }

        [TestMethod]
        public void hireNewStoreOwnerTestGood()
        {
            string storename = "hnsotg_store_1", user1 = "hnsotg_user_1", user2 = "hnsotg_user_2";
            UserController.register(user1, user1, 99, "male", "hnsotg 1");
            UserController.register(user2, user2, 99, "male", "hnsotg 2");
            UserController.EstablishStore(user1, storename);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            permissionList.Add("HireNewStoreManager");
            UserController.hireNewStoreOwner(user1, storename, user2, permissionList);
            Assert.IsTrue(UserServices.getUser(user1).GetAllPermissions()[storename].Contains("AddProduct"));
            Assert.IsTrue(UserServices.getUser(user1).GetAllPermissions()[storename].Contains("HireNewStoreManager"));
            Assert.IsTrue(UserServices.getUser(user1).GetAllPermissions()[storename].Count == 2);
        }

        [TestMethod]
        public void hireNewStoreOwnerTestBad()
        {
            string storename = "hnsotb_store_1", wrongstore = "wroongstore", user1 = "hnsotb_user_1", user2 = "hnsotb_user_2", wronguser = "wronguser";
            UserController.register(user1, user1, 99, "male", "hnsotb 1");
            UserController.register(user2, user2, 99, "male", "hnsotb 2");
            UserController.EstablishStore(user1, storename);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            permissionList.Add("HireNewStoreManager");
            Assert.IsFalse(UserController.hireNewStoreOwner(user1, wrongstore, user2, permissionList)); //store doesnt exist
            Assert.IsFalse(UserController.hireNewStoreOwner(user1, storename, wronguser, permissionList)); //user doesnt exist
            UserController.hireNewStoreOwner(user1, storename, user2, permissionList);
            Assert.IsFalse(UserController.hireNewStoreOwner(user1, storename, user2, permissionList)); //hire already hired user
        }

        [TestMethod]
        public void removeManagerTestGood()
        {
            string storename = "rmtg_store_1", user1 = "rmtg_user_1", user2 = "rmtg_user_2";
            UserController.register(user1, user1, 99, "male", "rmtg 1");
            UserController.register(user2, user2, 99, "male", "rmtg 2");
            UserController.EstablishStore(user1, storename);
            UserController.hireNewStoreManager(user1,storename, user2);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            UserController.editManagerPermissions(user1, storename, user2, permissionList);
            Assert.IsTrue(UserController.removeManager(user1, storename, user2));
            Assert.IsTrue(UserServices.getUser(user2).GetAllPermissions()[storename].Count == 0);
            Assert.IsFalse(UserController.getInfoEmployees(user1, storename).Count == 0);
        }

        [TestMethod]
        public void removeManagerTestBad1()
        {//remove manager you didnt assign
            string storename = "rmtb1_store_1", user1 = "rmtb1_user_1", user2 = "rmtb1_user_2", user3 = "rmtb1_user_3";
            UserController.register(user1, user1, 99, "male", "rmtb 1");
            UserController.register(user2, user2, 99, "male", "rmtb 2");
            UserController.register(user3, user3, 99, "male", "rmtb 3");
            UserController.EstablishStore(user1, storename);
            UserController.hireNewStoreManager(user1, storename, user2);
            UserController.hireNewStoreManager(user1, storename, user3);
            Assert.IsFalse(UserController.removeManager(user2, storename, user3));
        }

        [TestMethod]
        public void removeManagerTestBad2()
        {//remove non-manager
            string storename = "rmtb2_store_1", user1 = "rmtb2_user_1", user2 = "rmtb2_user_2";
            UserController.register(user1, user1, 99, "male", "rmtb 4");
            UserController.register(user2, user2, 99, "male", "rmtb 5");
            UserController.EstablishStore(user1, storename);
            Assert.IsFalse(UserController.removeManager(user1, storename, user2)); //remove non employee
            Assert.IsFalse(UserController.removeManager(user1, storename, user1)); //remove self
            UserController.hireNewStoreManager(user1, storename, user2);
            UserController.removeManager(user1, storename, user2);
            Assert.IsFalse(UserController.removeManager(user1, storename, user2)); //remove manager that was already removed
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            UserController.hireNewStoreOwner(user1, storename, user2, permissionList);
            Assert.IsFalse(UserController.removeManager(user1, storename, user2)); //user remove manager on owner
        }

        [TestMethod]
        public void removeManagerTestBad3()
        {//remove manager from wrong store
            string storename = "rmtb3_store_1", wrongstore = "wrongstore", user1 = "rmtb3_user_1", user2 = "rmtb3_user_2";
            UserController.register(user1, user1, 99, "male", "rmtb 6");
            UserController.register(user2, user2, 99, "male", "rmtb 7");
            UserController.EstablishStore(user1, storename);
            UserController.hireNewStoreManager(user1, storename, user2);
            Assert.IsFalse(UserController.removeManager(user1, wrongstore, user2));
        }

        [TestMethod]
        public void removeOwnerTestGood()
        {
            string storename = "rotg_store_1", user1 = "rotg_user_1", user2 = "rotg_user_2";
            UserController.register(user1, user1, 99, "male", "rotg 1");
            UserController.register(user2, user2, 99, "male", "rotg 2");
            UserController.EstablishStore(user1, storename);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            UserController.hireNewStoreOwner(user1, storename, user2, permissionList);
            Assert.IsTrue(UserController.removeOwner(user1, storename, user2));
            Assert.IsTrue(UserServices.getUser(user1).GetAllPermissions()[storename].Count == 0);
            Assert.IsFalse(StoreController.searchStore(storename).ownerNames.Contains(user2));
        }

        [TestMethod]
        public void removeOwnerTestBad1()
        {//remove owner you didn't assign
            string storename = "rotb1_store_1", user1 = "rotb1_user_1", user2 = "rotb1_user_2", user3 = "rotb1_user_3";
            UserController.register(user1, user1, 99, "male", "rotb 1");
            UserController.register(user2, user2, 99, "male", "rotb 2");
            UserController.register(user3, user3, 99, "male", "rotb 3");
            UserController.EstablishStore(user1, storename);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            UserController.hireNewStoreOwner(user1, storename, user2, permissionList);
            UserController.hireNewStoreOwner(user1, storename, user3, permissionList);
            Assert.IsFalse(UserController.removeOwner(user2, storename, user3));
        }

        [TestMethod]
        public void removeOwnerTestBad2()
        {//remove non-owner
            string storename = "rotb2_store_1", user1 = "rotb2_user_1", user2 = "rotb2_user_2";
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            UserController.register(user1, user1, 99, "male", "rotb 4");
            UserController.register(user2, user2, 99, "male", "rotb 5");
            UserController.EstablishStore(user1, storename);
            Assert.IsFalse(UserController.removeOwner(user1, storename, user2)); //remove non employee
            Assert.IsFalse(UserController.removeOwner(user1, storename, user1)); //remove self
            UserController.hireNewStoreOwner(user1, storename, user2, permissionList);
            UserController.removeOwner(user1, storename, user2);
            Assert.IsFalse(UserController.removeOwner(user1, storename, user2)); //remove manager that was already removed
            UserController.hireNewStoreManager(user1, storename, user2);
            Assert.IsFalse(UserController.removeOwner(user1, storename, user2)); //user remove owner on manager
        }

        [TestMethod]
        public void removeOwnerTestBad3()
        {//remove owner from wrong store
            string storename = "rotb3_store_1", wrongstore = "wrongstore", user1 = "rotb3_user_1", user2 = "rotb3_user_2";
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            UserController.register(user1, user1, 99, "male", "rotb 6");
            UserController.register(user2, user2, 99, "male", "rotb 7");
            UserController.EstablishStore(user1, storename);
            UserController.hireNewStoreOwner(user1, storename, user2, permissionList);
            Assert.IsFalse(UserController.removeOwner(user1, wrongstore, user2));
        }

        [TestMethod]
        public void removeOwnerTestDeep()
        {
            string storename = "rotd_store_1", user1 = "rotd_user_1", user2 = "rotd_user_2", user3 = "rotd_user_3";
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            UserController.register(user1, user1, 99, "male", "rotb 1");
            UserController.register(user2, user2, 99, "male", "rotb 2");
            UserController.register(user3, user3, 99, "male", "rotb 3");
            UserController.EstablishStore(user1, storename);
            UserController.hireNewStoreOwner(user1, storename, user2, permissionList);
            UserController.hireNewStoreManager(user2, storename, user3);
            Assert.IsTrue(UserController.removeOwner(user1, storename, user2));
            Assert.IsFalse(UserController.getInfoEmployees(user1, storename).Count == 0);
            Assert.IsFalse(StoreController.searchStore(storename).ownerNames.Contains(user2));
            Assert.IsFalse(StoreController.searchStore(storename).managerNames.Contains(user3));
        }

        [TestMethod]
        public void noPermissionTest()
        {
            string storename = "npt_store_1", user1 = "npt_user_1", user2 = "npt_user_2";
            string productName = "bamba", productManuf = "osem";
            UserController.register(user1, user1, 99, "male", "rotb 1");
            UserController.register(user2, user2, 99, "male", "rotb 2");
            UserController.EstablishStore(user1, storename);
            Assert.IsFalse(UserController.addNewProduct(user2, storename, productName, 5.9, 5, "snacks", productManuf));
            Assert.IsFalse(StoreController.searchStore(storename).inventory.Count == 1);
        }

    }

    [TestClass]
    public class StoreTests
    {
        private static ProductInfo product1;
        private static ProductInfo product2;
        private static string store1Name;
        private static string store2Name;

        [ClassInitialize]
        public static void classInit(TestContext context)
        {
            string username = "ShopOwner11", pass = "123xX321";
            bool ownerReg = UserServices.register(username, pass)[0].Equals("true");
            // create 2 stores
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
        }

        [TestMethod]
        public void createStoreTestGood()
        {
            string storeOwnerName = "StoreOwner_", storeOwnerPass = "123Xx456";
            string storeName = null;
            bool ownerReg = UserController.register(storeOwnerName, storeOwnerPass)[0].Equals("true");
            UserServices.login(storeOwnerName, storeOwnerPass);
            aUser storeOwner = UserServices.getUser(storeOwnerName);
            // try to create a store  with an empty name
            Assert.IsFalse(Stores.addStore(storeName, (Member)storeOwner), "managed to create a store with a null name");
            Assert.IsNull(Stores.searchStore(storeName), "found a store with a null name");
            UserController.logout();
        }

        [TestMethod]
        public void createStoreTestBad()
        {
            string storeOwnerName = "StoreOwner", storeOwnerPass = "123Xx456";
            string storeName = "createStoreTestBad_store1";
            bool ownerReg = UserController.register(storeOwnerName, storeOwnerPass)[0].Equals("true");
            UserServices.login(storeOwnerName, storeOwnerPass);
            aUser storeOwner = UserServices.getUser(storeOwnerName);
            Stores.addStore(storeName, (Member)storeOwner);
            Store store = Stores.searchStore(storeName);

            Assert.IsFalse(Stores.addStore(storeName, (Member)storeOwner), "manage to open a store with an existing name");
            Assert.AreEqual(store.founder.getUserName(), storeOwnerName, "store founder changed");
            UserController.logout();
        }

        [TestMethod]
        public void searchStoreTest()
        {
            string storeOwnerName = "StoreOwner_1", storeOwnerPass = "123Xx456";
            string storeName = "searchStoreTestGood_store1";
            bool ownerReg = UserServices.register(storeOwnerName, storeOwnerPass)[0].Equals("true");
            UserServices.login(storeOwnerName, storeOwnerPass);
            aUser storeOwner = UserServices.getUser(storeOwnerName);
            Stores.addStore(storeName, (Member)storeOwner);
            Store store = Stores.searchStore(storeName);

            Assert.IsNotNull(store, "could not find an existing store: " + storeName);
            Assert.IsNull(Stores.searchStore("this store does not exist"));
            //UserController.logout();
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

        [TestMethod]
        public void purchaseTestGood()
        {
            // register
            string username1 = "StoreOwner1";
            string pass1 = "123xX456";
            // register and login
            bool ownerReg = UserServices.register(username1, pass1, 12, "f", "some address")[0].Equals("true");
            UserServices.login(username1, pass1);

            aUser storeOwner = UserServices.getUser(username1);
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

            bool user1reg = UserServices.register(username, pass, 12, "f", "some address")[0].Equals("true");
            UserServices.login(username, pass);

            aUser user1 = UserServices.getUser(username);
            // try to buy 12 bamba - less that overall
            user1.getCart().getBasket(aliShop).addProduct(new Product(ProductInfo.getProductInfo("Bamba", "Food", "Osem"), 12, 0));
            // purchase
            string[] receipts1 = user1.purchase("111111111111", "11/22", "123");
            // check for the amounts
            Assert.AreEqual(aliShop.searchProduct("Bamba", "Osem").amount, 8);
            Assert.AreEqual(user1.getBasket(aliShop).products.Count, 0);
        }

        [TestMethod]
        public void purchaseTestBad()
        {
            // register
            string ownerUsername = "AliBB", ownerPass = "123Xx123";
            bool reg = UserServices.register(ownerUsername, ownerPass, 12, "m", "some address")[0].Equals("true");
            string storeName = "Ali Shop3";

            UserServices.login(ownerUsername, ownerPass);
            aUser owner = UserServices.getUser(ownerUsername);
            // establish a new store
            Stores.addStore(storeName, (Member)owner);

            Store aliShop = Stores.searchStore(storeName);
            // add products to the strore
            aliShop.addProduct("Bamba", "Food", "Osem");
            // set the price of the product
            aliShop.editPrice("Bamba", "Osem", 3);
            // supply 
            aliShop.supply("Bamba", "Osem", 20);
            // register and login
            string username = "AliKSBa";
            string pass = "123xX456";

            bool user1reg = UserServices.register(username, pass, 12, "f", "some address")[0].Equals("true");
            UserServices.login(username, pass);

            aUser user1 = UserServices.getUser(username);
            // try to buy 22 bamba - more that overall
            user1.getCart().getBasket(aliShop).addProduct(new Product(ProductInfo.getProductInfo("Bamba", "Food", "Osem"), 22, 0));
            // purchase
            string[] receipts1 = user1.purchase("111111111111", "11/22", "123");
            // check for the amounts
            Assert.AreEqual(aliShop.searchProduct("Bamba", "Osem").amount, 20);
            Assert.AreNotEqual(user1.getBasket(aliShop).products.Count, 0);
            Assert.AreEqual(receipts1[0], "false", "The receipt tells that the purchase had done.");
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

        [TestMethod]
        public void addFeedBackBad()
        {
            // trying to add a feedback on a product that the user didn't buy
            // init usernames and passes
            string ownerUsername = "ownerA1", ownerPass = "123Xx123";
            string newUsername = "noOne", newPass = "123Xx321";
            // register the users
            UserServices.register(ownerUsername, ownerPass, 12, "f", "some address");
            UserServices.register(newUsername, newPass, 12, "f", "some address");

            string storeName = "Ali Shop444";

            UserServices.login(ownerUsername, ownerPass);
            aUser owner = UserServices.getUser(ownerUsername);
            // establish a new store
            Stores.addStore(storeName, (Member)owner);

            Store aliShop = Stores.searchStore(storeName);
            // add products to the strore
            aliShop.addProduct("Bamba", "Food", "Osem");

            // try to leave feedback with the new user - didn't buy yet
            aUser newUser = UserServices.getUser(newUsername);
            bool feedbackSuccess = UserServices.leaveFeedback(newUsername, storeName, "Bamba", "Osem", "This is non-valid feedback.");

            Assert.IsFalse(feedbackSuccess, "Managed to leave a feedback with a user that didn't buy the product.");
        }

        [TestMethod]
        public void addFeedBackGood()
        {
            // trying to add a feedback on a product that the user didn't buy
            // init usernames and passes
            string ownerUsername = "ownerA2", ownerPass = "123Xx123";
            string buyerUsername = "noOne2", newPass = "123Xx321";
            string FOUsername = "FBObserve", FOPass = "123Xx123"; // FO: feedback observer
            // register the users
            UserServices.register(ownerUsername, ownerPass, 12, "f", "some address");
            UserServices.register(buyerUsername, newPass, 12, "f", "some address");
            UserServices.register(FOUsername, FOPass, 12, "f", "some address");

            string storeName = "Ali Shop121";

            UserServices.login(ownerUsername, ownerPass);
            aUser owner = UserServices.getUser(ownerUsername);
            // establish a new store
            Stores.addStore(storeName, (Member)owner);

            Store aliShop = Stores.searchStore(storeName);
            // add products to the strore
            aliShop.addProduct("Bamba", "Food", "Osem");
            // set the price of the product
            aliShop.editPrice("Bamba", "Osem", 3);
            // supply 
            aliShop.supply("Bamba", "Osem", 20);

            UserServices.login(buyerUsername, newPass);
            aUser user1 = UserServices.getUser(buyerUsername);

            // add the product to the basket
            user1.getCart().getBasket(aliShop).addProduct(new Product(ProductInfo.getProductInfo("Bamba", "Food", "Osem"), 12, 0));
            // purchase
            string[] receipts1 = user1.purchase("111111111111", "11/22", "123");

            string feedback = "This is valid feedback.";
            bool feedbackSuccess = UserServices.leaveFeedback(buyerUsername, storeName, "Bamba", "Osem", feedback);

            // chack if the answer is true
            Assert.IsTrue(feedbackSuccess, "Couldn't leave feedback even though the user has already bought the product.");

            // now check if other users could see the feedback
            UserServices.login(FOUsername, FOPass);
            aUser observer = UserServices.getUser(FOUsername);

            foreach (KeyValuePair<Store, Product> product in observer.browseProducts("Bamba", "Osem"))
            {
                if (product.Key.name.Equals(storeName)) // the value is the product in the store, check for the feedback
                {
                    Assert.AreEqual(product.Value.info.feedbacks[buyerUsername], feedback, "the feedback was not equal/not found");
                    break;
                }
            }
        }

        [TestMethod]
        public void purchasePaymentSystemBad()
        {
            string storename = "ppsb_store_1", user1 = "ppsb_user_1";
            string productName = "bamba", productManuf = "osem";
            UserController.register(user1, user1, 99, "male", "ppsb 1");
            UserController.EstablishStore(user1, storename);
            UserController.addNewProduct(user1, storename, productName, 5.5, 100, "snacks", productManuf);
            Dictionary<string, int> products = new Dictionary<string, int>();
            products.Add(productName, 1);
            UserController.saveProduct(user1, storename, productManuf, products);
            Assert.IsTrue(UserController.purchase(user1, "abcd", "abcd", "abcd")[1].Equals("payment not approved"));
        }

        [TestMethod]
        public void purchaseSupplySystemBad()
        {
            string storename = "pssb_store_1", user1 = "pssb_user_1";
            string productName = "bamba", productManuf = "osem";
            UserController.register(user1, user1, 99, "male", "0");
            UserController.EstablishStore(user1, storename);
            UserController.addNewProduct(user1, storename, productName, 5.5, 100, "snacks", productManuf);
            Dictionary<string, int> products = new Dictionary<string, int>();
            products.Add(productName, 1);
            UserController.saveProduct(user1, storename, productManuf, products);
            Assert.IsTrue(UserController.purchase(user1, "1234567812345678", "01/99", "111")[1].Equals("supply not approved"));
        }
    }

    [TestClass]
    public class BasketTests
    {
        private static ProductInfo prod1, prod2;


        [ClassInitialize]
        public static void classInit(TestContext context)
        {
            /*
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
            */
        }


        [TestMethod]
        public void saveProductTest()
        {
            string username = "AlIbAd";
            string pass = "123xX4";
            string storeName = "new_store_1";

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
    public class ZReciptTests
    {
        aUser user1, user2;
        string username1 = "AliKB", username2 = "Bader", pass1 = "123xX456", pass2 = "456xX789";
        string storeName;
        private static ProductInfo prod1, prod2;
        [ClassInitialize]
        public static void classInit(TestContext context)
        {
            UserServices.register("recipt1", "Recipt1");
            UserServices.register("recipt2", "Recipt2");
            UserServices.login("recipt2", "Recipt2");
            aUser u = UserServices.getUser("recipt2");
            u.EstablishStore("StoreRecipt1");
            u.EstablishStore("StoreRecipt2");
            ProductInfo newInfo1 = ProductInfo.getProductInfo("item1", "cat", "man");

            ProductInfo newInfo2 = ProductInfo.getProductInfo("item2", "cat2", "man2");


            prod1 = newInfo1;
            prod2 = newInfo2;


            Product items1 = new Product(prod1, 2, 5), items2 = new Product(prod2, 2, 5);


            ShoppingBasket basket = new ShoppingBasket(Stores.searchStore("StoreRecipt1"), (Member)u);
            basket.products.Add(items1);
            basket.products.Add(items2);
            /*
            bridge.addInventory(basket);


            bridge.logout();
            bridge.login("recipt1", "Recipt1");
            bridge.addProducts(basket);
            bridge.purchase("Credit");

    */
    
        }

        [TestMethod]
        public void TestAll()
        {
            // this fuction initializes all the needed arguments 
            // and runs all tests
            // register twice and login from two different users
            bool user1reg = UserServices.register(username1, pass1, 25, "Male", "Be'er Sheva")[0].Equals("true");
            bool user2reg = UserServices.register(username2, pass2, 30, "Female", "Tel Aviv")[0].Equals("true");
            // login
            UserServices.login(username1, pass1);
            user1 = UserServices.getUser(username1);
            UserServices.login(username2, pass2);
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
            // test 
            userReciptTestGood();
            userReciptTestBad();
            storeReciptsTest();
            adminReceiptsGood();
        }


        public void userReciptTestGood()
        {
            bool user1HasReceipt = false, user2HasReceipt = false;
            // check if the receipts that the user holds contain the receipt from the previous purchase
            ICollection<Receipt> u1Receipts = ((Member)UserServices.getUser(user1.getUserName())).reciepts;
            ICollection<Receipt> u2Receipts = ((Member)user2).reciepts;
            // check for first user
            foreach (Receipt receipt in u1Receipts)
            {
                Assert.AreEqual(receipt.username, username1, "the username is wrong");
                if (receipt.store.name.Equals(storeName) & receipt.actualProducts.Count == 1)
                {
                    LinkedList<Receipt> rAsList = new LinkedList<Receipt>(u1Receipts);
                    LinkedList<Product> products = new LinkedList<Product>(rAsList.First.Value.actualProducts);
                    if (products.First.Value.info.Equals(ProductInfo.getProductInfo("Bamba", "Food", "Osem")) & products.First.Value.amount == 12)
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
                    if (products.First.Value.info.Equals(ProductInfo.getProductInfo("Bamba", "Food", "Osem")) & products.First.Value.amount == 18)
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
                    if (products.First.Value.info.Equals(ProductInfo.getProductInfo("Bamba", "Food", "Osem"))
                        & products.First.Value.amount == 12)
                    {
                        user1HasReceipt = true;
                    }
                }

                if (receipt.username.Equals(username2))
                {
                    LinkedList<Receipt> rAsList = new LinkedList<Receipt>(u1Receipts);
                    LinkedList<Product> products = new LinkedList<Product>(rAsList.First.Value.actualProducts);
                    if (products.First.Value.info.Equals(ProductInfo.getProductInfo("Bamba", "Food", "Osem"))
                        & products.First.Value.amount == 18)
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
            UserServices.register(ownerUsername, ownerPass);
            UserServices.register(buyerUsername, newPass);
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
            string[] receipts1 = client.purchase("111111111111", "11/22", "123");

            Admin admin = (Admin)(UserServices.getAdmin());
            ICollection<Receipt> adminReceiptsCol = admin.getAllReceipts();
            LinkedList<Receipt> adminReceipts = new LinkedList<Receipt>(adminReceiptsCol);

            bool hasFirst = false, hasSecond = false;

            foreach (Receipt receipt in adminReceipts)
            {
                LinkedList<Product> products = new LinkedList<Product>(adminReceipts.First.Value.actualProducts);
                if (products.First.Value.info.Equals(ProductInfo.getProductInfo(p1_name, p1_cat, p1_man))
                    & products.First.Value.amount == 12)
                {
                    hasFirst = true;
                }

                if (products.First.Value.info.Equals(ProductInfo.getProductInfo(p2_name, p2_cat, p2_man))
                   & products.First.Value.amount == 24)
                {
                    hasSecond = true;
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
    public class PermissionsTests
    {
        private static string storeName1;
        private static string ownerName1; private static string ownerPassword1;
        private static double age1; private static string gender1; private static string address1;
        private static string storeName2;
        private static string ownerName2; private static string ownerPassword2;
        private static double age2; private static string gender2; private static string address2;
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
        public void addProductPermissionBad()
        {
            UserServices.login(ownerName2, ownerPassword2);
            Member owner2 = (Member)UserServices.getUser(ownerName2);

            bool wrongStore = owner2.addNewProduct(storeName1, p1.name, price1, amount1, p1.category, p1.manufacturer);
            Assert.IsFalse(wrongStore);//did not establish this store, and is not at any managment position. has no permissions at all.

            owner2.removePermission(storeName2, null);//removes all permissions from the user
            bool noPermission = owner2.addNewProduct(storeName2, p2.name, price2, amount2, p2.category, p2.manufacturer);
            Assert.IsFalse(noPermission);//user has no "addNewProduct" Permission.

            Member owner1 = (Member)UserServices.getUser(ownerName1);
            foreach (PersmissionsTypes p in owner1.GetPermissions(storeName1))
            {
                owner2.addPermission(aPermission.who(p, storeName2, null));//return all permissions to storeOwner
            }

            UserServices.logout(ownerName2);
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

                Assert.IsTrue(hasPermission, "asd");

                double newPrice = 6.99;
                bool passTest = owner1.editProduct(storeName1, p3.name, newPrice, p3.manufacturer);

                Assert.IsTrue(passTest, "ere");
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
        }

        [TestMethod]
        public void removeProductPermissionBad()
        {
            UserServices.login(ownerName2, ownerPassword2);
            Member owner2 = (Member)UserServices.getUser(ownerName2);

            ProductInfo p5 = ProductInfo.getProductInfo("error", "none", "empty");
            bool passTest = owner2.removeProduct(storeName1, p1.name, p1.manufacturer);

            Assert.IsFalse(passTest);//does not have the permission to do so.
            int num = Stores.searchStore(storeName1).searchProduct(p1.name, p1.manufacturer).amount;
            Assert.AreEqual(num, amount1);
        }
    }

    [TestClass]
    public class PolicyTests
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
        public void CategoryAgePolicyGood() {
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
        public void CategoryAgePolicyBad() {
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
            string storeName1 = "store_CategoryAgePolicyGood";
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
                Assert.AreEqual(store1.searchProduct(p1_name, p1_man).amount, 8, "expected 8 products after purchase.");
            }
        }

        [TestMethod]
        public void ProductMaxAmountPolicyBad() {
            // init usernames and passes
            string storeName1 = "store_CategoryAgePolicyGood";
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
        public void CategoryMaxAmountPolicyGood() {
            // init usernames and passes
            string storeName1 = "store_CategoryAgePolicyGood";
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
        public void CategoryMaxAmountPolicyBad() {
            // init usernames and passes
            string storeName1 = "store_CategoryAgePolicyGood";
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

