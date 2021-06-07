using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientWeb.Connection;
using System.Windows;

using System.Threading;
using System.Windows.Controls;
using ClientWeb.Objects;

namespace ClientWeb
{
    class Controller
    {
        private static Controller instance = null;


        private Controller()
        {
            Thread alarmthread = new Thread(new ThreadStart( Alarmanager.start ));
            alarmthread.Start();
            System.Threading.Thread.Sleep(1);
        }

        public static Controller GetController()
        {
            if (instance == null)
                instance = new Controller();
            return instance;
        }

        public void disconnect()
        {
            
            ConnectionManager.disconnect();
        }


        private DecodedMessge readMessage()
        {
           
            return Alarmanager.getMsg();
        }


        public string[] Login(string username, string password)
        {
            DecodedMessge msg = new DecodedMessge();
            msg.type = msgType.FUNC;
            msg.name = "login";
            msg.param_list = new string[] { username, password };
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);
          
            DecodedMessge ans_d = readMessage();
            if (ans_d.type == msgType.OBJ && ans_d.name == "string[]")
            {
                return ans_d.param_list;
            }
            return null;
        }
        public bool Logoutfunc()
        {
            DecodedMessge msg = new DecodedMessge();
            msg.type = msgType.FUNC;
            msg.name = "logout";
            msg.param_list = new string[] { };
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);
    
