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
using TradingSystem.BuissnessLayer.commerce.Rules.DicountPolicy;
using TradingSystem.BuissnessLayer.commerce.Rules.Policy;

namespace Tests
{
    [TestClass]
    public class UserAccessUnitTest
    {
        [ClassInitialize]
        public static void classInit(TestContext context)
        {
            UserServices.register("admin", "adminPass123", 45, "male", "address1");
            /*
            bridge = Driver.getBridge();
            bridge.register("user1", "Password1");
            bridge.register("user2", "Password2");
            */
            //UserController.register("admin", "Admin1");
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
            string username = "admin", pass = "adminPass123";
            string[] user = UserController.login(username, pass);
            Assert.IsTrue(UserController.getUserName().Equals("admin"));
        }

        [TestMethod]
        public void establishStoreTestGood()
        {
            string storename1 = "estg_store_1";
            string user1 = "estg_user_1";
            UserController.register(user1, "qweE1", 99, "male", "estg 1");
            UserController.login(user1, "qweE1");
            Assert.IsTrue(UserController.EstablishStore(user1, storename1));
            Assert.IsNotNull(StoreController.searchStore(storename1));
            SLstore store = StoreController.searchStore(storename1);
            Assert.IsTrue(store.founderName.Equals(user1));
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
            UserController.EstablishStore(user1, storename1);
            Assert.IsFalse(UserController.EstablishStore(user1, storename1));
            UserController.logout();
            UserController.login(user2, "qweE1");
            Assert.IsFalse(UserController.EstablishStore(user2, storename1));
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
            UserController.EstablishStore(user1, storename);
            UserController.hireNewStoreManager(user1, storename, user2);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            permissionList.Add("EditProduct");
            UserController.editManagerPermissions(user1, storename, user2, permissionList);
            var temp = ((Member)UserServices.getUser(user1)).GetPermissions(storename);
            Assert.IsTrue(UserController.addNewProduct(user1, storename, "bamba", 5.9, 5, "snacks", "osem"));
            Assert.IsTrue(UserController.editProduct(user1, storename, "bamba", 5.5, "osem"));
            Assert.IsTrue(UserController.editManagerPermissions(user1, storename, user2, permissionList));
            UserController.logout();
        }

        [TestMethod]
        public void editManagerPermissionsTestBad1()
        {//edit permissions of unhired member
            string storename = "emptb1_store_1", user1 = "emptb1_user_1", user2 = "emptb1_user_2";
            UserController.register(user1, "qweE1", 99, "male", "emptb 1");
            UserController.login(user1, "qweE1");
            UserController.EstablishStore(user1, storename);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            permissionList.Add("EditProduct");
            UserController.editManagerPermissions(user1, storename, user2, permissionList);
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
            UserController.EstablishStore(owner1, storename);
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
            UserController.EstablishStore(owner1, storename);
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
            UserController.EstablishStore(user1, storename);
            UserController.hireNewStoreManager(user1, storename, user2);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            permissionList.Add("EditProduct");
            Assert.IsFalse(UserController.editManagerPermissions(user1, wrongstore, user2, permissionList));
            UserController.logout();
        }

        [TestMethod]
        public void ownerGetInfoEmployeesTestGood()
        {
            string storename = "ogietg_store_1", user1 = "ogietg_user_1", user2 = "ogietg_user_2";
            UserController.register(user1, "qweE1", 99, "male", "ogietg 1");
            UserController.register(user2, "qweE1", 99, "male", "ogietg 2");
            UserController.login(user1, "qweE1");
            UserController.EstablishStore(user1, storename);
            UserController.hireNewStoreManager(user1, storename, user2);
            UserController.logout();
            UserController.login(user2, "qweE1");
            var emp = UserController.getInfoEmployees(user2, storename);
            bool check = false;
            foreach (SLemployee em in emp)
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
            UserController.EstablishStore(user1, storename);
            var ma = UserController.getInfoEmployees(user1, storename);
            Assert.IsNotNull(UserController.getInfoEmployees(user1, storename)); //owner not his own employee
            UserController.hireNewStoreManager(user1, storename, user2);
            var nvn = UserController.getInfoEmployees(user1, wrongstore);
            Assert.IsNull(UserController.getInfoEmployees(user1, wrongstore)); //store doesnt exist
            UserController.logout();
            UserController.login(user2, "qweE1");
            Assert.IsNotNull(UserController.getInfoEmployees(user2, storename)); //no permissions
            UserController.logout();

        }

        [TestMethod]
        public void hireNewStoreOwnerTestGood()
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
            Assert.IsTrue(UserServices.getUser(user2).GetAllPermissions()[storename].Contains("AddProduct"));
            Assert.IsTrue(UserServices.getUser(user2).GetAllPermissions()[storename].Contains("HireNewStoreManager"));
            Assert.IsTrue(UserServices.getUser(user2).GetAllPermissions()[storename].Count == 2);
            UserController.logout();
        }

