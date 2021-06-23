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

        static UserController()
        {
            //register("milon", "qweE1");
            
            //login("almog", "qweE1");
           // EstablishStore("almog", "Castro");
          //  EstablishStore("almog", "Castro2");
           // addNewProduct("almog", "Castro", "pro", 10.1, 10, "cat", "man");
           // logout();
            
        }

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
            if(alarmThread != null)
            {
                alarmThread.Abort();
                alarmThread = user.estblishAlarmHandler(olduser.getAlarmParams(), olduser.getAlarmLock(), alarmHandler);
            }
            
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
                    if(alarmThread != null)
                    {
                        alarmThread.Abort();
                        alarmThread = user.estblishAlarmHandler(olduser.getAlarmParams(), olduser.getAlarmLock(), alarmHandler);
                    }

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
        public static aUser getUser(string username)
        {
            return UserServices.getUser(username);
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
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(userName))
            {
                UserServices.addGuest();
            }
            return BuissnessLayer.UserServices.saveProduct(userName, storeName, manufacturer, product);
        }

        public static bool removeProduct(string username, string storeName, string manufacturer, Dictionary<string, int> product)
        {
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
            {
                UserServices.addGuest();
            }
            return BuissnessLayer.UserServices.removeProduct(username, storeName, manufacturer, product);
        }

        public static SLbasket getBasket(string username, string storeName)
        {
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
            {
                UserServices.addGuest();
            }
            BuissnessLayer.commerce.ShoppingBasket basket = BuissnessLayer.UserServices.getBasket(username, storeName);
            return ProductController.makeSLbasket(basket);
        }

        public static ICollection<SLbasket> getCart(string username)
        {
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
            {
                UserServices.addGuest();
            }
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
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
                return false;
            return BuissnessLayer.UserServices.EstablishStore(username, storeName);
        }

        public static double checkPrice(string username)
        {
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
            {
                UserServices.addGuest();
            }
            return BuissnessLayer.UserServices.checkPrice(username);
        }

        public static string[] purchase(string username, string creditNumber, string validity, string cvv)
        {
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
            {
                UserServices.addGuest();
            }
            string[] temp = BuissnessLayer.UserServices.purchase(username, creditNumber, validity, cvv);
            if (temp == null || temp[0].Equals("false"))
                return temp;
            ICollection<SLreceipt> list = new List<SLreceipt>();
            for (int i=1; i<temp.Length; i++)
            {
                SLreceipt receipt = ProductController.makeReceipt(convertReceipt(temp[i]));
                ICollection<string> storeOwners = StoreController.searchStore(receipt.storeName).ownerNames;
                foreach (string owner in storeOwners)
                {
                    string msg = username + "has bought the following products from the store " + receipt.storeName+ ":";
                    ICollection<SLproduct> products = receipt.products;
                    foreach (SLproduct product in products)
                    {
                        msg += "\n" + product.productName + " by " + product.manufacturer + " X" + product.amount;
                    }
                    UserServices.getUser(owner).addAlarm("Purchased products", msg);
                }
                list.Add(receipt);
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
            return new BuissnessLayer.commerce.Receipt(DataLayer.ORM.DataAccess.getReciept(int.Parse(receipt)));
            /*
            string[] arr = receipt.Split('$');
            ans.user = (Member)UserServices.getUser(arr[0]);
            ans.store = BuissnessLayer.commerce.Stores.searchStore(arr[1]);
            ans.price = double.Parse(arr[2]);
            ans.date = Convert.ToDateTime(arr[3]);
            ans.receiptId = int.Parse(arr[4]);
            string[] pro = arr[5].Split('=');
            //Dictionary<int, int> dic = new Dictionary<int, int>();
            List<DataLayer.ProductData> products = new List<DataLayer.ProductData>();
            if (pro.Length > 0 && pro[0].Length > 0)
            {
                for (int i = 0; i < pro.Length; i++)
                {
                    string[] info = pro[i].Split('<');
                    DataLayer.ProductData data = DataLayer.ORM.DataAccess.getProduct(ToGuid(int.Parse(info[0])));
                    products.Add(new DataLayer.ProductData(data.productData, int.Parse(info[1]), data.price, data.storeName, ToGuid(int.Parse(info[0]))));
                    //dic[int.Parse(info[0])] = int.Parse(info[1]);
                }
            }
            ans.basket = new DataLayer.BasketInRecipt(products, DataLayer.ORM.DataAccess.getReciept())
            return ans;
            */
        }
        private static Guid ToGuid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
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
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
                return null;
            ICollection<BuissnessLayer.commerce.Receipt> receipts = BuissnessLayer.UserServices.getReceiptsHistory(username, storeName);
            return ProductController.makeSLreceiptCollection(receipts);
        }

        public static ICollection<SLreceipt> getAllReceiptsHistory(string username, string storeName)
        {
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
                return null;
            ICollection<BuissnessLayer.commerce.Receipt> receipts = BuissnessLayer.UserServices.getAllReceiptsHistory(username, storeName);
            return ProductController.makeSLreceiptCollection(receipts);
        }

        public static ICollection<SLreceipt> getAllMyReceiptHistory(string username)
        {
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
                return null;
            ICollection<BuissnessLayer.commerce.Receipt> receipts = BuissnessLayer.UserServices.getAllMyReceiptHistory(username);
            return ProductController.makeSLreceiptCollection(receipts);
        }

        public static ICollection<SLemployee> getInfoEmployees(string username, string storeName)
        {
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
                return null;
            ICollection<BuissnessLayer.Member> employees = BuissnessLayer.UserServices.getInfoEmployees(username, storeName);
            ICollection<SLemployee> SLemployees = new List<SLemployee>();
            if (employees == null)
                return null;
            foreach (BuissnessLayer.aUser employee in employees)
            {
                SLemployees.Add(UserController.makeSLemployee(employee));
            }
            return SLemployees;
        }

        public static bool addNewProduct(string username, string storeName, string productName, double price, int amount, string category, string manufacturer)
        {
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
                return false;
            return BuissnessLayer.UserServices.addNewProduct(username, storeName, productName, price, amount, category, manufacturer);
        }

        public static bool removeProduct(string username, string storeName, string productName, string manufacturer)
        {
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
                return false;
            return BuissnessLayer.UserServices.removeProduct(username, storeName, productName, manufacturer);
        }

        public static bool editProduct(string username, string storeName, string productName, double price, string manufacturer)
        {
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
                return false;
            return BuissnessLayer.UserServices.editProduct(username, storeName, productName, price, manufacturer);
        }

        public static bool hireNewStoreManager(string username, string storeName, string userToHire)
        {
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
                return false;
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
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
                return false;
            return BuissnessLayer.UserServices.editManagerPermissions(username, storeName, userToHire, Permissions);
        }

        public static bool hireNewStoreOwner(string username, string storeName, string userToHire, List<string> Permissions)
        {
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
                return false;
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
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
                return false;
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
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
                return false;
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
        public static Dictionary<string, string> getAllFeedbacks(string storeName, string productName)
        {
            return BuissnessLayer.UserServices.getAllFeedbacks(storeName, productName);
        }
        public static bool closeStore(string username, string storeName)
        {
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
                return false;
            return UserServices.closeStore(username, storeName);
        }
        public static bool reopenStore(string username, string storeName)
        {
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
                return false;
            return UserServices.reopenStore(username, storeName);
        }
        public static string[] GetPermissions(string username, string storeName)
        {
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
                return null;
            return UserServices.GetPermissions(username, storeName);
        }
        public static string[] GetMyStores(string username)
        {
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
                return null;
            return UserServices.GetMyStores(username);
        }
        public static bool sendMessage(string username, string userToSend, string storeToSend, string msg)
        {
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
                return false;
            return UserServices.sendMessage(username, userToSend, storeToSend, msg);
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

        public static bool supplyProduct(string username, string storeName, string productName, int amount, string manufacturer)
        {
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
                return false;
            return BuissnessLayer.UserServices.supply(username, storeName, productName, amount, manufacturer);
        }

        // tries to place an offer
        // return value: in case of success - the id of the request, -1 otherwise
        public static int placeOffer(string username, string storeName, string productName, string category, string manufacturer,int amount, double price)
        {
            if (user.getUserName().Equals("guest") || !user.getUserName().Equals(username))
                return -1;
            return UserServices.placeOffer(username, storeName, productName, category, manufacturer,amount, price);
        }
    }
}