            DecodedMessge ans_d = readMessage();
            bool ans = false;
            if (ans_d.type == msgType.OBJ && ans_d.name == "bool")
            {
                ans = ans_d.param_list[0] == "true";
            }
            return ans;
        }
        public string getUserName()
        {
          
            DecodedMessge msg = new DecodedMessge();
            msg.type = msgType.FUNC;
            msg.name = "get online user name";
            msg.param_list = new string[] { };
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            string ans = "guest";
            if (ans_d.type == msgType.OBJ && ans_d.name == "string")
            {
                ans = ans_d.param_list[0];
            }
            return ans;
        }

        public string[] Register(string username, string password)
        {
            DecodedMessge msg = new DecodedMessge();
            msg.type = msgType.FUNC;
            msg.name = "register";
            msg.param_list = new string[] { username, password };
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            if (ans_d.type == msgType.OBJ && ans_d.name == "string[]")
            {
                return ans_d.param_list;
            }
            return null;
        }

        public bool OpenStore(string username, string storename)
        {
            DecodedMessge msg = new DecodedMessge();
            msg.type = msgType.FUNC;
            msg.name = "open store";
            msg.param_list = new string[] { username, storename };
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);
       
            DecodedMessge ans_d = readMessage();
            bool ans = false;
            if(ans_d.type == msgType.OBJ && ans_d.name == "bool")
            {
                ans = ans_d.param_list[0] == "true";
            }
            return ans;
        }

        public string GetStore(string username, string storename)
        {
            DecodedMessge msg = new DecodedMessge();
            msg.type = msgType.FUNC;
            msg.name = "browse store";
            msg.param_list = new string[] { username, storename };
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            string ans = "";
            if (ans_d.type == msgType.OBJ && ans_d.name == "string")
            {
                ans = ans_d.param_list[0];
            }
            return ans;
        }
        public string SearchStore(string storeName)
        {
            DecodedMessge msg = new DecodedMessge();
            // init message fields
            msg.type = msgType.FUNC;
            msg.name = "search store";
            msg.param_list = new string[] { storeName };
            // encode and send message
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            string ans = "";

            if (ans_d.type == msgType.OBJ && ans_d.name == "string")
            {
                ans = ans_d.param_list[0];
            }
            return ans;
        }
        public ICollection<string> SearchStores(string storeName)
        {
            DecodedMessge msg = new DecodedMessge();
            // init message fields
            msg.type = msgType.FUNC;
            msg.name = "search stores";
            msg.param_list = new string[] { storeName };
            // encode and send message
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            bool ans = false;
            ICollection<string> stores = new LinkedList<string>();

            if (ans_d.type == msgType.OBJ && ans_d.name == "bool")
            {
                ans = ans_d.param_list[0] == "true";
            }
            return stores;
        }
        public bool SaveProduct(string username, string storeName, string manu, string products)
        { // products -> product$product$product -> name&amount 
            DecodedMessge msg = new DecodedMessge();
            // init message fields
            msg.type = msgType.FUNC;
            msg.name = "save product";
            msg.param_list = new string[] { username, storeName, manu, products };
            // encode and send message
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            bool ans = false;
            if (ans_d.type == msgType.OBJ && ans_d.name == "bool")
            {
                ans = ans_d.param_list[0] == "true";
            }
            return ans;
        }
        public bool RemoveProducts(string username, string storeName, string manu, string products)
        { // products -> product$product$product -> name&amount 
            DecodedMessge msg = new DecodedMessge();
            // init message fields
            msg.type = msgType.FUNC;
            msg.name = "remove product";
            msg.param_list = new string[] { username, storeName, manu, products };
            // encode and send message
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            bool ans = false;
            if (ans_d.type == msgType.OBJ && ans_d.name == "bool")
            {
                ans = ans_d.param_list[0] == "true";
            }
            return ans;
        }
        public string[] GetBasket(string username, string storeName)
        { // products -> product$product$product -> name&amount 
            DecodedMessge msg = new DecodedMessge();
            // init message fields
            msg.type = msgType.FUNC;
            msg.name = "get basket";
            msg.param_list = new string[] { username, storeName };
            // encode and send message
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            if (ans_d.type == msgType.OBJ && ans_d.name == "basket")
            {
                return ans_d.param_list;
            }
            return null;
        }
        public string[] GetCart(string username)
        { // products -> product$product$product -> name&amount 
            DecodedMessge msg = new DecodedMessge();
            // init message fields
            msg.type = msgType.FUNC;
            msg.name = "get cart";
            msg.param_list = new string[] { username };
            // encode and send message
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            if (ans_d.type == msgType.OBJ && ans_d.name == "cart")
            {
                return ans_d.param_list;
            }
            return null;
        }
        public string[] Purchase(string username, string paymant)
        { // products -> product$product$product -> name&amount 
            DecodedMessge msg = new DecodedMessge();
            // init message fields
            msg.type = msgType.FUNC;
            msg.name = "purchase";
            msg.param_list = new string[] { username, paymant };
            // encode and send message
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            if (ans_d.type == msgType.OBJ && ans_d.name == "receipts")
            {
                return ans_d.param_list;
            }
            return null;
        }
        public string CheckPrice(string username)
        { // products -> product$product$product -> name&amount 
            DecodedMessge msg = new DecodedMessge();
            // init message fields
            msg.type = msgType.FUNC;
            msg.name = "check price";
            msg.param_list = new string[] { username };
            // encode and send message
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            string ans = "";
            if (ans_d.type == msgType.OBJ && ans_d.name == "double")
            {
                ans = ans_d.param_list[0];
            }
            return ans;
        }
        public Dictionary<string, SLproduct> BrowseProducts(string username, string productname, string manu)
        { // products -> product$product$product -> name&amount 
            DecodedMessge msg = new DecodedMessge();
            // init message fields
            msg.type = msgType.FUNC;
            msg.name = "browse products";
            msg.param_list = new string[] { username, productname, manu };
            // encode and send message
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            if (ans_d.type == msgType.OBJ && ans_d.name == "dictionary product")
            {
                return SLproduct.makeObjectsWithStore(ans_d.param_list);
            }
            else
                throw new TypeLoadException();
            return null;
        }
        public string[] GetReceiptsHistory(string username, string storename)
        { // products -> product$product$product -> name&amount 
            DecodedMessge msg = new DecodedMessge();
            // init message fields
            msg.type = msgType.FUNC;
            msg.name = "get receipts in store";
            msg.param_list = new string[] { username, storename };
            // encode and send message
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            if (ans_d.type == msgType.OBJ && ans_d.name == "receipts")
            {
                return ans_d.param_list;
            }
            return null;
        }
        public string[] GetAllReceiptsHistory(string username, string storename)
        { // products -> product$product$product -> name&amount 
            DecodedMessge msg = new DecodedMessge();
            // init message fields
            msg.type = msgType.FUNC;
            msg.name = "get all receipts in store";
            msg.param_list = new string[] { username, storename };
            // encode and send message
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            if (ans_d.type == msgType.OBJ && ans_d.name == "receipts")
            {
                return ans_d.param_list;
            }
            return null;
        }
        public string[] GetAllMyReceiptHistory(string username)
        { // products -> product$product$product -> name&amount 
            DecodedMessge msg = new DecodedMessge();
            // init message fields
            msg.type = msgType.FUNC;
            msg.name = "get all my receipts";
            msg.param_list = new string[] { username };
            // encode and send message
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            if (ans_d.type == msgType.OBJ && ans_d.name == "receipts")
            {
                return ans_d.param_list;
            }
            return null;
        }
        public string[] GetInfoEmployees(string username, string storename)
        { // products -> product$product$product -> name&amount 
            DecodedMessge msg = new DecodedMessge();
            // init message fields
            msg.type = msgType.FUNC;
            msg.name = "get my employees";
            msg.param_list = new string[] { username, storename };
            // encode and send message
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            if (ans_d.type == msgType.OBJ && ans_d.name == "users")
            {
                return ans_d.param_list;
            }
            return null;
        }
        public bool AddNewProduct(string username, string storename, string productname, string price, string amount, string category, string manu)
        {
            DecodedMessge msg = new DecodedMessge();
            msg.type = msgType.FUNC;
            msg.name = "add new product to store";
            msg.param_list = new string[] { username, storename, productname, price, amount, category, manu };
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            bool ans = false;
            if (ans_d.type == msgType.OBJ && ans_d.name == "bool")
            {
                ans = ans_d.param_list[0] == "true";
            }
            return ans;
        }
        public bool RemoveProductFromStore(string username, string storename, string productname, string manu)
        {
            DecodedMessge msg = new DecodedMessge();
            msg.type = msgType.FUNC;
            msg.name = "remove product from store";
            msg.param_list = new string[] { username, storename, productname, manu };
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            bool ans = false;
            if (ans_d.type == msgType.OBJ && ans_d.name == "bool")
            {
                ans = ans_d.param_list[0] == "true";
            }
            return ans;
        }
        public bool EditProduct(string username, string storename, string productname, string price, string manu)
        {
            DecodedMessge msg = new DecodedMessge();
            msg.type = msgType.FUNC;
            msg.name = "edit product from store";
            msg.param_list = new string[] { username, storename, productname, price, manu };
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            bool ans = false;
            if (ans_d.type == msgType.OBJ && ans_d.name == "bool")
            {
                ans = ans_d.param_list[0] == "true";
            }
            return ans;
        }
        public bool HireNewStoreManager(string username, string storeName, string userToHire)
        {
            DecodedMessge msg = new DecodedMessge();
            msg.type = msgType.FUNC;
            msg.name = "hire new manager";
            msg.param_list = new string[] { username, storeName, userToHire };
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            bool ans = false;
            if (ans_d.type == msgType.OBJ && ans_d.name == "bool")
            {
                ans = ans_d.param_list[0] == "true";
            }
            return ans;
        }
        public bool EditManagerPermissions(string username, string storeName, string userToHire, string permissions)
        {
            DecodedMessge msg = new DecodedMessge();
            msg.type = msgType.FUNC;
            msg.name = "edit manager";
            msg.param_list = new string[] { username, storeName, userToHire, permissions };
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            bool ans = false;
            if (ans_d.type == msgType.OBJ && ans_d.name == "bool")
            {
                ans = ans_d.param_list[0] == "true";
            }
            return ans;
        }
        public bool HireNewStoreOwner(string username, string storeName, string userToHire, string permissions)
        {
            DecodedMessge msg = new DecodedMessge();
            msg.type = msgType.FUNC;
            msg.name = "hire new owner";
            msg.param_list = new string[] { username, storeName, userToHire, permissions };
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            bool ans = false;
            if (ans_d.type == msgType.OBJ && ans_d.name == "bool")
            {
                ans = ans_d.param_list[0] == "true";
            }
            return ans;
        }
        public bool RemoveManager(string username, string storeName, string userToHire)
        {
            DecodedMessge msg = new DecodedMessge();
            msg.type = msgType.FUNC;
            msg.name = "remove manager";
            msg.param_list = new string[] { username, storeName, userToHire };
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            bool ans = false;
            if (ans_d.type == msgType.OBJ && ans_d.name == "bool")
            {
                ans = ans_d.param_list[0] == "true";
            }
            return ans;
        }
        public bool LeaveFeedback(string username, string storeName, string productName, string manufacturer, string comment)
        {
            DecodedMessge msg = new DecodedMessge();
            msg.type = msgType.FUNC;
            msg.name = "leave feedback";
            msg.param_list = new string[] { username, storeName, productName, manufacturer, comment };
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            bool ans = false;
            if (ans_d.type == msgType.OBJ && ans_d.name == "bool")
            {
                ans = ans_d.param_list[0] == "true";
            }
            return ans;
        }
        public string getAllFeedbacks(string storeName, string productName, string manufacturer)
        { // products -> product$product$product -> name&amount 
            DecodedMessge msg = new DecodedMessge();
            // init message fields
            msg.type = msgType.FUNC;
            msg.name = "get all feedbacks";
            msg.param_list = new string[] { storeName, productName, manufacturer };
            // encode and send message
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            string ans = "";
            if (ans_d.type == msgType.OBJ && ans_d.name == "feedbacks")
            {
                ans = ans_d.param_list[0];
            }
            return ans;
        }
        public string[] GetPermissions(string username, string storename)
        {
            DecodedMessge msg = new DecodedMessge();
            // init message fields
            msg.type = msgType.FUNC;
            msg.name = "get my permission on store";
            msg.param_list = new string[] { username, storename };
            // encode and send message
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            if (ans_d.type == msgType.OBJ && ans_d.name == "string[]")
            {
                return ans_d.param_list;
            }
            return null;
        }
        public string[] GetMyStore(string username)
        {
            DecodedMessge msg = new DecodedMessge();
            // init message fields
            msg.type = msgType.FUNC;
            msg.name = "get my stores";
            msg.param_list = new string[] { username };
            // encode and send message
            byte[] enc = Connection.Encoder.encode(msg);
            Connection.ConnectionManager.sendMessage(enc);

            DecodedMessge ans_d = readMessage();
            if (ans_d.type == msgType.OBJ && ans_d.name == "string[]")
            {
                return ans_d.param_list;
            }
            return null;
        }

        public string test()
        {
            DecodedMessge msg = new DecodedMessge();    
            msg.type = msgType.FUNC;
            msg.name = "username";
            msg.param_list = new string[] { "zzz"};

            byte[] enc = Connection.Encoder.encode(msg);

            Connection.ConnectionManager.sendMessage(enc);
 
            DecodedMessge ans_d = readMessage();
            return "blup";
        }
        /*public ICollection<SLproduct> searchProduct(string storeName, string productName, string category, string manufacturer, double minPrice, TextBox maxPrice)
        {
            throw new NotImplementedException();
        }*/ //todo


    }
}