        [TestMethod]
        public void hireNewStoreOwnerTestBad()
        {
            string storename = "hnsotb_store_1", wrongstore = "wroongstore", user1 = "hnsotb_user_1", user2 = "hnsotb_user_2", wronguser = "wronguser";
            UserController.register(user1, "qweE1", 99, "male", "hnsotb 1");
            UserController.register(user2, "qweE1", 99, "male", "hnsotb 2");
            UserController.login(user1, "qweE1");
            UserController.EstablishStore(user1, storename);
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
            UserController.EstablishStore(user1, storename);
            UserController.hireNewStoreManager(user1, storename, user2);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            UserController.editManagerPermissions(user1, storename, user2, permissionList);
            Assert.IsTrue(UserController.removeManager(user1, storename, user2));
            var jf = UserServices.getUser(user2).GetAllPermissions().Keys;
            var tt = UserController.getInfoEmployees(user1, storename);
            Assert.IsTrue(UserServices.getUser(user2).GetAllPermissions().Keys.Count == 1);
            Assert.IsFalse(UserController.getInfoEmployees(user1, storename).Count == 0);
            UserController.logout();
        }

        [TestMethod]
        public void removeManagerTestBad1()
        {//remove manager you didnt assign
            string storename = "rmtb1_store_1", user1 = "rmtb1_user_1", user2 = "rmtb1_user_2", user3 = "rmtb1_user_3";
            UserController.register(user1, "qweE1", 99, "male", "rmtb 1");
            UserController.register(user2, "qweE1", 99, "male", "rmtb 2");
            UserController.register(user3, "qweE1", 99, "male", "rmtb 3");
            UserController.login(user1, "qweE1");
            UserController.EstablishStore(user1, storename);
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
            UserController.EstablishStore(user1, storename);
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
            UserController.EstablishStore(user1, storename);
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
            UserController.EstablishStore(user1, storename);
            List<string> permissionList = new List<string>();
            permissionList.Add("AddProduct");
            UserController.hireNewStoreOwner(user1, storename, user2, permissionList);
            Assert.IsTrue(UserController.removeOwner(user1, storename, user2));
            var gg = UserServices.getUser(user1).GetAllPermissions();
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
            UserController.EstablishStore(user1, storename);
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
            UserController.EstablishStore(user1, storename);
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
            UserController.EstablishStore(user1, storename);
            UserController.hireNewStoreOwner(user1, storename, user2, permissionList);
            Assert.IsFalse(UserController.removeOwner(user1, wrongstore, user2));
            UserController.logout();
        }

        [TestMethod]
        public void removeOwnerTestDeep()
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
            Assert.IsTrue(UserController.removeOwner(user1, storename, user2));
            var t = UserController.browseStore(user1, storename);
            var g = UserController.getInfoEmployees(user1, storename);
            Assert.IsTrue(UserController.getInfoEmployees(user1, storename).Count == 1);
            Assert.IsFalse(StoreController.searchStore(storename).ownerNames.Contains(user2));
            Assert.IsFalse(StoreController.searchStore(storename).managerNames.Contains(user3));
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
            UserController.EstablishStore(user1, storename);
            UserController.logout();
            UserController.login(user2, "qweE1");
            var ma = UserServices.getUser(user2).GetAllPermissions();
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

        public ICollection<Receipt> convertReceiptsArray(string[] receiptsString)
        {
            ICollection<Receipt> receipts = new LinkedList<Receipt>();
            // receiptsString[0] contains the answer
            for (int i = 1; i < receiptsString.Length; i++)
                receipts.Add(convertReceipt(receiptsString[i]));

            return receipts;
        }

