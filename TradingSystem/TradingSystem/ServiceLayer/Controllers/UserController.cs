using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem.BuissnessLayer;

namespace TradingSystem.ServiceLayer
{
    public class UserController
    {
        public static aUser user = new Guest();
        public static bool login(string username, string password)
        {
            aUser logged = BuissnessLayer.UserServices.login(username, password);
            if ((logged == null))
                return false;
            user = logged;
            return true;

        }

        public static bool logout()
        {
            if(!user.getUserName().Equals("guest") && UserServices.onlineUsers.Contains(user.getUserName()))
            {
                user = new Guest();
                return UserServices.logout(user.getUserName());
            }
            return false;
        }
        public static aUser getCorrentOnlineUser()
        {
            return user;
        }
        public static bool register(string userName, string password)
        {
            return BuissnessLayer.UserServices.register(userName, password);
        }

        public static bool saveProduct(string userName, string storeName, string manufacturer, int amount, Dictionary<string, int> product)
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
            foreach (BuissnessLayer.commerce.Receipt receipt in temp)
            {                
                receipts.Add(ProductController.makeReceipt(receipt));
            }
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
            return BuissnessLayer.UserServices.browseStore(username,storeName).name;
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

        public static bool editProduct(string username, string storeName, int productName, double price, string manufacturer)
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

        //TODO
        private static SLemployee makeSLemployee(BuissnessLayer.aUser employee)
        {
            Object[] parameters = new Object[SLbasket.PARAMETER_COUNT];
            parameters[0] = employee.getUserName();
            parameters[1] = employee.GetAllPermissions();
            return (new SLemployee(parameters));
        }
    }
}
