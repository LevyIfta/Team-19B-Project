using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTests.Bridge;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private static Bridge.Bridge bridge { get; set; }
        [ClassInitialize]
        public static void classInit(TestContext context)
        {
            bridge = Bridge.Driver.GetBridge();
            bridge.register("user", "password");
        }

        [TestMethod]
        public void testRegister()
        {
            string username = "gooduser"; 
            string password = "goodpassword";
            Assert.IsTrue(bridge.register(username, password), "Failed to register as a valid user");
            username = "@u@";
            password = "stillgoodpassword";
            Assert.IsFalse(bridge.register(username, password), "managed to register with invalid username");
            username = "stillokuser";
            password = "%z\n";
            Assert.IsFalse(bridge.register(username, password), "managed to register with invalid password");
            username = "really bad and long username that is bad and contain bad stuff like DROP TABLE users or; even a link like https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO";
            password = "still a bad password like \n $#*";
            Assert.IsFalse(bridge.register(username, password), "managed to register with invalid password and invalid username");

        }

        [TestMethod]
        public void testLogin()
        {
            Assert.IsTrue(bridge.login("user", "password"), "Failed to login as a valid user");
            Assert.IsFalse(bridge.login("falseUser", "password"), "managed to login with wrong username");
            Assert.IsFalse(bridge.login("user", "falsePassword"), "managed to login with wrong password");
            Assert.IsFalse(bridge.login("falseUser", "falsePassword"), "managed to login with non-existing username and password");

        }
            
        
    }
}
