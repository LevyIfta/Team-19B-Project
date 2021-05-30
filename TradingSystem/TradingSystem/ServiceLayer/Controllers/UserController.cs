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
                //UserServices.getAdmin().addAlarm("login failed", ans[1]);
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
        public static string[] register(string userName, string password)
        {

            string[] ans = BuissnessLayer.UserServices.register(userName, password);
            if(ans[0].Equals("true"))
            {
            //    DirAppend.AddToLogger("new user register", Result.Log);
                return ans;
            }
            //user.addAlarm("register", ans[1]);
            return ans;
        }
        public static string[] register(string userName, string password, double age, string gender, string address)
        {

            string[] ans = BuissnessLayer.UserServices.register(userName, password, age, gender, address);
            if (ans[0].Equals("true"))
            {
                //    DirAppend.AddToLogger("new user register", Result.Log);
                return ans;
            }
            //user.addAlarm("register", ans[1]);
            return ans;
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

        public static string[] purchase(string username, string creditNumber, string validity, string cvv)
        {
            string[] temp = BuissnessLayer.UserServices.purchase(username, creditNumber, validity, cvv);
            if (temp == null || temp[0].Equals("false"))
                return temp;
            ICollection<SLreceipt> list = new List<SLreceipt>();
            for (int i=1; i<temp.Length; i++)
            {
                list.Add(ProductController.makeReceipt(convertReceipt(temp[i])));
            }

       //     DirAppend.AddToLogger("user " + user.getUserName() + " just purchase his cart", Result.Log);
            return ReceiptsToStringArray(list);
        }
        private static string[] ReceiptsToStringArray(ICollection<SLreceipt> receipts)
        {
            string[] ans = new string[receipts.Count];
            int i = 0;
            foreach (SLreceipt receipt in receipts)
            {
                ans[i] += ReceiptToString(receipt);
                i++;
            }
            return ans;
        }
        private static string ReceiptToString(SLreceipt receipt)
        {
            string ans = "";
            foreach (SLproduct pro in receipt.products)
            {
                ans += ProductToString(pro) + "&";
            }
            if (ans.Length > 0)
                ans = ans.Substring(0, ans.Length - 1); // delete the & in the end
            return receipt.userName + "$" + receipt.storeName + "$" + receipt.price + "$" + receipt.date.ToString("dddd, dd MMMM yyyy HH:mm:ss") + "$" + receipt.receiptID + "$" + ans;
        }
        private static string feedbackToString(Dictionary<string, string> dic) // username : comment
        {
            string ans = "";
            foreach (string user in dic.Keys)
            {
                ans += user + "#" + dic[user] + "_";
            }
            if (ans.Length > 0)
                ans = ans.Substring(0, ans.Length - 1); // delete the _ in the end
            return ans; // almog#what i think_gal#what he think
        }
        private static string ProductToString(SLproduct pro)
        { // product name^price^manu^category^amount^feedback
            return pro.productName + "^" + pro.price + "^" + pro.manufacturer + "^" + pro.category + "^" + pro.amount + "^" + feedbackToString(pro.feedbacks);
        } // bamba^10.3^manu1^food^10^almog#what i think_gal#what he think
        private static BuissnessLayer.commerce.Receipt convertReceipt(string receipt)
        {
            BuissnessLayer.commerce.Receipt ans = new BuissnessLayer.commerce.Receipt();
            string[] arr = receipt.Split('$');
            ans.username = arr[0];
            ans.store = BuissnessLayer.commerce.Stores.searchStore(arr[1]);
            ans.price = double.Parse(arr[2]);
            ans.date = Convert.ToDateTime(arr[3]);
            ans.receiptId = int.Parse(arr[4]);
            string[] pro = arr[5].Split('=');
            Dictionary<int, int> dic = new Dictionary<int, int>();
            for (int i=0; i<pro.Length; i++)
            {
                string[] info = pro[i].Split('<');
                dic[int.Parse(info[0])] = int.Parse(info[1]);
            }
            ans.products = dic;
            return ans;
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
            return BuissnessLayer.UserServices.hireNewStoreManager(username, storeName, userToHire);
        }

        public static bool editManagerPermissions(string username, string storeName, string userToHire, List<string> Permissions)
        {
            return BuissnessLayer.UserServices.editManagerPermissions(username, storeName, userToHire, Permissions);
        }

        public static bool hireNewStoreOwner(string username, string storeName, string userToHire, List<string> Permissions)
        {
            return BuissnessLayer.UserServices.hireNewStoreOwner(username, storeName, userToHire, Permissions);
        }

        public static bool removeManager(string username, string storeName, string userToHire)
        {
            return BuissnessLayer.UserServices.removeManager(username, storeName, userToHire);
        }

        public static bool leaveFeedback(string username, string storeName, string productName, string manufacturer, string comment)
        {
            return BuissnessLayer.UserServices.leaveFeedback(username, storeName, productName, manufacturer, comment);
        }

        public static Dictionary<string, string> getAllFeedbacks(string storeName, string productName, string manufacturer)
        {
            return BuissnessLayer.UserServices.getAllFeedbacks(storeName, productName, manufacturer);
        }
        public static bool closeStore(string username, string storeName)
        {
            return UserServices.closeStore(username, storeName);
        }
        public static bool reopenStore(string username, string storeName)
        {
            return UserServices.reopenStore(username, storeName);
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