        public Receipt convertReceipt(string receiptString)
        {
            Receipt receipt = new Receipt();

            string[] splitReceipt = receiptString.Split('$');
            // username&storename$price$date$receiptId$<products>
            //receipt.user.userName = splitReceipt[0];
            // receipt.store = Stores.searchStore(splitReceipt[1]); //changed
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
            string storename = "ppsb_store_1", user1 = "Ppsb_user_1";
            string productName = "bamba", productManuf = "osem";
            UserController.register(user1, user1, 99, "male", "ppsb 1");
            UserController.login(user1, user1);
            UserController.EstablishStore(user1, storename);
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
            UserController.EstablishStore(user1, storename);
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
        string username1 = "AliKB12", username2 = "Bader12", pass1 = "123xX456", pass2 = "456xX789";
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
            //string username1 = "almog";
            //string username2 = "almo";
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
                Assert.AreEqual(receipt.user.userName, username1, "the username is wrong");
                if (receipt.store.name.Equals(storeName) & receipt.basket.products.Count == 1)
                {
                    LinkedList<Receipt> rAsList = new LinkedList<Receipt>(u1Receipts);
                    LinkedList<Product> products = new LinkedList<Product>();
                    foreach (var product in rAsList.First.Value.basket.products)
                        products.AddLast(new Product(product));
                    if (products.First.Value.info.Equals(ProductInfo.getProductInfo("Bamba", "Food", "Osem")) & products.First.Value.amount == 18)
                    {
                        user1HasReceipt = true;
                    }
                }
            }
            // check for user 2
            foreach (Receipt receipt in u2Receipts)
            {
                Assert.AreEqual(receipt.user.userName, username2, "the username is wrong");
                if (receipt.store.name.Equals(storeName) & receipt.basket.products.Count == 1)
                {
                    LinkedList<Receipt> rAsList = new LinkedList<Receipt>(u2Receipts);
                    LinkedList<Product> products = new LinkedList<Product>();
                    foreach (var product in rAsList.First.Value.basket.products)
                        products.AddLast(new Product(product));
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
                Assert.AreNotEqual(receipt.user.userName, username2, "user1 got user2's receipt");

            foreach (Receipt receipt in u2Receipts)
                Assert.AreNotEqual(receipt.user.userName, username1, "user2 got user1's receipt");
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
                Assert.AreEqual(receipt.basket.products.Count, 1);
                Assert.IsTrue(receipt.user.userName.Equals(username1) | receipt.user.userName.Equals(username2));

                if (receipt.user.userName.Equals(username1))
                {
                    LinkedList<Receipt> rAsList = new LinkedList<Receipt>(u1Receipts);
                    LinkedList<Product> products = new LinkedList<Product>();
                    foreach (var product in rAsList.First.Value.basket.products)
                        products.AddLast(new Product(product));
                    if (products.First.Value.info.Equals(ProductInfo.getProductInfo("Bamba", "Food", "Osem")))
                    {
                        user1HasReceipt = true;
                    }
                }

                if (receipt.user.userName.Equals(username2))
                {
                    LinkedList<Receipt> rAsList = new LinkedList<Receipt>(u1Receipts);
                    LinkedList<Product> products = new LinkedList<Product>();
                    foreach (var product in rAsList.First.Value.basket.products)
                        products.AddLast(new Product(product));
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
                LinkedList<Product> products = new LinkedList<Product>();
                foreach (var product in adminReceipts.First.Value.basket.products)
                    products.AddLast(new Product(product));
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
            //receipt.user.userName = splitReceipt[0];
            //receipt.store = Stores.searchStore(splitReceipt[1]); //changed
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
            string ownerUsername = "owner_009", ownerPass = "123Xx123";
            string buyerUsername = "noOne_009", newPass = "123Xx321";
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
                Assert.AreEqual(Stores.searchStore(storeName1).searchProduct(p1_name, p1_man).amount, 17, "expected 8 products after purchase.");
            }
        }

        [TestMethod]
        public void ProductMaxAmountPolicyBad()
        {
            // init usernames and passes
            string storeName1 = "store_CategoryAgePolicyGood";
            string ownerUsername = "owner_008", ownerPass = "123Xx123";
            string buyerUsername = "noOne_008", newPass = "123Xx321";
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
        public void CategoryMaxAmountPolicyBad()
        {
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
            //receipt.user.userName = splitReceipt[0];
            //receipt.store = Stores.searchStore(splitReceipt[1]); //changed
            receipt.price = double.Parse(splitReceipt[2]);
            receipt.date = Convert.ToDateTime(splitReceipt[3]);
            receipt.receiptId = int.Parse(splitReceipt[4]);
            // the products - todo


            return receipt;
        }

        [TestMethod]
        public void BaseDiscountPolicyByProductGood()
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


            store1.addSingleDiscountPolicyByProduct(p1_name, p1_cat, p1_man, DateTime.MaxValue, 0.1);

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

        [TestMethod]
        public void BaseDiscountPolicyByProductBad()
        {
            // init usernames and passes
            string storeName1 = InfoGenerator.generateValidStoreName();
            string ownerUsername = InfoGenerator.generateValidUsername(),
                       ownerPass = InfoGenerator.generateValidPassword();
            string buyerUsername = InfoGenerator.generateValidUsername(),
                         newPass = InfoGenerator.generateValidPassword();
            // init products info
            string p1_name = "Bamba", p1_man = "Osem", p1_cat = "Food",
                p2_name = "Bamba - no discount", p2_man = "Osem", p2_cat = "Food";
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
            store1.addProduct(p2_name, p2_cat, p2_man);
            // set the price of the products
            store1.editPrice(p1_name, p1_man, 10);
            store1.editPrice(p2_name, p2_man, 10);
            // supply 
            store1.supply(p1_name, p1_man, 20);
            store1.supply(p2_name, p2_man, 20);


            store1.addSingleDiscountPolicyByProduct(p1_name, p1_cat, p1_man, DateTime.MaxValue, 0.1);

            UserServices.login(buyerUsername, newPass);
            aUser client = UserServices.getUser(buyerUsername);

            // add the product to the basket
            client.getCart().getBasket(store1).addProduct(new Product(ProductInfo.getProductInfo(p2_name, p2_cat, p2_man), 1, 0));

            // purchase
            string[] receipts1 = client.purchase("111111111111", "11/22", "123");

            Assert.IsTrue(receipts1[0].Equals("true") || !receipts1[1].Equals("Policy err"), "couldn't manage to buy bamba with age = 18");

            // check for amounts in the basket and in the store
            if (receipts1[0].Equals("true"))
            {
                Receipt actualReceipt = convertReceipt(receipts1[1]);
                Assert.AreEqual(actualReceipt.price, 10, "Got discount on wrong product.");
            }
        }

        [TestMethod]
        public void BaseDiscountPolicyByCategoryBad()
        {
            // init usernames and passes
            string storeName1 = InfoGenerator.generateValidStoreName();
            string ownerUsername = InfoGenerator.generateValidUsername(),
                       ownerPass = InfoGenerator.generateValidPassword();
            string buyerUsername = InfoGenerator.generateValidUsername(),
                         newPass = InfoGenerator.generateValidPassword();
            // init products info
            string p1_name = "Bamba", p1_man = "Osem", p1_cat = "Food",
                p2_name = "Bamba - no discount", p2_man = "Osem", p2_cat = "Food-no dis";
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
            store1.addProduct(p2_name, p2_cat, p2_man);
            // set the price of the products
            store1.editPrice(p1_name, p1_man, 10);
            store1.editPrice(p2_name, p2_man, 10);
            // supply 
            store1.supply(p1_name, p1_man, 20);
            store1.supply(p2_name, p2_man, 20);


            store1.addSingleDiscountPolicyByProduct(p1_name, p1_cat, p1_man, DateTime.MaxValue, 0.1);

            UserServices.login(buyerUsername, newPass);
            aUser client = UserServices.getUser(buyerUsername);

            // add the product to the basket
            client.getCart().getBasket(store1).addProduct(new Product(ProductInfo.getProductInfo(p2_name, p2_cat, p2_man), 1, 0));

            // purchase
            string[] receipts1 = client.purchase("111111111111", "11/22", "123");

            Assert.IsTrue(receipts1[0].Equals("true") || !receipts1[1].Equals("Policy err"), "couldn't manage to buy bamba with age = 18");

            // check for amounts in the basket and in the store
            if (receipts1[0].Equals("true"))
            {
                Receipt actualReceipt = convertReceipt(receipts1[1]);
                Assert.AreEqual(actualReceipt.price, 10, "Got a discount on the wrong category.");
            }
        }

        [TestMethod]
        public void BaseDiscountPolicyByCategoryGood()
        {
            // init usernames and passes
            string storeName1 = InfoGenerator.generateValidStoreName();
            string ownerUsername = InfoGenerator.generateValidUsername(),
                       ownerPass = InfoGenerator.generateValidPassword();
            string buyerUsername = InfoGenerator.generateValidUsername(),
                         newPass = InfoGenerator.generateValidPassword();
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


            store1.addSingleDiscountPolicyByProduct(p1_name, p1_cat, p1_man, DateTime.MaxValue, 0.1);

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
                Assert.AreEqual(actualReceipt.price, 9, "expected 1 shekel discount.");
            }
        }

        [TestMethod]
        public void ConditionedDiscountByAmountAndProductGood()
        {
            // init usernames and passes
            string storeName1 = InfoGenerator.generateValidStoreName();
            string ownerUsername = InfoGenerator.generateValidUsername(),
                       ownerPass = InfoGenerator.generateValidPassword();
            string buyerUsername = InfoGenerator.generateValidUsername(),
                         newPass = InfoGenerator.generateValidPassword();
            // init products info
            string p1_name = "Bamba", p1_man = "Osem", p1_cat = "Food",
                p2_name = "Bamba2", p2_man = "Osem", p2_cat = "Food";
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
            store1.addProduct(p2_name, p2_cat, p2_man);
            // set the price of the products
            store1.editPrice(p1_name, p1_man, 10);
            store1.editPrice(p2_name, p2_man, 10);
            // supply 
            store1.supply(p1_name, p1_man, 20);
            store1.supply(p2_name, p2_man, 20);

            // create a new conditioned policy
            ConditioningPolicyDiscount condition = DiscountPoliciesGenerator.generateMinProductsCondition(p1_name, p1_cat, p1_man, 5);
            iPolicyDiscount discountPolicy = DiscountPoliciesGenerator.generateDiscountPolicyByProduct(p1_name, p2_cat, p2_man, 0.1, DateTime.MaxValue);
            // add the condition to the policy, and the policy to the store
            discountPolicy.addCondition(condition);
            store1.addDiscountPolicy(discountPolicy);


            UserServices.login(buyerUsername, newPass);
            aUser client = UserServices.getUser(buyerUsername);

            // add the product to the basket
            client.getCart().getBasket(store1).addProduct(new Product(ProductInfo.getProductInfo(p2_name, p2_cat, p2_man), 1, 0));
            client.getCart().getBasket(store1).addProduct(new Product(ProductInfo.getProductInfo(p1_name, p1_cat, p1_man), 5, 0));

            // purchase
            string[] receipts1 = client.purchase("111111111111", "11/22", "123");

            Assert.IsTrue(receipts1[0].Equals("true") || !receipts1[1].Equals("Policy err"), "couldn't manage to buy bamba with age = 18");

            // check for amounts in the basket and in the store
            if (receipts1[0].Equals("true"))
            {
                Receipt actualReceipt = convertReceipt(receipts1[1]);
                Assert.AreEqual(actualReceipt.price, 59, "expected 1 shekel discount.");
            }
        }

        [TestMethod]
        public void BasicDiscountByProductG()
        {
            // init usernames and passes
            string storeName1 = InfoGenerator.generateValidStoreName();
            string ownerUsername = InfoGenerator.generateValidUsername(),
                       ownerPass = InfoGenerator.generateValidPassword();
            string buyerUsername = InfoGenerator.generateValidUsername(),
                         newPass = InfoGenerator.generateValidPassword();
            // init products info
            string p1_name = "Bamba", p1_man = "Osem", p1_cat = "Food",
                p2_name = "Bamba2", p2_man = "Osem", p2_cat = "Food";

            iPolicyDiscount discountPolicy1 = DiscountPoliciesGenerator.generateDiscountPolicyByProduct(p1_name, p1_cat, p1_man, 0.1, DateTime.MaxValue);
            iPolicyDiscount discountPolicy2 = DiscountPoliciesGenerator.generateDiscountPolicyByProduct(p2_name, p2_cat, p2_man, 0.1, DateTime.MaxValue);
            Product p1 = new Product(ProductInfo.getProductInfo(p1_name, p1_cat, p1_man), 1, 10);


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
            store1.addProduct(p2_name, p2_cat, p2_man);
            // set the price of the products
            store1.editPrice(p1_name, p1_man, 10);
            store1.editPrice(p2_name, p2_man, 10);
            // supply 
            store1.supply(p1_name, p1_man, 20);
            store1.supply(p2_name, p2_man, 20);

            UserServices.login(buyerUsername, newPass);
            aUser client = UserServices.getUser(buyerUsername);

            // add the product to the basket
            //client.getCart().getBasket(store1).addProduct(new Product(ProductInfo.getProductInfo(p2_name, p2_cat, p2_man), 1, 0));
            client.getCart().getBasket(store1).addProduct(new Product(ProductInfo.getProductInfo(p1_name, p1_cat, p1_man), 1, 10));

            Assert.AreEqual(1, discountPolicy1.ApplyDiscount(client.getCart().getBasket(store1), 10));

        }

        [TestMethod]
        public void BasicDiscountByProductB()
        {
            // init usernames and passes
            string storeName1 = InfoGenerator.generateValidStoreName();
            string ownerUsername = InfoGenerator.generateValidUsername(),
                       ownerPass = InfoGenerator.generateValidPassword();
            string buyerUsername = InfoGenerator.generateValidUsername(),
                         newPass = InfoGenerator.generateValidPassword();
            // init products info
            string p1_name = "Bamba", p1_man = "Osem", p1_cat = "Food",
                p2_name = "Bamba2", p2_man = "Osem", p2_cat = "Food";

            iPolicyDiscount discountPolicy1 = DiscountPoliciesGenerator.generateDiscountPolicyByProduct(p1_name, p1_cat, p1_man, 0.1, DateTime.MaxValue);
            iPolicyDiscount discountPolicy2 = DiscountPoliciesGenerator.generateDiscountPolicyByProduct(p2_name, p2_cat, p2_man, 0.1, DateTime.MaxValue);
            Product p1 = new Product(ProductInfo.getProductInfo(p1_name, p1_cat, p1_man), 1, 10);


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
            store1.addProduct(p2_name, p2_cat, p2_man);
            // set the price of the products
            store1.editPrice(p1_name, p1_man, 10);
            store1.editPrice(p2_name, p2_man, 10);
            // supply 
            store1.supply(p1_name, p1_man, 20);
            store1.supply(p2_name, p2_man, 20);

            UserServices.login(buyerUsername, newPass);
            aUser client = UserServices.getUser(buyerUsername);

            // add the product to the basket
            //client.getCart().getBasket(store1).addProduct(new Product(ProductInfo.getProductInfo(p2_name, p2_cat, p2_man), 1, 0));
            client.getCart().getBasket(store1).addProduct(new Product(ProductInfo.getProductInfo(p1_name, p1_cat, p1_man), 1, 10));


            Assert.AreEqual(0, discountPolicy2.ApplyDiscount(client.getBasket(store1), 10));
        }

    }


