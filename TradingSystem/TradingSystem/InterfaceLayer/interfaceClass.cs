using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSystem;


namespace TradingSystem
{
    public class interfaceClass
    {
        
        private static User user = new Guest();


        public static string getUsername
        {
            get { return user.getUserName(); }
            set { }
        }

        public static void loadMenu()
        {
            string username = "";
            string password;

            while (true)
            {
                printMenu();
                string option = Console.ReadLine();
                
                switch (option)
                {
                    case "1"://login
                        Console.WriteLine("Login- please enter username\n");
                        username = Console.ReadLine();
                        Console.WriteLine("Login- please enter your password, " + username + '\n');
                        password = Console.ReadLine();
                        interfaceClass.login(username, password);
                        break;

                    case "2"://register
                        Console.WriteLine("Register- please enter username\n");
                        username = Console.ReadLine();
                        Console.WriteLine("Register- please enter your password, " + username + '\n');
                        password = Console.ReadLine();
                        interfaceClass.register(username, password);
                        break;
                    case "3"://logout
                        interfaceClass.logout();
                        break;
                }
            }
        }
        

        // User 
        public static bool login(string username, string password){

            Member reg = User.login(username, password);
            if (reg == null)
                return false;
            user = reg;
            return true;
            
        }
        public static bool register(string username, string password)
        {
            if (!checkUserValidity(username) || !checkPasswordValidity(password))
                return false;
            else
                return user.register(username, password);
        }
        public static void logout(){
<<<<<<< HEAD
          
=======
>>>>>>> parent of eed0e08... added logs
            user = new Guest();
        }
        // Guest
        public static Dictionary<string, Dictionary<int, int>> browseProducts(string productName, double minPrice, double maxPrice, string category, string manufacturer){
            return StoresServices.searchProducts(productName, minPrice, maxPrice, category, manufacturer);
        }     
<<<<<<< HEAD
        public static Store browseStore(string name){
            return StoresServices.getStore(name);
        }
        public static bool saveProduct(string storeName, Dictionary<int, int> products) 
=======
        public Store browseStore(string name){
            return StoresServices.getStore(name);
        }
        public bool saveProduct(string storeName, Dictionary<int, int> products) 
>>>>>>> parent of eed0e08... added logs
        {
            return user.saveProduct(storeName, products);
        }
        public static bool removeProduct(string storeName, Dictionary<int, int> products)
        {
            return user.removeProduct(storeName, products);
        }

        public static ShoppingBasket checkShoppingBasketDetails(string storeName)
        {
            return user.checkShoppingBasketDetails(storeName);
        }

        public static bool purchase(string storeName)
        {
            return user.purchase(storeName);
        }

        // Member
        public static bool EstablishStore(string storeName)
        {
            return user.EstablishStore(storeName);
        }

        public static LinkedList<string> getPurchHistory()
        {
            return user.getPurchHistory();
        }

