using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using Tests.Bridge;
using TradingSystem.BuissnessLayer;
using System.Linq;
using TradingSystem.BuissnessLayer.commerce;
using TradingSystem.ServiceLayer;
using TradingSystem.BuissnessLayer.User.Permmisions;
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

            string[] usr = UserServices.login(username2, pass2);
            Assert.IsTrue(usr[0].Equals("true"));
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
            int oldCount = UserServices.Users.Count;
            string[] u1 = UserServices.register("newUser1reg", "newPassword1");
            Assert.AreEqual(UserServices.Users.Count, oldCount + 1);
            oldCount = UserServices.Users.Count;
            string[] u2 = UserServices.register("newUser2reg", "newPassword1");
            Assert.AreEqual(UserServices.Users.Count, oldCount + 1);
        }

        [TestMethod]
        public void registerTestBad()
        {
            UserServices.register("user1", "Password1");
            int oldCount = UserServices.Users.Count;
            string[] u1 = UserServices.register("user1", "Password1");
            Assert.AreEqual(UserServices.Users.Count, oldCount, "registered same user twice");
            oldCount = UserServices.Users.Count;
            string[] u2 = UserServices.register("user1", "Password2");
            Assert.AreEqual(UserServices.Users.Count, oldCount, "managed to register with an existig username (different passwords)");
            oldCount = UserServices.Users.Count;
            string[] u3 = UserServices.register("badbadBADUSERwithBadWord!@$%$$$$_8", "OKpassword");
            Assert.AreEqual(UserServices.Users.Count, oldCount, "managed to register with bad username");
            oldCount = UserServices.Users.Count;
            string[] u4 = UserServices.register("okUser", "badpassword^^^^^^^^^^^^^^^^^^^^^6^^#@#$%^");
            Assert.AreEqual(UserServices.Users.Count, oldCount, "managed to register with bad password");
            oldCount = UserServices.Users.Count;
            string[] u5 = UserServices.register("badbadBADUSERwithBadWord!@$%$$$$_8", "badpassword^^^#@#$%^");
            Assert.AreEqual(UserServices.Users.Count, oldCount, "managed to register with bad username and bad password");
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
            string[] user = UserController.login(username, pass);
            Assert.IsTrue(UserController.getUserName().Equals("admin"));
        }

        [TestMethod]
        public void establishStoreTestGood()
        {
            string storename1 = "uestg_store_1";
            string user1 = "uestg_user_1";
            UserController.register(user1, "qweE1", 99, "male", "estg 1");
            UserController.login(user1, "qweE1");
            Assert.IsTrue(UserServices.EstablishStore(user1, storename1));
            UserController.logout();
        }

        [TestMethod]
        public void establishStoreTestBad()
        {
            string storename1 = "estb_store_1", storename2 = "estb_store_2";
            string user1 = "estb_user_1", user2 = "estb_user_2";
            UserController.register(user1, "qweE1", 99, "male", "estb 1");
            UserController.register(user2, "qweE1", 99, "male", "estb 2");
            UserController.login(user1, "qweE1");
            UserServices.EstablishStore(user1, storename1);
            Assert.IsFalse(UserServices.EstablishStore(user1,storename1));
            UserController.logout();
            UserController.login(user2, "qweE1");
            Assert.IsFalse(UserServices.EstablishStore(user2, storename1));
            UserController.logout();
            Assert.IsFalse(UserServices.EstablishStore("notRealUserName", storename2));
        }

        [TestMethod]
        public void editManagerPermissionsTestGood()
        {
            string storename = "emptg_store_1", user1 = "emptg_user_1", user2 = "emptg_user_1";
            UserController.register(user1, "qweE1", 99, "male", "emptg 1");
            UserController.register(user2, "qweE1", 99, "male", "emptg 2");
            UserController.login(user1, "qweE1");
            UserServices.EstablishStore(user1, storename);
            UserController.hireNewStoreManager(user1, storename, user2);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            permissionList.Add("EditProduct");
            Assert.IsTrue(UserController.editManagerPermissions(user1, storename, user2, permissionList));
            UserController.logout();
        }

        [TestMethod]
        public void editManagerPermissionsTestBad1()
        {//edit permissions of unhired member
            string storename = "emptb1_store_1", user1 = "emptb1_user_1", user2 = "emptb1_user_2";
            UserController.register(user1, "qweE1", 99, "male", "emptb 1");
            UserController.login(user1, "qweE1");
            UserServices.EstablishStore(user1, storename);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            permissionList.Add("EditProduct");
            Assert.IsFalse(UserController.editManagerPermissions(user1, storename, user2, permissionList));
            UserController.logout();
        }

        [TestMethod]
        public void editManagerPermissionsTestGood1()
        {//edit permission of manager hired by different owner
            string storename = "emptb2_store_1_1", owner1 = "emptb2_user_1_1", owner2 = "emptb2_user_2_1", manager = "emptb2_user_3";
            UserController.register(owner1, "qweE1", 99, "male", "emptb 2");
            UserController.register(owner2, "qweE1", 99, "male", "emptb 3");
            UserController.register(manager, "qweE1", 99, "male", "emptb 4");
            UserController.login(owner1, "qweE1");
            UserServices.EstablishStore(owner1, storename);
            UserController.hireNewStoreManager(owner1, storename, manager);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            permissionList.Add("EditProduct");
            permissionList.Add("EditManagerPermissions");
            UserController.hireNewStoreOwner(owner1, storename, owner2, permissionList);
            UserController.logout();
            UserController.login(owner2, "qweE1");
            Assert.IsTrue(UserController.editManagerPermissions(owner2, storename, manager, permissionList));
            UserController.logout();
        }
        [TestMethod]
        public void editManagerPermissionsTestBad2()
        {//edit permission of manager hired by different owner
            string storename = "emptb2_store_1", owner1 = "emptb2_user_1", owner2 = "emptb2_user_2", manager = "emptb2_user_3";
            UserController.register(owner1, "qweE1", 99, "male", "emptb 2");
            UserController.register(owner2, "qweE1", 99, "male", "emptb 3");
            UserController.register(manager, "qweE1", 99, "male", "emptb 4");
            UserController.login(owner1, "qweE1");
            UserServices.EstablishStore(owner1, storename);
            UserController.hireNewStoreManager(owner1, storename, manager);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            permissionList.Add("EditProduct");
            UserController.hireNewStoreOwner(owner1, storename, owner2, permissionList);
            UserController.logout();
            UserController.login(owner2, "qweE1");
            Assert.IsFalse(UserController.editManagerPermissions(owner2, storename, manager, permissionList));
            UserController.logout();
        }

        [TestMethod]
        public void editManagerPermissionsTestBad3()
        {//edit permissions of manager in different store
            string storename = "emptb2_store_11", wrongstore = "wrongstore", user1 = "emptb3_user_11", user2 = "emptb3_user_22";
            UserController.register(user1, "qweE1", 99, "male", "emptb 5");
            UserController.register(user2, "qweE1", 99, "male", "emptb 6");
            UserController.login(user1, "qweE1");
            UserServices.EstablishStore(user1, storename);
            UserController.hireNewStoreManager(user1, storename, user2);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            permissionList.Add("EditProduct");
            Assert.IsFalse(UserController.editManagerPermissions(user1,wrongstore, user2, permissionList));
            UserController.logout();
        }

        [TestMethod]
        public void ownerGetInfoEmployeesTestGood()
        {
            string storename = "ogietg_store_1", user1 = "ogietg_user_1", user2 = "ogietg_user_2";
            UserController.register(user1, "qweE1", 99, "male", "ogietg 1");
            UserController.register(user2, "qweE1", 99, "male", "ogietg 2");
            UserController.login(user1, "qweE1");
            UserServices.EstablishStore(user1, storename);
            UserController.hireNewStoreManager(user1, storename, user2);
            UserController.logout();
            UserController.login(user2, "qweE1");
            var emp = UserController.getInfoEmployees(user2, storename);
            bool check = false;
            foreach(SLemployee em in emp)
            {
                if (em.userName.Equals(user2))
                    check = true;
            }
            Assert.IsTrue(check);
            UserController.logout();
        }

        [TestMethod]
        public void ownerGetInfoEmployeesTestBad()
        {
            string storename = "ogietb_store_1", wrongstore = "wrongstore", user1 = "ogietb_user_1", user2 = "ogietb_user_2";
            UserController.register(user1, "qweE1", 99, "male", "ogietb 1");
            UserController.register(user2, "qweE1", 99, "male", "ogietb 2");
            UserController.login(user1, "qweE1");
            UserServices.EstablishStore(user1, storename);
            var ma = UserController.getInfoEmployees(user1, storename);
            Assert.IsNotNull(UserController.getInfoEmployees(user1, storename)); //owner not his own employee
            UserController.hireNewStoreManager(user1, storename, user2);
            var nvn = UserController.getInfoEmployees(user1, wrongstore);
            Assert.IsNull(UserController.getInfoEmployees(user1, wrongstore)); //store doesnt exist
            UserController.logout();
            UserController.login(user2, "qweE1");
            Assert.IsNotNull(UserController.getInfoEmployees(user2,storename)); //no permissions
            UserController.logout();

        }

        [TestMethod]
        public void hireNewStoreOwnerTestGood()
        {
            string storename = "uhnsotg_store_1", user1 = "uhnsotg_user_1", user2 = "uhnsotg_user_2";
            UserController.register(user1, "qweE1", 99, "male", "hnsotg 1");
            UserController.register(user2, "qweE1", 99, "male", "hnsotg 2");
            UserController.login(user1, "qweE1");
            UserServices.EstablishStore(user1, storename);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            permissionList.Add("HireNewStoreManager");
            Assert.IsTrue(UserController.hireNewStoreOwner(user1, storename, user2, permissionList));
            UserController.logout();
        }

        [TestMethod]
        public void hireNewStoreOwnerTestBad()
        {
            string storename = "hnsotb_store_1", wrongstore = "wroongstore", user1 = "hnsotb_user_1", user2 = "hnsotb_user_2", wronguser = "wronguser";
            UserController.register(user1, "qweE1", 99, "male", "hnsotb 1");
            UserController.register(user2, "qweE1", 99, "male", "hnsotb 2");
            UserController.login(user1, "qweE1");
            UserServices.EstablishStore(user1, storename);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            permissionList.Add("HireNewStoreManager");
            Assert.IsFalse(UserController.hireNewStoreOwner(user1, wrongstore, user2, permissionList)); //store doesnt exist
            Assert.IsFalse(UserController.hireNewStoreOwner(user1, storename, wronguser, permissionList)); //user doesnt exist
            UserController.hireNewStoreOwner(user1, storename, user2, permissionList);
            Assert.IsFalse(UserController.hireNewStoreOwner(user1, storename, user2, permissionList)); //hire already hired user
            UserController.logout();
        }

        [TestMethod]
        public void removeManagerTestGood()
        {
            string storename = "rmtg_store_1", user1 = "rmtg_user_1", user2 = "rmtg_user_2";
            UserController.register(user1, "qweE1", 99, "male", "rmtg 1");
            UserController.register(user2, "qweE1", 99, "male", "rmtg 2");
            UserController.login(user1, "qweE1");
            UserServices.EstablishStore(user1, storename);
            UserController.hireNewStoreManager(user1, storename, user2);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            UserController.editManagerPermissions(user1, storename, user2, permissionList);
            Assert.IsTrue(UserController.removeManager(user1, storename, user2));
            UserController.logout();
        }

        [TestMethod]
        public void removeManagerTestBad1()
        {//remove manager you didnt assign
            string storename = "urmtb1_store_1", user1 = "urmtb1_user_1", user2 = "urmtb1_user_2", user3 = "urmtb1_user_3";
            UserController.register(user1, "qweE1", 99, "male", "rmtb 1");
            UserController.register(user2, "qweE1", 99, "male", "rmtb 2");
            UserController.register(user3, "qweE1", 99, "male", "rmtb 3");
            UserController.login(user1, "qweE1");
            UserServices.EstablishStore(user1, storename);
            UserController.hireNewStoreManager(user1, storename, user2);
            UserController.hireNewStoreManager(user1, storename, user3);
            UserController.logout();
            UserController.login(user2, "qweE1");
            Assert.IsFalse(UserController.removeManager(user2, storename, user3));
            UserController.logout();
        }

        [TestMethod]
        public void removeManagerTestBad2()
        {//remove non-manager
            string storename = "rmtb2_store_1", user1 = "rmtb2_user_1", user2 = "rmtb2_user_2";
            UserController.register(user1, "qweE1", 99, "male", "rmtb 4");
            UserController.register(user2, "qweE1", 99, "male", "rmtb 5");
            UserController.login(user1, "qweE1");
            UserServices.EstablishStore(user1, storename);
            Assert.IsFalse(UserController.removeManager(user1, storename, user2)); //remove non employee
            Assert.IsFalse(UserController.removeManager(user1, storename, user1)); //remove self
            UserController.hireNewStoreManager(user1, storename, user2);
            UserController.removeManager(user1, storename, user2);
            Assert.IsFalse(UserController.removeManager(user1, storename, user2)); //remove manager that was already removed
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            UserController.hireNewStoreOwner(user1, storename, user2, permissionList);
            //Assert.IsFalse(UserController.removeManager(user1, storename, user2)); //user remove manager on owner
            UserController.logout();
        }

        [TestMethod]
        public void removeManagerTestBad3()
        {//remove manager from wrong store
            string storename = "rmtb3_store_1", wrongstore = "wrongstore", user1 = "rmtb3_user_1", user2 = "rmtb3_user_2";
            UserController.register(user1, "qweE1", 99, "male", "rmtb 6");
            UserController.register(user2, "qweE1", 99, "male", "rmtb 7");
            UserController.login(user1, "qweE1");
            UserServices.EstablishStore(user1, storename);
            UserController.hireNewStoreManager(user1, storename, user2);
            Assert.IsFalse(UserController.removeManager(user1, wrongstore, user2));
            UserController.logout();
        }

        [TestMethod]
        public void removeOwnerTestGood()
        {
            string storename = "rotg_store_14", user1 = "rotg_user_111", user2 = "rotg_user_222";
            UserController.register(user1, "qweE1", 99, "male", "rotg 1");
            UserController.register(user2, "qweE1", 99, "male", "rotg 2");
            UserController.login(user1, "qweE1");
            UserServices.EstablishStore(user1, storename);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            UserController.hireNewStoreOwner(user1, storename, user2, permissionList);
            Assert.IsTrue(UserController.removeOwner(user1, storename, user2));
            Assert.IsTrue(UserServices.getUser(user1).GetAllPermissions().Keys.Count == 1);
            Assert.IsFalse(StoreController.searchStore(storename).ownerNames.Contains(user2));
            UserController.logout();
        }

        [TestMethod]
        public void removeOwnerTestBad1()
        {//remove owner you didn't assign
            string storename = "rotb1_store_1", user1 = "rotb1_user_1", user2 = "rotb1_user_2", user3 = "rotb1_user_3";
            UserController.register(user1, "qweE1", 99, "male", "rotb 1");
            UserController.register(user2, "qweE1", 99, "male", "rotb 2");
            UserController.register(user3, "qweE1", 99, "male", "rotb 3");
            UserController.login(user1, "qweE1");
            UserServices.EstablishStore(user1, storename);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            UserController.hireNewStoreOwner(user1, storename, user2, permissionList);
            UserController.hireNewStoreOwner(user1, storename, user3, permissionList);
            UserController.logout();
            UserController.login(user2, "qweE1");
            Assert.IsFalse(UserController.removeOwner(user2, storename, user3));
            UserController.logout();
        }

        [TestMethod]
        public void removeOwnerTestBad2()
        {//remove non-owner
            string storename = "rotb2_store_12", user1 = "rotb2_user_12", user2 = "rotb2_user_21";
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            UserController.register(user1, "qweE1", 99, "f", "rotb 4");
            UserController.register(user2, "qweE1", 99, "f", "rotb 5");
            UserController.login(user1, "qweE1");
            UserServices.EstablishStore(user1, storename);
            Assert.IsFalse(UserController.removeOwner(user1, storename, user2)); //remove non employee
            Assert.IsFalse(UserController.removeOwner(user1, storename, user1)); //remove self
            bool t = UserController.hireNewStoreOwner(user1, storename, user2, permissionList);
            bool r = UserController.removeOwner(user1, storename, user2);
            Assert.IsFalse(UserController.removeOwner(user1, storename, user2)); //remove manager that was already removed
            UserController.hireNewStoreManager(user1, storename, user2);
            Assert.IsFalse(UserController.removeOwner(user1, storename, user2)); //user remove owner on manager
            UserController.logout();
        }

        [TestMethod]
        public void removeOwnerTestBad3()
        {//remove owner from wrong store
            string storename = "rotb3_store_1", wrongstore = "wrongstore", user1 = "rotb3_user_1", user2 = "rotb3_user_2";
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            UserController.register(user1, "qweE1", 99, "f", "rotb 6");
            UserController.register(user2, "qweE1", 99, "f", "rotb 7");
            UserController.login(user1, "qweE1");
            UserServices.EstablishStore(user1, storename);
            UserController.hireNewStoreOwner(user1, storename, user2, permissionList);
            Assert.IsFalse(UserController.removeOwner(user1, wrongstore, user2));
            UserController.logout();
        }


        [TestMethod]
        public void noPermissionTest()
        {
            string storename = "npt_store_1", user1 = "npt_user_1", user2 = "npt_user_2";
            string productName = "bamba", productManuf = "osem";
            UserController.register(user1, "qweE1", 99, "male", "rotb 1");
            UserController.register(user2, "qweE1", 99, "male", "rotb 2");
            UserController.login(user1, "qweE1");
            UserServices.EstablishStore(user1, storename);
            UserController.logout();
            UserController.login(user2, "qweE1");
            Assert.IsFalse(UserController.addNewProduct(user2, storename, productName, 5.9, 5, "snacks", productManuf));
            Assert.IsFalse(StoreController.searchStore(storename).inventory.Count == 1);
            UserController.logout();
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
            UserServices.register(username, pass);
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
            string storeOwnerName = "Owner-ucsrg", storeOwnerPass = "123Xx456";
            string storeName = "store-ucstg";
            UserController.register(storeOwnerName, storeOwnerPass, 99, "male", "address1");
            UserServices.login(storeOwnerName, storeOwnerPass);
            aUser storeOwner = UserServices.getUser(storeOwnerName);
            Assert.IsTrue(Stores.addStore(storeName, (Member)storeOwner), "failed to create store with valid and new name");
            UserController.logout();
        }

        [TestMethod]
        public void createStoreTestBad()
        {
            string storeOwnerName = "Owner-icstb", storeOwnerPass = "123Xx456";
            string storeName = "createStoreTestBad_store1";
            UserController.register(storeOwnerName, storeOwnerPass, 99, "male", "address1");
            UserServices.login(storeOwnerName, storeOwnerPass);
            aUser storeOwner = UserServices.getUser(storeOwnerName);
            Stores.addStore(storeName, (Member)storeOwner);
            Store store = Stores.searchStore(storeName);
            Assert.IsFalse(Stores.addStore(null, (Member)storeOwner), "managed to create a store with a null name");
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
            string userNameAdd = "userAdd1"; string passwordAdd = "abcdE12"; string storeNameAdd1 = "storeAddTest1"; string storeNameAdd2 = "storeAddTest2";
            UserController.register(userNameAdd, passwordAdd, 21, "m", "address");
            UserController.login(userNameAdd, passwordAdd);
            UserServices.EstablishStore(userNameAdd, storeNameAdd1);
            UserServices.EstablishStore(userNameAdd, storeNameAdd2);
            // add new ProductInfo
            ProductInfo p = ProductInfo.getProductInfo("productX2", "categoryX2", "ManufacturerX2");
            ProductInfo p1 = ProductInfo.getProductInfo("productY2", "categoryY2", "ManufacturerY2");
            ProductInfo p2 = ProductInfo.getProductInfo("productZ2", "categoryZ2", "ManufacturerZ2");
            // add products to stores
            Stores.searchStore(storeNameAdd1).addProduct(p1.name, p1.category, p1.manufacturer);
            Stores.searchStore(storeNameAdd1).addProduct(p2.name, p2.category, p2.manufacturer);
            Stores.searchStore(storeNameAdd2).addProduct(p1.name, p1.category, p1.manufacturer);
            // assert that the products exist
            Assert.IsTrue(Stores.searchStore(storeNameAdd1).isProductExist(p1.name, p1.manufacturer));
            Assert.IsTrue(Stores.searchStore(storeNameAdd1).isProductExist(p2.name, p2.manufacturer));
            Assert.IsTrue(Stores.searchStore(storeNameAdd2).isProductExist(p1.name, p1.manufacturer));
            Assert.IsFalse(Stores.searchStore(storeNameAdd2).isProductExist(p2.name, p2.manufacturer));
            // clean the stores
            Stores.searchStore(storeNameAdd1).removeProduct(p1.name, p1.manufacturer);
            Stores.searchStore(storeNameAdd1).removeProduct(p2.name, p2.manufacturer);
            Stores.searchStore(storeNameAdd2).removeProduct(p1.name, p1.manufacturer);
            // assert that the products were removed
            Assert.IsFalse(Stores.searchStore(storeNameAdd1).isProductExist(p1.name, p1.manufacturer));
            Assert.IsFalse(Stores.searchStore(storeNameAdd1).isProductExist(p2.name, p2.manufacturer));
            Assert.IsFalse(Stores.searchStore(storeNameAdd2).isProductExist(p1.name, p1.manufacturer));
        }

        [TestMethod]
        public void supplyTestGood()
        {
            string userNameSupply = "userSupply1"; string passwordSupply = "abcdE12"; string storeNameSupply1 = "storeSupplyTest1"; string storeNameSupply2 = "storeSupplyTest2";
            UserController.register(userNameSupply, passwordSupply, 21, "m", "address");
            UserController.login(userNameSupply, passwordSupply);
            UserServices.EstablishStore(userNameSupply, storeNameSupply1);
            UserServices.EstablishStore(userNameSupply, storeNameSupply2);
            // add the products to the stores
            Stores.searchStore(storeNameSupply1).addProduct(product1.name, product1.category, product1.manufacturer);
            Stores.searchStore(storeNameSupply1).addProduct(product2.name, product2.category, product2.manufacturer);
            Stores.searchStore(storeNameSupply2).addProduct(product1.name, product1.category, product1.manufacturer);
            Stores.searchStore(storeNameSupply2).addProduct(product2.name, product2.category, product2.manufacturer);
            // supply store1
            Stores.searchStore(storeNameSupply1).supply(product1.name, product1.manufacturer, 25);
            Stores.searchStore(storeNameSupply1).supply(product2.name, product2.manufacturer, 30);
            // supply store2
            Stores.searchStore(storeNameSupply2).supply(product1.name, product1.manufacturer, 10);
            Stores.searchStore(storeNameSupply2).supply(product2.name, product2.manufacturer, 40);
            // check amounts
            // store1
            Assert.AreEqual(Stores.searchStore(storeNameSupply1).searchProduct(product1.name, product1.manufacturer).amount, 25);
            Assert.AreEqual(Stores.searchStore(storeNameSupply1).searchProduct(product2.name, product2.manufacturer).amount, 30);
            // store2
            Assert.AreEqual(Stores.searchStore(storeNameSupply2).searchProduct(product1.name, product1.manufacturer).amount, 10);
            Assert.AreEqual(Stores.searchStore(storeNameSupply2).searchProduct(product2.name, product2.manufacturer).amount, 40);
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
            string buyerUsername = "AddFeedBackUser", newPass = "123Xx321";
            string FOUsername = "FBObserve", FOPass = "123Xx123"; // FO: feedback observer
            // register the users
            UserServices.register(ownerUsername, ownerPass, 12, "f", "some address");
            UserServices.register(buyerUsername, newPass, 12, "f", "some address");
            UserServices.register(FOUsername, FOPass, 12, "f", "some address");

            string storeName = "Ali_Shop121FeedBackTest";

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
            string storename = "ppsb_store_1", user1 = "Ppsb_user_1";
            string productName = "bamba", productManuf = "osem";
            UserController.register(user1, user1, 99, "male", "ppsb 1");
            UserController.login(user1, user1);
            UserServices.EstablishStore(user1, storename);
            UserController.addNewProduct(user1, storename, productName, 5.5, 100, "snacks", productManuf);
            Dictionary<string, int> products = new Dictionary<string, int>();
            products.Add(productName, 1);
            UserController.saveProduct(user1, storename, productManuf, products);
            //var y = UserController.browseStore(user1, storename);
            //var t = UserController.purchase(user1, "abcd", "abcd", "abcd");
            Assert.IsTrue(UserController.purchase(user1, "abcd", "abcd", "abcd")[1].Equals("payment not approved"));
            UserController.logout();
        }

        [TestMethod]
        public void purchaseSupplySystemBad()
        {
            string storename = "pssb_store_1", user1 = "Pssb_user_1";
            string productName = "bamba", productManuf = "osem";
            UserController.register(user1, user1, 99, "male", "");
            UserController.login(user1, user1);
            UserServices.EstablishStore(user1, storename);
            UserController.addNewProduct(user1, storename, productName, 5.5, 100, "snacks", productManuf);
            Dictionary<string, int> products = new Dictionary<string, int>();
            products.Add(productName, 1);
            UserController.saveProduct(user1, storename, productManuf, products);
            Assert.IsTrue(UserController.purchase(user1, "1234567812345678", "01/99", "111")[1].Equals("supply not approved"));
            UserController.logout();
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
            Assert.IsTrue(passTest);
            UserServices.logout(ownerName1);
        }

        [TestMethod]
        public void addProductPermissionBad()
        {
            UserServices.login(ownerName2, ownerPassword2);
            Member owner2 = (Member)UserServices.getUser(ownerName2);

            bool wrongStore = owner2.addNewProduct(storeName1, p1.name, price1, amount1, p1.category, p1.manufacturer);
            Assert.IsFalse(wrongStore);//did not establish this store, and is not at any managment position. has no permissions at all.
            UserController.hireNewStoreManager(ownerName2, storeName2, ownerName1);
            //owner2.removePermission(storeName2, null);//removes all permissions from the user
            bool noPermission = ((Member)UserServices.getUser(ownerName1)).addNewProduct(storeName2, p2.name, price2, amount2, p2.category, p2.manufacturer);
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
                double newPrice = 6.99;

                Assert.IsTrue(owner1.editProduct(storeName1, p3.name, newPrice, p3.manufacturer), "failed edit product");
            }

            UserServices.logout(ownerName1);

        }

        [TestMethod]
        public void editProductPermissionBad()
        {
            UserServices.login(ownerName2, ownerPassword2);
            Member owner2 = (Member)UserServices.getUser(ownerName2);

            ProductInfo p5 = ProductInfo.getProductInfo("uerror", "unone", "uempty");
            double newPrice = 1.99;
            bool passTest = owner2.editProduct(storeName2, p5.name, newPrice, p5.manufacturer);

            Assert.IsFalse(passTest);//product does not exist in the inventory

            passTest = owner2.editProduct(storeName1, p1.name, newPrice, p1.manufacturer);

            Assert.AreEqual(Stores.searchStore(storeName1).searchProduct(p1.name, p1.manufacturer).price, price1);//price should not change in case of unauthorized use.

            UserServices.logout(ownerName2);
        }

        [TestMethod]
        public void removeProductPermissionGood()
        {
            UserServices.login(ownerName1, ownerPassword1);
            Member owner1 = (Member)UserServices.getUser(ownerName1);
            bool passTest = owner1.removeProduct(storeName1, pToRemove.name, pToRemove.manufacturer);

            Assert.IsTrue(owner1.removeProduct(storeName1, pToRemove.name, pToRemove.manufacturer));
            UserServices.logout(ownerName1);
        }

        [TestMethod]
        public void removeProductPermissionBad()
        {
            string ownerNameRemovePermission = "ownerRemovePerm"; string PasswordRemovePermission = "abcD1234"; string storeNameRemove = "storeRemovePerm";
            UserController.register(ownerNameRemovePermission, PasswordRemovePermission, 21, "f", "address");
            UserServices.login(ownerName1, ownerPassword1);
            Member owner2 = (Member)UserServices.getUser(ownerNameRemovePermission);
            UserServices.EstablishStore(ownerName1, storeNameRemove);
            Stores.searchStore(storeNameRemove).addProduct(p1.name, p1.category, p1.manufacturer);
            Stores.searchStore(storeNameRemove).supply(p1.name, p1.manufacturer, 15);
            UserController.logout();
            UserServices.login(ownerNameRemovePermission, PasswordRemovePermission);
            Member notOwner = (Member)UserServices.getUser(ownerNameRemovePermission);
            bool passTest = notOwner.removeProduct(storeNameRemove, p1.name, p1.manufacturer);
            Assert.AreEqual(Stores.searchStore(storeNameRemove).searchProduct(p1.name, p1.manufacturer).amount, 15);
            UserController.logout();
        }

        [TestMethod]
        public void HireNewStoreManagerBad()
        {

        }

    }

    [TestClass]
    public class PolicyTests
    {
        [TestMethod]
        public void ProductAgePolicyGood()
        {
            // init usernames and passes
            string storeName1 = "ustore_ProductAgePolicyGood";
            string ownerUsername = "uowner_001", ownerPass = "u123Xx123";
            string buyerUsername = "unoOne_001", newPass = "u123Xx321";
            // init products info
            string p1_name = "uBamba", p1_man = "uOsem", p1_cat = "uFood";
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
            store1.supply(p1_name, p1_man, 20);
            store1.addAgePolicyByProduct(p1_name, p1_cat, p1_man, 18);
            Assert.IsTrue(store1.purchasePolicies.Count == 1);   
        }

        [TestMethod]
        public void CategoryAgePolicyGood() {
            // init usernames and passes
            string storeName1 = "ustore_CategoryAgePolicyGood";
            string ownerUsername = "uowner_003", ownerPass = "u123Xx123";
            string buyerUsername = "unoOne_003", newPass = "u123Xx321";
            // init products info
            string p1_name = "uCorona Beer", p1_man = "uCorona", p1_cat = "ualcohol";
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

            Assert.IsTrue(store1.purchasePolicies.Count == 1);
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
            string storeName1 = "ustore_MaxAmountPolicyGood";
            string ownerUsername = "uowner_005", ownerPass = "u123Xx123";
            string buyerUsername = "unoOne_005", newPass = "u123Xx321";
            // init products info
            string p1_name = "uiPhone 8", p1_man = "uApple", p1_cat = "uphones";
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

            Assert.AreEqual(store1.purchasePolicies.Count,1);
        }

        [TestMethod]
        public void CategoryMaxAmountPolicyGood() {
            // init usernames and passes
            string storeName1 = "ustore_CategoryMaxAmountPolicyGood";
            string ownerUsername = "uowner_007", ownerPass = "u123Xx123";
            string buyerUsername = "unoOne_007", newPass = "u123Xx321";
            // init products info
            string p1_name = "uiPhone 8", p1_man = "uApple", p1_cat = "uphones";
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

            Assert.IsTrue(store1.purchasePolicies.Count == 1);
        }
    }


    [TestClass]
    public class DiscountPolicyTests
    {


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
        public void SingleDiscountBambaGood()
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
            store1.editPrice(p1_name, p1_man, 10);
            // supply 
            store1.supply(p1_name, p1_man, 20);


            store1.addSingleDiscountPolicyByProduct(p1_name, p1_cat, p1_man, DateTime.MaxValue, 10);
          
            UserServices.login(buyerUsername, newPass);
            aUser client = UserServices.getUser(buyerUsername);

            // add the product to the basket
            client.getCart().getBasket(store1).addProduct(new Product(ProductInfo.getProductInfo(p1_name, p1_cat, p1_man), 1, 0));

            // purchase
            string[] receipts1 = client.purchase("111111111111", "11/22", "123");

            Assert.IsTrue(receipts1[0].Equals("true") || !receipts1[1].Equals("Policy err"), "couldn't manage to buy bamba with age = 18");

            // check for amounts in the basket and in the store
            if (receipts1[0].Equals("true"))
            {
                Receipt actualReceipt = convertReceipt(receipts1[1]);
                Assert.AreEqual(actualReceipt.price, 9, "expected 9 the price");
            }

        }
    }

    }

