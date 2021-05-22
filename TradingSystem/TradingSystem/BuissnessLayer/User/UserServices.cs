using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;
using TradingSystem.BuissnessLayer.commerce;
using TradingSystem.BuissnessLayer.User.Permmisions;

namespace TradingSystem.BuissnessLayer
{
    public static class UserServices
    {
        
        public static ICollection<aUser> Users { get; private set; } = new List<aUser>();
        public static ICollection<string> onlineUsers { get; private set; } = new List<string>();
        public static ICollection<string> offlineUsers { get; private set; } = new List<string>() ;
        static UserServices()
        {
            UserServices.register("admin", "Admin1");
            //Users.Add(new Admin("admin", "Admin1"));
        }
        // menu functions
        // users
        public static aUser getAdmin() 
        {
            return getUser("admin");
        }
        public static string[] login(string username, string password)
        {
            if (onlineUsers.Contains(username))
                return new string[] { "false", "user already login" };
            bool ans = false;
            foreach (aUser user1 in Users)
            {
                if (user1.getUserName().Equals(username))
                    ans = true;
            }
            if (!ans && !MemberDAL.isExist(username))
                return new string[] { "false", "username not exist" };

            if (!MemberDAL.getMember(username).password.Equals(password))
                return new string[] { "false", "password incorrect" };
            onlineUsers.Add(username);
            offlineUsers.Remove(username);
            //Users.Add(new Member(MemberDAL.getMember(username)));
            return new string[] { "true", "" };
        }
        public static bool logout(string username)
        {
            onlineUsers.Remove(username);
            offlineUsers.Add(username);
            return true;
        }

        public static string[] register(string username, string password)
        {
            string[] fullans = new string[2]
;            if (MemberDAL.isExist(username))
                return new string[] { "false", "user already exist" };
            fullans[1] = checkUserNameValid(username) + ":";
            fullans[1] += checkPasswordValid(password);
            fullans[0] = "false";
            if(fullans[1].Length < 3)
            {
                MemberDAL.addMember(new MemberData(username, password));
                Users.Add(new Member(username, password));
                offlineUsers.Add(username);
                fullans[0] = "true";
            }
            return fullans;
        }
        public static bool register(string username, string password, double age, string gender)
        {
            if (MemberDAL.isExist(username))
                return false;
           /* if (!checkUserNameValid(username))
                return false;
            if (!checkPasswordValid(password))
                return false;*/
            MemberDAL.addMember(new MemberData(username, password, age, gender));
            Users.Add(new Member(username, password, age, gender));
            offlineUsers.Add(username);
            return true;
        }
        // aUser
        public static bool saveProduct(string username, string storeName, string manufacturer, Dictionary<string, int> product)
        {
            ShoppingBasket basket = new ShoppingBasket(new BasketData(storeName, username));
            foreach (string pName in product.Keys)
            {
                Product p1 = Stores.searchStore(storeName).searchProduct(pName, manufacturer);
                p1.amount = product[pName];
                basket.addProduct(p1);
            }
            return getUser(username).saveProduct(basket);
        }
        public static bool removeProduct(string username, string storeName, string manufacturer, Dictionary<string, int> product)
        {
            ShoppingBasket basket = new ShoppingBasket(new BasketData(storeName, username));
            foreach (string pName in product.Keys)
            {
                Product p1 = Stores.searchStore(storeName).searchProduct(pName, manufacturer);
                basket.addProduct(p1);
            }
            return getUser(username).removeProduct(basket);
        }
        public static ShoppingBasket getBasket(string username, string storeName)
        {
            return getUser(username).getBasket(Stores.searchStore(storeName));
        }
        public static ShoppingCart getCart(string username)
        {
            return getUser(username).getCart();
        }
        public static bool EstablishStore(string username, string storeName)
        {
            return getUser(username).EstablishStore(storeName);
        }
        public static double checkPrice(string username)
        {
            return getUser(username).checkPrice();
        }
        public static ICollection<Receipt> purchase(string username, string paymentName)
        {
            PaymentMethod p = null;
            switch (paymentName)
            {
                case "Immediate":
                    p = new Immediate();
                    break;
                /*case "Offer":
                    p = new Offer();
                    break;
                case "Auction":
                    p = new Auction();
                    break;
                case "Raffle":
                    p = new Raffle();
                    break;*/
            }
            return getUser(username).purchase(p);
        }
        public static Dictionary<Store, Product> browseProducts(string username, string productName, string manufacturer)
        {
            return getUser(username).browseProducts(productName, manufacturer);
        }
        public static Dictionary<Store, Product> browseProducts(string username, string productName, string category, string manufacturer, double minPrice, double maxPrice)
        {
            return getUser(username).browseProducts(productName, category, manufacturer, minPrice, maxPrice);
        }
        public static Store browseStore(string username, string storeName)
        {
            return getUser(username).browseStore(storeName);
        }
        public static ICollection<Receipt> getAllMyReceiptHistory(string username)
        {
            return getUser(username).getPurchHistory();
        }
        public static ICollection<Receipt> getReceiptsHistory(string username, string storeName)
        {
            return getUser(username).getMyPurchaseHistory(storeName);
        }
        public static ICollection<Receipt> getAllReceiptsHistory(string username, string storeName)
        {
            return getUser(username).getPurchaseHistory(storeName);
        }
        public static bool addNewProduct(string username, string storeName, string productName, double price, int amount, string category, string manufacturer)
        {
            return getUser(username).addNewProduct(storeName, productName, price, amount, category, manufacturer);
        }
        public static bool removeProduct(string username, string storeName, string productName, string manufacturer)
        {
            return getUser(username).removeProduct(storeName, productName, manufacturer);
        }
        public static bool editProduct(string username, string storeName, string productName, double price, string manufacturer)
        {
            return getUser(username).editProduct(storeName, productName, price, manufacturer);
        }
        public static bool hireNewStoreManager(string username, string storeName, string userToHire)
        {
            return getUser(username).hireNewStoreManager(storeName, userToHire);
        }
        public static bool editManagerPermissions(string username, string storeName, string userToHire, List<string> Permissions)
        {
            return getUser(username).editManagerPermissions(storeName, userToHire, convertPermission(Permissions));
        }
        public static bool hireNewStoreOwner(string username, string storeName, string userToHire, List<string> Permissions)
        {
            return getUser(username).hireNewStoreOwner(storeName, userToHire, convertPermission(Permissions));
        }
        public static bool removeManager(string username, string storeName, string userToHire)
        {
            return getUser(username).removeManager(storeName, userToHire);
        }
        public static ICollection<aUser> getInfoEmployees(string username, string storeName)
        {
            return getUser(username).getInfoEmployees(storeName);
        }

