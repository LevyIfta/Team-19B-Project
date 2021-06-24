using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.DataLayer;
using TradingSystem.BuissnessLayer.commerce;
using TradingSystem.BuissnessLayer.User.Permmisions;
using TradingSystem.BuissnessLayer.User;

namespace TradingSystem.BuissnessLayer
{
    public static class UserServices
    {

        
        public static ICollection<aUser> Users { get; private set; } = new List<aUser>();
        public static ICollection<string> onlineUsers { get; private set; } = new List<string>();
        public static ICollection<string> offlineUsers { get; private set; } = new List<string>() ;
        static UserServices()
        {
        //    UserServices.register("admin", "Admin1");
         //   UserServices.register("UserTest", "123Xx123");
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
            if (!ans && !DataLayer.ORM.DataAccess.isMemberExist(username))
                return new string[] { "false", "username not exist" };

            if (!DataLayer.ORM.DataAccess.getMember(username).password.Equals(password))
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
            string[] fullans = new string[2];
            if (DataLayer.ORM.DataAccess.isMemberExist(username))
                return new string[] { "false", "user already exist" };
            fullans[1] = checkUserNameValid(username) + ":";
            fullans[1] += checkPasswordValid(password);
            fullans[0] = "false";
            if(fullans[1].Length < 3)
            {
                //MemberDAL.addMember(new MemberData(username, password));
                DataLayer.ORM.DataAccess.create((new Member(username, password)).toDataObject());
                if (username.Equals("admin"))
                    Users.Add(new Admin(username, password));
                else
                    Users.Add(new Member(username, password));
                offlineUsers.Add(username);
                fullans[0] = "true";
            }
            return fullans;
        }
        public static string[] register(string username, string password, double age, string gender, string address)
        {
            string[] fullans = new string[2]
;            if (DataLayer.ORM.DataAccess.isMemberExist(username))
                return new string[] { "false", "user already exist" };
            fullans[1] = checkUserNameValid(username) + ":";
            fullans[1] += checkPasswordValid(password);
            fullans[0] = "false";
            if(fullans[1].Length < 3)
            {
                DataLayer.ORM.DataAccess.create(new MemberData(username, password, age, gender, address, new List<BasketInCart>(), new List<ReceiptData>(), new List<MessageData>()));
                if (username.Equals("admin"))
                    Users.Add(new Admin(username, password));
                else
                    Users.Add(new Member(username, password, age, gender, address));
                offlineUsers.Add(username);
                fullans[0] = "true";
            }
            return fullans;
        }
        // aUser
        public static bool saveProduct(string username, string storeName, string manufacturer, Dictionary<string, int> product)
        {
            aUser temp = getUser(username);
            if (temp == null)
                return false;
            Store store = browseStore(username, storeName);
            if (store == null)
                return false;
            ShoppingBasket basket = new ShoppingBasket(store, temp);
            foreach (string pName in product.Keys)
            {
                Product p1 = Stores.searchStore(storeName).searchProduct(pName, manufacturer);
                p1.amount = product[pName];
                basket.addProduct(p1);
            }

            return temp.saveProduct(basket);
        }
        public static bool removeProduct(string username, string storeName, string manufacturer, Dictionary<string, int> product)
        {
            aUser temp = getUser(username);
            if (temp == null)
                return false;
            Store store = browseStore(username, storeName);
            if (store == null)
                return false;
            ShoppingBasket basket = new ShoppingBasket(store, temp);
            foreach (string pName in product.Keys)
            {
                Product p1 = Stores.searchStore(storeName).searchProduct(pName, manufacturer);
                basket.addProduct(p1);
            }
            return temp.removeProduct(basket);
        }
        public static ShoppingBasket getBasket(string username, string storeName)
        {
            aUser temp = getUser(username);
            if (temp == null)
                return null;
            return temp.getBasket(Stores.searchStore(storeName));
        }
        public static ShoppingCart getCart(string username)
        {
            aUser temp = getUser(username);
            if (temp == null)
                return null;
            return temp.getCart();
        }
        public static bool EstablishStore(string username, string storeName)
        {
            aUser temp = getUser(username);
            if (temp == null)
                return false;
            return temp.EstablishStore(storeName);
        }
        public static double checkPrice(string username)
        {
            aUser temp = getUser(username);
            if (temp == null)
                return -1;
            return temp.checkPrice();
        }
        public static string[] purchase(string username, string creditNumber, string validity, string cvv)
        {
            /*
            PaymentMethod p = null;
            switch (paymentName)
            {
                case "Immediate":
                    p = new Immediate();
                    break;
                case "Offer":
                    p = new Offer();
                    break;
                case "Auction":
                    p = new Auction();
                    break;
                case "Raffle":
                    p = new Raffle();
                    break;
            }*/
            aUser temp = getUser(username);
            if (temp == null)
                return new string[] { "false", "user not exist"};
            return temp.purchase(creditNumber, validity, cvv);
        }
        public static Dictionary<Store, Product> browseProducts(string username, string productName, string manufacturer)
        {
            aUser temp = getUser(username);
            if (temp == null)
                return null;
            return temp.browseProducts(productName, manufacturer);
        }
        public static Dictionary<Store, Product> browseProducts(string username, string productName, string category, string manufacturer, double minPrice, double maxPrice)
        {
            aUser temp = getUser(username);
            if (temp == null)
                return null;
            return temp.browseProducts(productName, category, manufacturer, minPrice, maxPrice);
        }
        public static Store browseStore(string username, string storeName)
        {
            aUser temp = getUser(username);
            if (temp == null)
                return null;
            return temp.browseStore(storeName);
        }
        public static Dictionary<Product, List<string[]>> getAllProducts()
        {
            Dictionary<Product, List<string[]>> ans = new Dictionary<Product, List<string[]>>();
            foreach (Store store in Stores.getAllStores())
            {
                foreach(Product product in store.inventory)
                {
                    if (!ans.Keys.Contains(product))
                        ans[product] = new List<string[]>();
                    ans[product].Add(new string[] { store.name, product.price + "" });
                }
            }
            return ans;
        }
        public static ICollection<Receipt> getAllMyReceiptHistory(string username)
        {
            aUser temp = getUser(username);
            if (temp == null)
                return null;
            return temp.getPurchHistory();
        }
        public static ICollection<Receipt> getReceiptsHistory(string username, string storeName)
        {
            aUser temp = getUser(username);
            if (temp == null)
                return null;
            return temp.getMyPurchaseHistory(storeName);
        }
        public static ICollection<Receipt> getAllReceiptsHistory(string username, string storeName)
        {
            aUser temp = getUser(username);
            if (temp == null)
                return null;
            return temp.getPurchaseHistory(storeName);
        }
        public static bool addNewProduct(string username, string storeName, string productName, double price, int amount, string category, string manufacturer)
        {
            aUser temp = getUser(username);
            if (temp == null)
                return false;
            return temp.addNewProduct(storeName, productName, price, amount, category, manufacturer);
        }
        public static bool removeProduct(string username, string storeName, string productName, string manufacturer)
        {
            aUser temp = getUser(username);
            if (temp == null)
                return false;
            return temp.removeProduct(storeName, productName, manufacturer);
        }
        public static bool editProduct(string username, string storeName, string productName, double price, string manufacturer)
        {
            aUser temp = getUser(username);
            if (temp == null)
                return false;
            return temp.editProduct(storeName, productName, price, manufacturer);
        }
        public static bool supply(string username, string storeName, string productName, int amount, string manufacturer)
        {
            aUser temp = getUser(username);
            if (temp == null)
                return false;
            return temp.supply(storeName, productName, amount, manufacturer);
        }
        public static bool hireNewStoreManager(string username, string storeName, string userToHire)
        {
            aUser temp = getUser(username);
            if (temp == null || getUser(userToHire) == null)
                return false;
            return temp.hireNewStoreManager(storeName, userToHire);
        }
        public static bool editManagerPermissions(string username, string storeName, string userToHire, List<string> Permissions)
        {
            aUser temp = getUser(username);
            if (temp == null || getUser(userToHire) == null)
                return false;
            return temp.editManagerPermissions(storeName, userToHire, convertPermission(Permissions));
        }
        public static bool hireNewStoreOwner(string username, string storeName, string userToHire, List<string> Permissions)
        {
            aUser temp = getUser(username);
            if (temp == null || getUser(userToHire) == null)
                return false;
            return temp.hireNewStoreOwner(storeName, userToHire, convertPermission(Permissions));
        }
        public static bool removeManager(string username, string storeName, string userToHire)
        {
            aUser temp = getUser(username);
            if (temp == null || getUser(userToHire) == null || username.Equals(userToHire))
                return false;
            return temp.removeManager(storeName, userToHire);
        }
        public static bool removeOwner(string username, string storeName, string userToHire)
        {
            aUser temp = getUser(username);
            if (temp == null || getUser(userToHire) == null || username.Equals(userToHire))
                return false;
            return temp.removeOwner(storeName, userToHire);
        }
        public static string[] GetPermissions(string username, string storeName)
        {
            aUser temp = getUser(username);
            if (temp == null)
                return null;
            var list = temp.GetPermissions(storeName);
            string[] persmissions = new string[list.Count];
            int i = 0;
            foreach (PersmissionsTypes type in list)
            {
                persmissions[i] = (aPermission.who(type));
                i++;
            }
            return persmissions;
        }
        public static string[] GetMyStores(string username)
        {
            aUser temp = getUser(username);
            if (temp == null)
                return null;
            var list = temp.GetAllPermissions();
            if(list != null)
            {
                string[] persmissions = new string[list.Count];
                int i = 0;
                foreach (string type in list.Keys)
                {
                    persmissions[i] = type;
                    i++;
                }
                return persmissions;
            }
            return null;
        }
        public static string[] getMessages(string username, string storename)
        {
            aUser temp = getUser(username);
            if (temp == null)
                return null;
            var msg = Stores.searchStore(storename).messages;
            string[] ans = new string[msg.Count];
            int i = 0;
            foreach (Message message in msg)
            {
                ans[i] = message.SenderName + "$" + message.StoreToSend + "$" + message.UserToSend + "$" + message.Msg + "$" + message.isNew.ToString();
                i++;
            }
            return ans;
        }
        public static string[] getMessages(string username)
        {
            aUser temp = getUser(username);
            if (temp == null)
                return null;
            var msg = ((Member)temp).messages;
            string[] ans = new string[msg.Count];
            int i = 0;
            foreach (Message message in msg)
            {
                ans[i] = message.SenderName + "$" + message.StoreToSend + "$" + message.UserToSend + "$" + message.Msg + "$" + message.isNew.ToString();
                i++;
            }
            return ans;
        }
        public static ICollection<Member> getInfoEmployees(string username, string storeName)
        {
            aUser temp = getUser(username);
            if (temp == null)
                return null;
            return temp.getInfoEmployees(storeName);
        }

