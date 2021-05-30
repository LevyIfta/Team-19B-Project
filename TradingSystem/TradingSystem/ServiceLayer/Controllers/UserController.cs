using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer;

namespace TradingSystem.ServiceLayer
{
    public class UserController
    {
        [ThreadStatic]
        public static aUser user = new Guest();
        [ThreadStatic]
        private static Thread alarmThread;


        private static Func<object, bool> alarmHandler;

        public static void init(Func<object, bool> alarmhandler)
        {
           
            alarmHandler = alarmhandler;
        }
        public static void threadInit()
        {
            user = new Guest();
        }
        

        public static string[] login(string username, string password)
        {
            //     DirAppend.AddToLogger("user " + username + " login", Result.Log);
        
            string[] ans = BuissnessLayer.UserServices.login(username, password);
            if (ans[0].Equals("false"))
            {
                UserServices.getAdmin().addAlarm("login failed", ans[1]);
                return ans;
            }

            aUser olduser = user;
            user = BuissnessLayer.UserServices.getUser(username);
            alarmThread.Abort();
            alarmThread= user.estblishAlarmHandler(olduser.getAlarmParams(),  olduser.getAlarmLock(), alarmHandler);
            return ans;

        }

        public static bool logout()
        {
            if(!user.getUserName().Equals("guest") && UserServices.onlineUsers.Contains(user.getUserName()))
            {
                //       DirAppend.AddToLogger("user " + user.getUserName() + " logout", Result.Log);
                //       DirAppend.AddToLogger("user " + user.getUserName() + " logout", Result.Log);
                if (UserServices.logout(user.getUserName()))
                {
                    aUser olduser = user;
                    user = new Guest();
                    alarmThread.Abort();
                    alarmThread = user.estblishAlarmHandler(olduser.getAlarmParams(), olduser.getAlarmLock(), alarmHandler);

                    return true;
                }
            }
        //    DirAppend.AddToLogger("There was a failed login attempt", Result.Warnning);
            return false;
        }
        public static aUser getCorrentOnlineUser()
        {
            return user;
        }
        public static string getCorrentOnlineUserName()
        {
            return UserController.user.getUserName();
        }
        public static bool register(string userName, string password)
        {

            string[] ans = BuissnessLayer.UserServices.register(userName, password);
            if(ans[0].Equals("true"))
            {
            //    DirAppend.AddToLogger("new user register", Result.Log);
                return true;
            }
            user.addAlarm("register", ans[1]);
            return false;
        }

        public static bool saveProduct(string userName, string storeName, string manufacturer, Dictionary<string, int> product)
        {
            return BuissnessLayer.UserServices.saveProduct(userName, storeName, manufacturer, product);
        }

        public static bool removeProduct(string username, string storeName, string manufacturer, Dictionary<string, int> product)
        {
            return BuissnessLayer.UserServices.removeProduct(username, storeName, manufacturer, product);
        }

        public static SLbasket getBasket(string username, string storeName)
        {
            BuissnessLayer.commerce.ShoppingBasket basket = BuissnessLayer.UserServices.getBasket(username, storeName);
            return ProductController.makeSLbasket(basket);
        }

        public static ICollection<SLbasket> getCart(string username)
        {
            BuissnessLayer.commerce.ShoppingCart cart = BuissnessLayer.UserServices.getCart(username);
            ICollection<SLbasket> SLcart = new List<SLbasket>();
            foreach (BuissnessLayer.commerce.ShoppingBasket basket in cart.baskets)
            {
                SLcart.Add(ProductController.makeSLbasket(basket));
            }
            return SLcart;
        }

        public static bool EstablishStore(string username, string storeName)
        {
            return BuissnessLayer.UserServices.EstablishStore(username, storeName);
        }

        public static double checkPrice(string username)
        {
            return BuissnessLayer.UserServices.checkPrice(username);
        }