        public static bool leaveFeedback (string username, string storeName, string productName, string manufacturer, string comment)
        {
            return Stores.searchStore(storeName).searchProduct(productName, manufacturer).info.leaveFeedback(username, comment);
        }

        public static Dictionary<string, string> getAllFeedbacks (string storeName, string productName, string manufacturer)
        {
            return Stores.searchStore(storeName).searchProduct(productName, manufacturer).info.getAllFeedbacks();
        }


        // users
        public static aUser getUser(string username)
        {
            foreach (aUser user in Users)
            {
                if (user.getUserName().Equals(username))
                    return user;
            }
            return null;
        }
        public static int countOnlineUsers()
        {
            return onlineUsers.Count;
        }
        public static bool isUserOnline(string username)
        {
            return onlineUsers.Contains(username);
        }


        // other
        private static List<PersmissionsTypes> convertPermission(List<string> list)
        {
            List<PersmissionsTypes> ans = new List<PersmissionsTypes>();
            foreach (string name in list)
            {
                switch (name)
                {
                    case "AddProduct":
                        ans.Add(PersmissionsTypes.AddProduct);
                        break;
                    case "EditManagerPermissions":
                        ans.Add(PersmissionsTypes.EditManagerPermissions);
                        break;
                    case "EditProduct":
                        ans.Add(PersmissionsTypes.EditProduct);
                        break;
                    case "GetInfoEmployees":
                        ans.Add(PersmissionsTypes.GetInfoEmployees);
                        break;
                    case "GetPurchaseHistory":
                        ans.Add(PersmissionsTypes.GetPurchaseHistory);
                        break;
                    case "HireNewStoreManager":
                        ans.Add(PersmissionsTypes.HireNewStoreManager);
                        break;
                    case "HireNewStoreOwner":
                        ans.Add(PersmissionsTypes.HireNewStoreOwner);
                        break;
                    case "RemoveManager":
                        ans.Add(PersmissionsTypes.RemoveManager);
                        break;
                    case "RemoveProduct":
                        ans.Add(PersmissionsTypes.RemoveProduct);
                        break;
                }
            }
            return ans;
        }
        public static bool removeEmployeesPermission(string storeName, string sponser)
        {
            foreach (Member user in Users)
            {
                if (user.removePermission(storeName, sponser))
                {
                    Stores.searchStore(storeName).removeManager(user);
                    Stores.searchStore(storeName).removeOwner(user);
                }
            }
            return true;
        }

        private static string checkUserNameValid(string username)
        {
            if (username == null || username.Length < 4)
                return "username too short";
            if (username.Length > 15)
                return "username too long";
            return "";
        }
        private static string checkPasswordValid(string password)
        {
            if (password == null || password.Length < 4)
                return "password too short";
            if (password.Length > 20 )
                return "password too long";
            string ans = "";
            if (!containNumber(password))
                ans += "numbers ";
            if (!containLatter(password))
                ans += "latters ";
            if (!containCapital(password))
                ans += "capital";
            string fullans = "";
            if(ans.Length > 0)
            {
                string[] str = ans.Split(' ');
                for(int i=0; i<str.Length; i++)
                {
                    if(str[i].Length > 2)
                    {
                        if (fullans.Length != 0)
                            fullans += " and ";
                        fullans += str[i];
                    }
                }
            }
            if (fullans.Length > 0)
                fullans = "password doesn't contain " + fullans;
            
            return fullans;
        }
        private static bool containNumber(string str)
        {
            foreach (char letter in str)
            {
                if (48 <= (int)letter && (int)letter <= 57)
                    return true;
            }
            return false;
        }
        private static bool containCapital(string str)
        {
            foreach (char letter in str)
            {
                if (65 <= (int)letter && (int)letter <= 90)
                    return true;
            }
            return false;
        }
        private static bool containLatter(string str)
        {
            foreach (char letter in str)
            {
                if (97 <= (int)letter && (int)letter <= 122)
                    return true;
            }
            return false;
        }
    }
}