        // Manager
<<<<<<< HEAD
        public static bool addNewProduct(string storeName,int productId, double price, int amount){
=======
        public bool addNewProduct(string storeName,int productId, double price, int amount){
>>>>>>> parent of eed0e08... added logs
            if (!user.isManager(storeName))
                return false;
            LinkedList<object> args = new LinkedList<object>();
            args.AddFirst(storeName);
            args.AddLast(productId);
            args.AddLast(price);
            args.AddLast(amount);
            return (bool)((Member)user).GetUserDetails().doManage(storeName, "addProduct").todo(args);
        }
<<<<<<< HEAD
        public static bool removeProduct(string storeName,int productId){
=======
        public bool removeProduct(string storeName,int productId){
>>>>>>> parent of eed0e08... added logs
            if (!user.isManager(storeName))
                return false;
            LinkedList<object> args = new LinkedList<object>();
            args.AddFirst(storeName);
            args.AddLast(productId);
            return (bool)((Member)user).GetUserDetails().doManage(storeName, "removeProduct").todo(args);
        }
<<<<<<< HEAD
        public static bool editProduct(string storeName,int productId, double price){
=======
        public bool editProduct(string storeName,int productId, double price){
>>>>>>> parent of eed0e08... added logs
            if (!user.isManager(storeName))
                return false;
            LinkedList<object> args = new LinkedList<object>();
            args.AddFirst(storeName);
            args.AddLast(productId);
            args.AddLast(price);
            return (bool)((Member)user).GetUserDetails().doManage(storeName, "editProduct").todo(args);
        }
<<<<<<< HEAD
        public static bool editManagerPermissions(string storeName, string username, List<string> Permissions){
=======
        public bool editManagerPermissions(string storeName, string username, List<string> Permissions){
>>>>>>> parent of eed0e08... added logs
            if (!user.isManager(storeName))
                return false;
            LinkedList<object> args = new LinkedList<object>();
            args.AddFirst(storeName);
            args.AddLast(username);
            args.AddLast(username);
            return (bool)((Member)user).GetUserDetails().doManage(storeName, "editManagerPermissions").todo(args);
        }
<<<<<<< HEAD
        public static LinkedList<string> getPurchaseHistory(string storeName){
=======
        public LinkedList<string> getPurchaseHistory(string storeName){
>>>>>>> parent of eed0e08... added logs
            if (!user.isManager(storeName))
                return null;
            LinkedList<object> args = new LinkedList<object>();
            args.AddFirst(storeName);
            return (LinkedList<string>)((Member)user).GetUserDetails().doManage(storeName, "getPurchaseHistory").todo(args);
        }
<<<<<<< HEAD
        public static bool hireNewStoreManager(string storeName,string username){
=======
        public bool hireNewStoreManager(string storeName,string username){
>>>>>>> parent of eed0e08... added logs
            if (!user.isManager(storeName))
                return false;
            LinkedList<object> args = new LinkedList<object>();
            args.AddFirst(storeName);
            args.AddLast(username);
            return (bool)((Member)user).GetUserDetails().doManage(storeName, "hireNewStoreManager").todo(args);
        }
<<<<<<< HEAD
        public static bool hireNewStoreOwner(string storeName,string username, List<string> pre){
=======
        public bool hireNewStoreOwner(string storeName,string username, List<string> pre){
>>>>>>> parent of eed0e08... added logs
            if (!user.isManager(storeName))
                return false;
            LinkedList<object> args = new LinkedList<object>();
            args.AddFirst(storeName);
            args.AddLast(username);
            args.AddLast(pre);
            return (bool)((Member)user).GetUserDetails().doManage(storeName, "hireNewStoreOwner").todo(args);
        }
<<<<<<< HEAD
        public static bool removeManager(string storeName,string username){
=======
        public bool removeManager(string storeName,string username){
>>>>>>> parent of eed0e08... added logs
            if (!user.isManager(storeName))
                return false;
            LinkedList<object> args = new LinkedList<object>();
            args.AddFirst(storeName);
            args.AddLast(username);
            return (bool)((Member)user).GetUserDetails().doManage(storeName, "removeManager").todo(args);
        }
<<<<<<< HEAD
        public static LinkedList<string> getInfoEmployees(string storeName){
=======
        public LinkedList<string> getInfoEmployees(string storeName){
>>>>>>> parent of eed0e08... added logs
            if (!user.isManager(storeName))
                return null;
            LinkedList<object> args = new LinkedList<object>();
            args.AddFirst(storeName);
            return (LinkedList<string>)((Member)user).GetUserDetails().doManage(storeName, "getInfoEmployees").todo(args);
        }  
        
        private static void printMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("Login- please enter username\n");
            string username = Console.ReadLine();
            Console.WriteLine("Login- please enter your password, " + username + '\n');
            string password = Console.ReadLine();
            user = User.login(username, password);
        }

        private static bool checkUserValidity(string username)
        {
            return DataClass.isUserExists(username);
        }
        private static bool checkPasswordValidity(string password)
        {
            int countNum = 0;
            int countCap = 0;
            int countLet = 0;
            if (password.Length < 4 || password.Length > 20)
                return false;
            foreach (char value in password)
            {
                if (Char.IsDigit(value))
                    countNum++;
                if (97 <= (int)value && (int)value <= 122)
                    countLet++;
                if (60 <= (int)value && (int)value <= 90)
                    countCap++;
            }
            return (countNum > 0 & countLet > 0 & countCap > 0);
        }
    }
}