        public static ICollection<SLreceipt> purchase(string username, string paymentName)
        {
            ICollection<BuissnessLayer.commerce.Receipt> temp = BuissnessLayer.UserServices.purchase(username, paymentName);
            ICollection<SLreceipt> receipts = new List<SLreceipt>();
            if(temp == null)
            {
                return null;
            }
            foreach (BuissnessLayer.commerce.Receipt receipt in temp)
            {                
                receipts.Add(ProductController.makeReceipt(receipt));
                ICollection<BuissnessLayer.Member> storeOwners = receipt.store.getOwners();
                foreach (BuissnessLayer.Member owner in storeOwners)
                {
                    string msg = username + "has bought the following products from the store " + receipt.store.name + ":";
                    ICollection<BuissnessLayer.commerce.Product> products = receipt.getProducts();
                    foreach (BuissnessLayer.commerce.Product product in products)
                    {
                        msg += "\n" + product.info.name + " by " + product.info.manufacturer + " X" + product.amount; //how to add product amount?
                    }
                    owner.addAlarm("Purchased products", msg);
                }
            }
       //     DirAppend.AddToLogger("user " + user.getUserName() + " just purchase his cart", Result.Log);
            return receipts;
        }

        public static Dictionary<string,SLproduct> browseProducts(string username, string productName, string manufacturer)
        {
            Dictionary<BuissnessLayer.commerce.Store, BuissnessLayer.commerce.Product> catallog = BuissnessLayer.UserServices.browseProducts(username, productName, manufacturer);
            Dictionary<string, SLproduct> SLcatalog = new Dictionary<string, SLproduct>();
            foreach (BuissnessLayer.commerce.Store store in catallog.Keys)
            {
                SLcatalog.Add(store.name, ProductController.makeSLproduct(catallog[store]));
            }
            return SLcatalog;
        }

        public static Dictionary<string, SLproduct> browseProducts(string username, string productName, string category, string manufacturer, double minPrice, double maxPrice)
        {
            Dictionary<BuissnessLayer.commerce.Store, BuissnessLayer.commerce.Product> catallog = BuissnessLayer.UserServices.browseProducts(username, productName, category, manufacturer, minPrice, maxPrice);
            Dictionary<string, SLproduct> SLcatalog = new Dictionary<string, SLproduct>();
            foreach (BuissnessLayer.commerce.Store store in catallog.Keys)
            {
                SLcatalog.Add(store.name, ProductController.makeSLproduct(catallog[store]));
            }
            return SLcatalog;
        }

        public static string browseStore(string username, string storeName)
        {
            var ans = BuissnessLayer.UserServices.browseStore(username, storeName);
            if (ans == null)
                return "";
            return ans.name;
        }

        public static ICollection<SLreceipt> getReceiptsHistory(string username, string storeName)
        {
            ICollection<BuissnessLayer.commerce.Receipt> receipts = BuissnessLayer.UserServices.getReceiptsHistory(username, storeName);
            return ProductController.makeSLreceiptCollection(receipts);
        }

        public static ICollection<SLreceipt> getAllReceiptsHistory(string username, string storeName)
        {
            ICollection<BuissnessLayer.commerce.Receipt> receipts = BuissnessLayer.UserServices.getAllReceiptsHistory(username, storeName);
            return ProductController.makeSLreceiptCollection(receipts);
        }

        public static ICollection<SLreceipt> getAllMyReceiptHistory(string username)
        {
            ICollection<BuissnessLayer.commerce.Receipt> receipts = BuissnessLayer.UserServices.getAllMyReceiptHistory(username);
            return ProductController.makeSLreceiptCollection(receipts);
        }

        public static ICollection<SLemployee> getInfoEmployees(string username, string storeName)
        {
            ICollection<BuissnessLayer.aUser> employees = BuissnessLayer.UserServices.getInfoEmployees(username, storeName);
            ICollection<SLemployee> SLemployees = new List<SLemployee>();
            foreach (BuissnessLayer.aUser employee in employees)
            {
                SLemployees.Add(UserController.makeSLemployee(employee));
            }
            return SLemployees;
        }

        public static bool addNewProduct(string username, string storeName, string productName, double price, int amount, string category, string manufacturer)
        {
            return BuissnessLayer.UserServices.addNewProduct(username, storeName, productName, price, amount, category, manufacturer);
        }

        public static bool removeProduct(string username, string storeName, string productName, string manufacturer)
        {
            return BuissnessLayer.UserServices.removeProduct(username, storeName, productName, manufacturer);
        }