        public static bool leaveFeedback(string username, string storeName, string productName, string manufacturer, string comment)
        {
            aUser temp = getUser(username);
           /* if (!temp.canLeaveFeedback)
                return false;*/
            return Stores.searchStore(storeName).searchProduct(productName, manufacturer).info.leaveFeedback(username, comment);
        }

        public static Dictionary<string, string> getAllFeedbacks (string storeName, string productName, string manufacturer)
        {
            return Stores.searchStore(storeName).searchProduct(productName, manufacturer).info.getAllFeedbacks();
        }
        public static Dictionary<string, string> getAllFeedbacks(string storeName, string productName)
        {
            return Stores.searchStore(storeName).searchProduct(productName).info.getAllFeedbacks();
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
        public static void addGuest()
        {
            Users.Add(new Guest());
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
            var mangers = Stores.searchStore(storeName).getManagers();
            var owners = Stores.searchStore(storeName).owners;
            foreach (Member user in mangers.ToList())
            {
                if (user.removePermission(storeName, sponser))
                {
                    Stores.searchStore(storeName).removeManager(user);
                    Stores.searchStore(storeName).removeOwner(user);
                }
            }
            
            foreach (Member user in owners.ToList())
            {
                if (user.removePermission(storeName, sponser))
                {
                    Stores.searchStore(storeName).removeManager(user);
                    Stores.searchStore(storeName).removeOwner(user);
                }
            }
            return true;
        }
        public static bool closeStore(string username, string storeName)
        {
            Store store = Stores.searchStore(storeName);
            if (store.isManager(username) || store.isOwner(username))
            {
                return Stores.closeStore(store);
            }
            else
                return false;
        }
        public static bool reopenStore(string username, string storeName)
        {
            Store store = Stores.searchStore(storeName);
            if (store.isManager(username) || store.isOwner(username))
            {
                return Stores.reopenStore(store);
            }
            else
                return false;
        }
        public static bool sendMessage(string username, string userToSend, string storeToSend, string msg)
        {
            if (userToSend != null && userToSend.Length > 0)
            {
                aUser temp = getUser(userToSend);
                return temp.sendMessage(new Message(username, storeToSend, userToSend, msg, true));
            }
            else if (storeToSend != null && storeToSend.Length > 0)
            {
                Store temp = Stores.searchStore(storeToSend);
                return temp.sendMessage(new Message(username, storeToSend, userToSend, msg, true));
            }
            return false;
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



        public static void LoadUser(MemberData member )
        {
            aUser user;
            if (member.userName != "admin")
                user = new Member(member);
            else
                user = new Admin(member);
            Users.Add(user);
            offlineUsers.Add(user.userName);

        }

        public static int placeOffer(string username, string storeName, string productName, string category, string manufacturer,int amount, double price)
        {
            aUser requester = getUser(username);
            Store store = Stores.searchStore(storeName);
            ProductInfo pInfo = ProductInfo.getProductInfo(productName, category, manufacturer);

            if (requester == null | store == null | pInfo == null | price < 0)
                return -1;

            Product product = new Product(pInfo, amount, price);
            OfferRequest request = new OfferRequest(product, requester, store);

            requester.placeOffer(request);
            return request.id;
        }
    }
}