    [TestClass]
    public class OfferRequestTests
    {
        [TestMethod]
        public void SimpleOfferRequestTestGood()
        {
            // init store anf users names&passes
            string storeName1 = InfoGenerator.generateValidStoreName();
            string ownerUsername = InfoGenerator.generateValidUsername(),
                       ownerPass = InfoGenerator.generateValidPassword();
            string buyerUsername = InfoGenerator.generateValidUsername(),
                         newPass = InfoGenerator.generateValidPassword();
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

            UserServices.login(buyerUsername, newPass);
            aUser client = UserServices.getUser(buyerUsername);

            // place a new offer
            int requestId = UserServices.placeOffer(buyerUsername, storeName1, p1_name, p1_cat, p1_man, 3, 5);

            // the owner should've been notified, the offer is in it's offer's list
            Assert.AreNotEqual(requestId, -1, "could't place an offer - got a negative id");
            OfferRequest requestToAnswer = owner.getRequestToAnswer(requestId);

            Assert.IsNotNull(requestToAnswer, "The offer was not saved in the owner's list of requests to answer");
            // make sure that the request jas the correct values in fields
            Assert.AreEqual(requestToAnswer.getPrice(), 5, "Wrong price in offer.");
            Assert.AreEqual(requestToAnswer.product.info, ProductInfo.getProductInfo(p1_name, p1_cat, p1_man), "The info wasn't initialized as expected.");
            Assert.AreEqual(requestToAnswer.status, OfferRequest.Status.PENDING_STORE, "Wrong status.");

            // accept requset
            bool accepted = owner.acceptRequest(requestToAnswer.id);

            Assert.IsTrue(accepted, "Couldn't manage to accept the request.");

            // make sure that the request general status has changed accordingly
            Assert.AreEqual(requestToAnswer.getPrice(), 5, "The price has changed while shouldn't've");
            Assert.AreEqual(requestToAnswer.status, OfferRequest.Status.ACCEPTED);
            Assert.AreEqual(requestToAnswer.product.info, ProductInfo.getProductInfo(p1_name, p1_cat, p1_man), "The info was changed");

            // try to purchase the product
            string[] receiptString = requestToAnswer.purchase("111111111111", "11/22", "123");
            Assert.AreEqual(receiptString[0], "true", "Couldn't purchase");

            Receipt receipt = client.getReceipt(int.Parse(receiptString[1]));
            Assert.IsNotNull(receipt, "The receipt was not saved in the client's receipts collection.");
            // check for the price
            Assert.AreEqual(receipt.price, 15, "Wrong price");
            // check for amounts in the store
            int newAmount = store1.searchProduct(p1_name, p1_man).amount;
            Assert.AreEqual(newAmount, 17, "The amounts were not correctly updated in store");
        }

