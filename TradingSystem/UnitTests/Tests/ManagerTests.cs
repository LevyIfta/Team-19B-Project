﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UnitTests.Bridge;

namespace UnitTests
{
    [TestClass]
    public class ManagerTests
    {

        private static Bridge.Bridge bridge { get; set; }
        [ClassInitialize]
        public static void classInit(TestContext context)
        {
            bridge = Bridge.Driver.GetBridge();
            bridge.register("owner", "ownerPass");
            bridge.register("owner2", "owner2Pass");
            bridge.register("owner2B", "owner2BPass");
            bridge.register("manager", "managerPass");
            bridge.register("manager2", "manager2Pass");
            bridge.register("manager2B", "manager2BPass");

            bridge.login("owner", "ownerPass");
            bridge.addStore("store1");
            bridge.addStore("store2");
         
            bridge.hireNewManager("store1", "manager");//todo hire manager
            List<string> permissions = new List<string>();
            permissions.Add("all");
            bridge.hireNewOwner("store1", "owner2", permissions);
            bridge.logout();
            bridge.login("owner2", "owner2Pass");
            bridge.hireNewManager("store1", "manager2");//todo hire manager
            bridge.logout();

        }
        [ClassCleanup]
        public static void classCleanup()
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
        public void editManagerPermissionTest()
        {
            bridge.login("owner", "ownerPass");
            List<string> premmisions = new List<string>();
            premmisions.Add("view info");
            bool ret = bridge.editManagerPermissions("manager", "store1", premmisions ); //edit manager premission
            Assert.IsTrue(ret, "failed to edit manager permission");
            ret = bridge.editManagerPermissions("manager2", "store1", premmisions); //edit manager 2 premiision
            Assert.IsFalse(ret, "managed to change permision of manager not assigned by the logged owner");
            bridge.logout();
            bridge.login("manager", "managerPass");
            ret = bridge.editManagerPermissions("manager2", "store1", premmisions); //edit manager 2 premisions
            Assert.IsFalse(ret , "managed to change permision of manager not assigned by the logged owner");


            //todo
            Assert.Fail();
        }

        [TestMethod]
        public void getEmployeesInfoTest()
        {
            //todo
            Assert.Fail();
        }


        [TestMethod]
        public void hireNewEmployeeTest()
        {
            //todo
            Assert.Fail();
        }

        [TestMethod]
        public void removeEmployeeTest()
        {
            //todo
            Assert.Fail();
        }





    }
}