        public static bool editProduct(string username, string storeName, string productName, double price, string manufacturer)
        {
            return BuissnessLayer.UserServices.editProduct(username, storeName, productName, price, manufacturer);
        }

        public static bool hireNewStoreManager(string username, string storeName, string userToHire)
        {
            bool ans = BuissnessLayer.UserServices.hireNewStoreManager(username, storeName, userToHire);
            if (ans)
            {
                List<string> strPersmissions = new List<string>();
                ICollection<BuissnessLayer.User.Permmisions.PersmissionsTypes> permissions = ((BuissnessLayer.Member)BuissnessLayer.UserServices.getUser(userToHire)).GetPermissions(storeName);
                foreach (BuissnessLayer.User.Permmisions.PersmissionsTypes perm in permissions)
                {
                    strPersmissions.Add(BuissnessLayer.User.Permmisions.aPermission.who(perm));
                }
                string msg = UserController.alarmMessage(username, "a manager", strPersmissions, storeName);
                BuissnessLayer.UserServices.getUser(userToHire).addAlarm("Hired as manager", msg);
            }
            return ans;
        }

        public static bool editManagerPermissions(string username, string storeName, string userToHire, List<string> Permissions)
        {
            return BuissnessLayer.UserServices.editManagerPermissions(username, storeName, userToHire, Permissions);
        }

        public static bool hireNewStoreOwner(string username, string storeName, string userToHire, List<string> Permissions)
        {
            bool ans = BuissnessLayer.UserServices.hireNewStoreOwner(username, storeName, userToHire, Permissions);
            if (ans)
            {
                string msg = UserController.alarmMessage(username, "an owner", Permissions, storeName);
                BuissnessLayer.UserServices.getUser(userToHire).addAlarm("Hired as owner", msg);
            }         
            return ans;
        }

        private static string alarmMessage(string sourceName, string managerOrOwner, List<string> permissions, string storeName)
        {
            string msg = sourceName + " has added you as " + managerOrOwner + " with the following permissions:";
            foreach (string perm in permissions)
            {
                msg += "\n" + perm;
            }  
            msg += "\n" + "to the store: " + storeName;
            return msg;
        }

        public static bool removeManager(string username, string storeName, string userToHire)
        {
            bool ans = BuissnessLayer.UserServices.removeManager(username, storeName, userToHire);
            if (ans)
            {
                string msg = username + " has removed you from being a manager of the store " + storeName;
                BuissnessLayer.UserServices.getUser(userToHire).addAlarm("Fired as manager", msg);
            }
            return ans;
        }

        public static bool removeOwner(string username, string storeName, string userToHire)
        {
            bool ans = BuissnessLayer.UserServices.removeOwner(username, storeName, userToHire);
            if (ans)
            {
                string msg = username + " has removed you from being an owner of the store " + storeName;
                BuissnessLayer.UserServices.getUser(userToHire).addAlarm("Fired as owner", msg);
            }
            return ans;
        }

        public static bool leaveFeedback(string username, string storeName, string productName, string manufacturer, string comment)
        {
            return BuissnessLayer.UserServices.leaveFeedback(username, storeName, productName, manufacturer, comment);
        }

        public static Dictionary<string, string> getAllFeedbacks(string storeName, string productName, string manufacturer)
        {
            return BuissnessLayer.UserServices.getAllFeedbacks(storeName, productName, manufacturer);
        }

        //TODO
        private static SLemployee makeSLemployee(BuissnessLayer.aUser employee)
        {
            Object[] parameters = new Object[SLbasket.PARAMETER_COUNT];
            parameters[0] = employee.getUserName();
            parameters[1] = employee.GetAllPermissions();
            return (new SLemployee(parameters));
        }

        public static string getUserName()
        {
            return UserController.user.getUserName();
        }

        public static Tuple<string, string> fetchAlarm()
        {
            return user.fetchAlarm();
        }

        public static bool isAlarmsEmpty()
        {
            return user.isAlarmsEmpty();
        }
        public static void estblishAlarmHandler(object queue, object waitEvent, AutoResetEvent alarmLock)
        {
            alarmThread =  user.estblishAlarmHandler(queue, waitEvent, alarmLock, alarmHandler);
        }
    }
}