        [TestMethod]
        public void SimpleOfferRequestTestBad()
        {
            // init store anf users names&passes
            string storeName1 = InfoGenerator.generateValidStoreName();
            string ownerUsername = InfoGenerator.generateValidUsername(),
                       ownerPass = InfoGenerator.generateValidPassword();
            string buyerUsername = InfoGenerator.generateValidUsername(),
                         newPass = InfoGenerator.generateValidPassword();
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

            UserServices.login(buyerUsername, newPass);
            aUser client = UserServices.getUser(buyerUsername);

            // place a new offer
            int requestId = UserServices.placeOffer(buyerUsername, storeName1, p1_name, p1_cat, p1_man, 3, 5);

            // the owner should've been notified, the offer is in it's offer's list
            Assert.AreNotEqual(requestId, -1, "could't place an offer - got a negative id");
            OfferRequest requestToAnswer = owner.getRequestToAnswer(requestId);

            Assert.IsNotNull(requestToAnswer, "The offer was not saved in the owner's list of requests to answer");
            // make sure that the request jas the correct values in fields
            Assert.AreEqual(requestToAnswer.getPrice(), 5, "Wrong price in offer.");
            Assert.AreEqual(requestToAnswer.product.info, ProductInfo.getProductInfo(p1_name, p1_cat, p1_man), "The info wasn't initialized as expected.");
            Assert.AreEqual(requestToAnswer.status, OfferRequest.Status.PENDING_STORE, "Wrong status.");

            // accept requset
            bool rejected = owner.rejectOffer(requestToAnswer.id);

            Assert.IsTrue(rejected, "Couldn't manage to reject the request.");

            // make sure that the request general status has changed accordingly
            Assert.AreEqual(requestToAnswer.getPrice(), 5, "The price has changed while shouldn't've");
            Assert.AreEqual(requestToAnswer.status, OfferRequest.Status.REJECTED);
            Assert.AreEqual(requestToAnswer.product.info, ProductInfo.getProductInfo(p1_name, p1_cat, p1_man), "The info was changed");

            // try to purchase the product
            string[] receiptString = requestToAnswer.purchase("111111111111", "11/22", "123");
            Assert.AreEqual(receiptString[0], "false", "Managed to purchase");
            
            // check for amounts in the store
            int newAmount = store1.searchProduct(p1_name, p1_man).amount;
            Assert.AreEqual(newAmount, 20, "The amounts were changed in store");
        }

        [TestMethod]
        public void NegotiateOfferRequest()
        {
            // init store anf users names&passes
            string storeName1 = InfoGenerator.generateValidStoreName();
            string ownerUsername = InfoGenerator.generateValidUsername(),
                       ownerPass = InfoGenerator.generateValidPassword();
            string buyerUsername = InfoGenerator.generateValidUsername(),
                         newPass = InfoGenerator.generateValidPassword();
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

            UserServices.login(buyerUsername, newPass);
            aUser client = UserServices.getUser(buyerUsername);

            // place a new offer
            int requestId = UserServices.placeOffer(buyerUsername, storeName1, p1_name, p1_cat, p1_man, 3, 5);

            // the owner should've been notified, the offer is in it's offer's list
            Assert.AreNotEqual(requestId, -1, "could't place an offer - got a negative id");
            OfferRequest requestToAnswer = owner.getRequestToAnswer(requestId);

            Assert.IsNotNull(requestToAnswer, "The offer was not saved in the owner's list of requests to answer");
            // make sure that the request jas the correct values in fields
            Assert.AreEqual(requestToAnswer.getPrice(), 5, "Wrong price in offer.");
            Assert.AreEqual(requestToAnswer.product.info, ProductInfo.getProductInfo(p1_name, p1_cat, p1_man), "The info wasn't initialized as expected.");
            Assert.AreEqual(requestToAnswer.status, OfferRequest.Status.PENDING_STORE, "Wrong status.");

            // accept requset
            bool negotiated = owner.negotiateRequest(requestToAnswer.id, 6); // the new price is 6, instead of 5

            Assert.IsTrue(negotiated, "Couldn't manage to accept the request.");

            // make sure that the request general status has changed accordingly
            Assert.AreEqual(requestToAnswer.getPrice(), 6, "The price hasn't changed to 6");
            Assert.AreEqual(requestToAnswer.status, OfferRequest.Status.ACCEPTED);
            Assert.AreEqual(requestToAnswer.product.info, ProductInfo.getProductInfo(p1_name, p1_cat, p1_man), "The info was changed");

            // try to purchase the product
            string[] receiptString = requestToAnswer.purchase("111111111111", "11/22", "123");
            Assert.AreEqual(receiptString[0], "true", "Couldn't purchase");

            Receipt receipt = client.getReceipt(int.Parse(receiptString[1]));
            Assert.IsNotNull(receipt, "The receipt was not saved in the client's receipts collection.");
            // check for the price
            Assert.AreEqual(receipt.price, 18, "Wrong price");
            // check for amounts in the store
            int newAmount = store1.searchProduct(p1_name, p1_man).amount;
            Assert.AreEqual(newAmount, 17, "The amounts were not correctly updated in store");
        }

        [TestMethod]
        public void purchaseOfferTestGood()
        {
            string storename = "Potg_store_1", user1 = "Potg_user_1", user2 = "Potg_user_2", password = "qweE1";
            string productName = "bamba", productManuf = "osem", productCtgr = "snacks";
            UserController.register(user1, password, 99, "male", "");
            UserController.register(user2, password, 99, "male", "");
            UserController.login(user1, password);
            UserController.EstablishStore(user1, storename);
            UserController.addNewProduct(user1, storename, productName, 5.5, 100, productCtgr, productManuf);
            UserController.logout();
            UserController.login(user2, password);
            //make initial offer
            int offerID1 = UserController.placeOffer(user2, storename, productName, productCtgr, productManuf, 1, 1.5);
            Assert.IsTrue(offerID1 > -1);
            Assert.IsNotNull(UserController.getOfferRequestsToAnswerIDs(user2).Length == 1);
            Assert.IsTrue(UserController.getOfferRequest(user1, offerID1)[6].Equals("PENDING_STORE"));
            UserController.logout();
            UserController.login(user1, password);
            //reject initial offer
            UserController.rejectOfferRequest(user1, offerID1);
            Assert.IsTrue(UserController.getOfferRequestToAnswer(user1, offerID1)[6].Equals("REJECTED"));
            UserController.logout();
            UserController.login(user2, password);
            //make secondary offer
            int offerID2 = UserController.placeOffer(user2, storename, productName, productCtgr, productManuf, 1, 1.5);
            UserController.logout();
            UserController.login(user1, password);
            //bargain on secondary offer
            UserController.negotiateOfferRequest(user1, offerID2, 2.5);
            Assert.IsTrue(UserController.getOfferRequestToAnswer(user1, offerID1)[6].Equals("PENDING_REQUESTER"));
            UserController.logout();
            UserController.login(user2, password);
            //accept bargain on secondary offer
            UserController.acceptOfferRequest(user2, offerID2);
            Assert.IsTrue(UserController.purchase(user2, "1234567812345678", "01/99", "111").Equals("True"));
            UserController.logout();
        }

        [TestMethod]
        public void purchaseOfferTestBad()
        {
            string storename = "Potb_store_1", user1 = "Potb_user_1", user2 = "Potb_user_2", password = "qweE1";
            string productName = "bamba", productManuf = "osem", productCtgr = "snacks";
            UserController.register(user1, password, 99, "male", "");
            UserController.register(user2, password, 99, "male", "");
            UserController.login(user1, password);
            UserController.EstablishStore(user1, storename);
            UserController.addNewProduct(user1, storename, productName, 5.5, 100, productCtgr, productManuf);
            UserController.logout();
            //guset placing offer
            int offerID1 = UserController.placeOffer("guest", storename, productName, productCtgr, productManuf, 1, 1.5);
            Assert.IsTrue(offerID1 == -1);
            //logged out user placing offer
            int offerID2 = UserController.placeOffer(user2, storename, productName, productCtgr, productManuf, 1, 1.5);
            Assert.IsTrue(offerID2 == -1);
            UserController.login(user2, password);
            //placing offer of nonexistant store
            int offerID3 = UserController.placeOffer(user2, "wrongstore", productName, productCtgr, productManuf, 1, 1.5);
            Assert.IsTrue(offerID3 < 0);
            //placing offer for nonexistant product
            int offerID4 = UserController.placeOffer(user2, storename, "wrongProduct", productCtgr, productManuf, 1, 1.5);
            Assert.IsTrue(offerID4 < 0);
            int offerID5 = UserController.placeOffer(user2, storename, productName, productCtgr, productManuf, 1, 1.5);
            UserController.logout();
            UserController.login(user1, password);
            UserController.closeStore(user1, storename);
            UserController.logout();
            UserController.login(user2, password);
            Assert.IsTrue(UserController.getOfferRequest(user2, offerID5)[6].Equals("REJECTED"));
            UserController.logout();
        }

    }







    public class InfoGenerator
    {
        // this class has static methods and fields
        // it provides services for generating valid unique info
        // including usernames, passwords and store names
        private static string baseUsername = "User",
                            basePassword = "123Xx",
                            baseStoreName = "Store";
        // id's for uniqueness
        private static int usernameID = 0,
                        passwordID = 0,
                        storeID = 0;
        // locks for preventing generating the same info twice
        private static Object usernameLocker = new object(),
                        passwordLocker = new object(),
                         storeLocker = new object();

        public static string generateValidUsername()
        {
            lock (usernameLocker)
            {
                usernameID++;
                return baseUsername + getThreeDigit(usernameID);
            }
        }

        public static string generateValidPassword()
        {
            lock (passwordLocker)
            {
                passwordID++;
                return basePassword + getThreeDigit(passwordID);
            }
        }

        public static string generateValidStoreName()
        {
            lock (storeLocker)
            {
                storeID++;
                return baseStoreName + getThreeDigit(storeID);
            }
        }

        private static string getThreeDigit(int ID)
        {
            return ID < 10 ? "00" + ID : ID < 100 ? "0" + ID : "" + ID;
        }
    }

    public class ReceiptsConverter
    {
        public static ICollection<Receipt> convertReceiptsArray(string[] receiptsString)
        {
            ICollection<Receipt> receipts = new LinkedList<Receipt>();
            // receiptsString[0] contains the answer
            for (int i = 1; i < receiptsString.Length; i++)
                receipts.Add(convertReceipt(receiptsString[i]));

            return receipts;
        }

        public static Receipt convertReceipt(string receiptString)
        {
            Receipt receipt = new Receipt();

            string[] splitReceipt = receiptString.Split('$');
            // username&storename$price$date$receiptId$<products>
            //receipt.user.userName = splitReceipt[0];
            // receipt.store = Stores.searchStore(splitReceipt[1]); //changed
            receipt.price = double.Parse(splitReceipt[2]);
            receipt.date = Convert.ToDateTime(splitReceipt[3]);
            receipt.receiptId = int.Parse(splitReceipt[4]);
            // the products - todo


            return receipt;
        }
    }
}

