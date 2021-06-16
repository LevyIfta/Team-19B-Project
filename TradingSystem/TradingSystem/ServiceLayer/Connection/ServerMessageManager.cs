using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TradingSystem;
using TradingSystem.BuissnessLayer;
using TradingSystem.ServiceLayer.Connection;

namespace TradingSystem.ServiceLayer
{
    
    class ServerMessageManager
    {

        public static readonly int EOT = 4;

        private static readonly string certPath = "C:/Users/Iftah/Desktop/Cert.txt";
        /// <summary>
        /// input:array parameters:
        /// 0: socket. unused
        /// 1: the queue used to store message to send to client
        /// 2: the event to trigger when you add a message to the queue
        /// 3: event to wait on untill a new alarm comes in
        /// 4: the function to check if there is an alarm
        /// 5: the function to fetch an alarm
        /// </summary>
        /// <param name="parameters"></param>


        private static void messagesHandler(object parameters)
        {
            object[] input = (object[])parameters;
        
            SslStream stream = (SslStream)input[0];
            Queue<DecodedMessge> qwewe = (Queue<DecodedMessge>)input[1];
            AutoResetEvent waitEvent = (AutoResetEvent)input[2];
            Action<DecodedMessge> add2Q = (DecodedMessge msg) =>
            {
                lock (qwewe)
                {
                    qwewe.Enqueue(msg);
                    waitEvent.Set();
                }
            };
            int serialNum = Publisher.subscribe(UserController.getUserNameFunc(), add2Q);


            List<byte> data = new List<byte>();
            try
            {
                while (true)
                {
                    char c = (char)stream.ReadByte();
                    if (c == EOT)
                    {
                        DecodedMessge response = act(Decoder.decode(data.ToArray()));

                      //  byte[] enc_os = TradingSystem.ServiceLayer.Encoder.encode(response);
                        lock (qwewe)
                        {
                            qwewe.Enqueue(response);
                            waitEvent.Set();
                        }
                        data = new List<byte>();
                        Publisher.awaken(serialNum);
                    }
                    else
                        data.Add(Convert.ToByte(c));

                }
            }
            catch(NotImplementedException e)
            {
                stream.Close();
                throw e;
            }
        }

        private static void sendHandler(object parameters)
        {
            object[] input = (object[])parameters;
            SslStream stream = (SslStream)input[0];

            Queue<DecodedMessge> qwewe = (Queue<DecodedMessge>)input[1];
            AutoResetEvent waitEvent = (AutoResetEvent)input[2];
            while (true)
            {
                while (qwewe.Count == 0)
                    waitEvent.WaitOne();
                lock (qwewe)
                {
                    ServerConnectionManager.sendMessage(Encoder.encode(qwewe.Dequeue()), stream);
                }
            }

        }


        public static void threadsMain(object client)
        {

            UserController.threadInit();
            TcpClient socket = (TcpClient)client;
            SslStream stream = new SslStream(socket.GetStream());
            //  X509Certificate certificate = new X509Certificate();
            //  stream.AuthenticateAsServer(certificate, false, System.Security.Authentication.SslProtocols.Default, false);
            string certificate_String = "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";
            var privateKeyBytes = Convert.FromBase64String(certificate_String);
            var pfxPassword = "TradePassword";
            X509Certificate2 cert = new X509Certificate2(privateKeyBytes, pfxPassword);

            stream.AuthenticateAsServer(cert, false, System.Security.Authentication.SslProtocols.Default, false);
            AutoResetEvent waitEvent = new AutoResetEvent(false);
            Queue<DecodedMessge> qwewe = new Queue<DecodedMessge>();
            AutoResetEvent alarmLock = new AutoResetEvent(false);
            //Thread alarmHandler = new Thread(new ParameterizedThreadStart(AlarmHandler));
            Thread sendhandler = new Thread(new ParameterizedThreadStart(sendHandler));

            object[] parameters = new object[] { stream, qwewe, waitEvent };
            //alarmHandler.Start(parameters);
            
            sendhandler.Start(parameters);
            messagesHandler(parameters);
        }


        private static List<string> StringToList(string str)
        {
            List<string> list = new List<string>();
            string[] Permissions = str.Split('$');
            foreach (string pre in Permissions)
            {
                list.Add(pre);
            }
            return list;

        }


        private static Dictionary<string, int> StringToDictionary(string str)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            string[] products = str.Split('$');
            foreach (string product in products)
            {
                string[] ans = product.Split('&');
                dic[ans[0]] = int.Parse(ans[1]);
            }
            return dic;
        }
        private static string[] DictionaryToString(Dictionary<string, SLproduct> dic) // store name : product
        {
            string[] ans = new string[dic.Count];
            int i = 0;
            foreach (string store in dic.Keys)
            {
                ans[i] = store + "$" + ProductToString(dic[store]);
                i++;
            }
            return ans;
        } // store1$bamba^10.3^manu1^food^10^almog#what i think_gal#what he think store2$bamba^10.3^manu1^food^10^almog#what i think_gal#what he think

        private static string ProductToString(SLproduct pro)
        { // product name^price^manu^category^amount^feedback
            return pro.productName + "^" + pro.price + "^" + pro.manufacturer + "^" + pro.category + "^" + pro.amount + "^" + feedbackToString(pro.feedbacks);
        } // bamba^10.3^manu1^food^10^almog#what i think_gal#what he think
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
        /*public ICollection<SLproduct> products { get; }
        public string storeName { get; }
        public string userName { get; }*/
        private static string[] BasketToStringArray(SLbasket basket)
        {
            string ans2 = "";
            foreach (SLproduct pro in basket.products)
            {
                ans2 += ProductToString(pro) + "$";
            }
            if (ans2.Length > 0)
                ans2 = ans2.Substring(0, ans2.Length - 1); // delete the $ in the end

            return new string[] { basket.userName, basket.storeName, ans2 }; // almog castro pro1$pro2$pro3
        }
        private static string BasketToString(SLbasket basket)
        {
            string ans2 = "";
            foreach (SLproduct pro in basket.products)
            {
                ans2 += ProductToString(pro) + "$";
            }
            if (ans2.Length > 0)
                ans2 = ans2.Substring(0, ans2.Length - 1); // delete the $ in the end

            return basket.userName + "&" + basket.storeName + "&" + ans2; // almog castro pro1$pro2$pro3
        }
        private static string[] CartToStringArray(ICollection<SLbasket> cart)
        {
            string[] ans = new string[cart.Count];
            int i = 0;
            foreach (SLbasket basket in cart)
            {
                ans[i] += BasketToString(basket);
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
        /*public string userName;
        public Dictionary<string, ICollection<string>> permissionsPerStore;*/
        private static string[] UsersToStringArray(ICollection<SLemployee> receipts)
        {
            string[] ans = new string[receipts.Count];
            int i = 0;
            foreach (SLemployee em in receipts)
            {
                ans[i] = UserToString(em);
                i++;
            }
            return ans;
        }
        private static string UserToString(SLemployee user)
        {
            return user.userName + "$" + PermissionsToString(user.permissionsPerStore);
        }
        private static string PermissionsToString(Dictionary<string, ICollection<string>> pers)
        {
            string ans = "";
            foreach (string store in pers.Keys)
            {
                ans += store + "^" + ListToString(pers[store]) + "&";
            }
            if (ans.Length > 0)
                ans = ans.Substring(0, ans.Length - 1); // delete the & in the end
            return ans;
        }
        private static string ListToString(ICollection<string> list)
        {
            string ans = "";
            foreach (string str in list)
            {
                ans += str + "#";
            }
            if (ans.Length > 0)
                ans = ans.Substring(0, ans.Length - 1); // delete the # in the end
            return ans;
        }

        private static DecodedMessge act(DecodedMessge msg)
        {

            bool ans = false;
            string ans_d = "";
            string[] ans_a;
            DecodedMessge msg_send = new DecodedMessge();
            if (msg.type == msgType.FUNC)
            {
                switch (msg.name)
                {
                    case ("register"):
                        var ans_r = TradingSystem.ServiceLayer.UserController.register(msg.param_list[0], msg.param_list[1], double.Parse(msg.param_list[2]), msg.param_list[3], msg.param_list[4]);
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "string[]";
                        msg_send.param_list = ans_r;
                        //  byte[] enc_r = TradingSystem.ServiceLayer.Encoder.encode(msg_send);
                        //   ServerConnectionManager.sendMessage(enc_r);
                        break;
                    case ("login"):
                        ans_a = TradingSystem.ServiceLayer.UserController.login(msg.param_list[0], msg.param_list[1]);
                        ans_d = "false";
                        if (ans_a[0].Equals("true"))
                            ans_d = "true";
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "string[]";
                        msg_send.param_list = new string[] { ans_d };
                        //   byte[] enc_l = TradingSystem.ServiceLayer.Encoder.encode(msg_send);
                        // ServerConnectionManager.sendMessage(enc_l);
                        break;
                    case ("get online user"):
                        TradingSystem.ServiceLayer.UserController.getCorrentOnlineUser();
                        break;
                    case ("get online user name"):
                        ans_d = TradingSystem.ServiceLayer.UserController.getCorrentOnlineUserName();
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "string";
                        msg_send.param_list = new string[] { ans_d };
                        //    byte[] enc_name = TradingSystem.ServiceLayer.Encoder.encode(msg_send);
                        //        ServerConnectionManager.sendMessage(enc_name);
                        break;
                    case ("logout"):
                        ans = TradingSystem.ServiceLayer.UserController.logout();
                        ans_d = "false";
                        if (ans)
                            ans_d = "true";
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "bool";
                        msg_send.param_list = new string[] { ans_d };
                        //   byte[] enc_lo = TradingSystem.ServiceLayer.Encoder.encode(msg_send);
                        //    ServerConnectionManager.sendMessage(enc_lo);
                        break;
                    case ("save product"): // Products : "name1&15$name2&30 ..."
                        ans = TradingSystem.ServiceLayer.UserController.saveProduct(msg.param_list[0], msg.param_list[1], msg.param_list[2], StringToDictionary(msg.param_list[3]));
                        if (ans)
                            ans_d = "true";
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "bool";
                        msg_send.param_list = new string[] { ans_d };
                        break;
                    case ("remove product"): // Products : "name1&15$name2&30 ..."
                        ans = TradingSystem.ServiceLayer.UserController.removeProduct(msg.param_list[0], msg.param_list[1], msg.param_list[2], StringToDictionary(msg.param_list[3]));
                        if (ans)
                            ans_d = "true";
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "bool";
                        msg_send.param_list = new string[] { ans_d };
                        break;
                    case ("get basket"): // string username, string storeName
                        var ans_gb = TradingSystem.ServiceLayer.UserController.getBasket(msg.param_list[0], msg.param_list[1]);
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "basket";
                        msg_send.param_list = BasketToStringArray(ans_gb); // {user name, store name, pros}. pros -> pro$pro -> proInfo^feedback -> feedback_feedback -> user#comment
                        break;
                    case ("get cart"): // CartToStringArray
                        var ans_gc = TradingSystem.ServiceLayer.UserController.getCart(msg.param_list[0]);
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "cart";
                        msg_send.param_list = CartToStringArray(ans_gc); // {basket, basket}. basket -> username&storename&pros. pros -> -> pro$pro -> proInfo^feedback -> feedback_feedback -> user#comment
                        break;
                    case ("open store"):
                        ans = TradingSystem.ServiceLayer.UserController.EstablishStore(msg.param_list[0], msg.param_list[1]);
                        ans_d = "false";
                        if (ans)
                            ans_d = "true";
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "bool";
                        msg_send.param_list = new string[] { ans_d };
                        // byte[] enc_os = TradingSystem.ServiceLayer.Encoder.encode(msg_send);
                        // ServerConnectionManager.sendMessage(enc_os);
                        break;
                    case ("purchase"): // string username, string creditNumber, string validity, string cvv
                        var ans_p = TradingSystem.ServiceLayer.UserController.purchase(msg.param_list[0], msg.param_list[1], msg.param_list[2], msg.param_list[3]);
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "receipts";
                        msg_send.param_list = ans_p; // { bool, receipt, receipt }. receipt -> user$store$price$date$id$products. products -> pro1&pro2&pro3 -> proInfo^feedback -> feedback_feedback -> user#comment
                        break;
                    case ("check price"): // string username
                        ans_d = "" + TradingSystem.ServiceLayer.UserController.checkPrice(msg.param_list[0]);
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "double";
                        msg_send.param_list = new string[] { ans_d };
                        break;
                    case ("browse products"): // string username, string productName, string manufacturer
                        var ans_bp = TradingSystem.ServiceLayer.UserController.browseProducts(msg.param_list[0], msg.param_list[1], msg.param_list[2]);
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "dictionary product";
                        List<string> lst = new List<string>();
                        foreach (KeyValuePair<string, SLproduct> item in ans_bp)
                        {
                            lst.AddRange(item.Value.toStringarr(item.Key));
                        }
                        msg_send.param_list = lst.ToArray();// arr[i] -> store$product -> proInfo^feedback -> feedback_feedback -> user#comment
                        break;
                    case ("browse store"):
                        ans_d = TradingSystem.ServiceLayer.UserController.browseStore(msg.param_list[0], msg.param_list[1]);
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "string";
                        msg_send.param_list = new string[] { ans_d };
                        break;
                    case ("search store"):
                        ans_d = "";
                        var temp = StoreController.searchStore(msg.param_list[0]);
                        if (temp != null)
                            ans_d = temp.storeName;
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "string";
                        msg_send.param_list = new string[] { ans_d };
                        break;
                    case ("get receipts in store"):
                        var ans_grs = TradingSystem.ServiceLayer.UserController.getReceiptsHistory(msg.param_list[0], msg.param_list[1]);
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "receipts";
                        msg_send.param_list = ReceiptsToStringArray(ans_grs);
                        break;
                    case ("get all receipts in store"):
                        var ans_gra = TradingSystem.ServiceLayer.UserController.getAllReceiptsHistory(msg.param_list[0], msg.param_list[1]);
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "receipts";
                        msg_send.param_list = ReceiptsToStringArray(ans_gra);
                        break;
                    case ("get all my receipts"):
                        var ans_gamr = TradingSystem.ServiceLayer.UserController.getAllMyReceiptHistory(msg.param_list[0]);
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "receipts";
                        msg_send.param_list = ReceiptsToStringArray(ans_gamr);
                        break;
                    case ("get my employees"):
                        var ans_gme = TradingSystem.ServiceLayer.UserController.getInfoEmployees(msg.param_list[0], msg.param_list[1]);
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "users";
                        msg_send.param_list = UsersToStringArray(ans_gme); // { user, user, user }. user -> name$permissions. permissions -> per1&per2&per3. per -> store$pers. pers -> pername#pername
                        break;
                    case ("add new product to store"): // string username, string storeName, string productName, double price, int amount, string category, string manufacturer
                        ans = TradingSystem.ServiceLayer.UserController.addNewProduct(msg.param_list[0], msg.param_list[1], msg.param_list[2], double.Parse(msg.param_list[3]), int.Parse(msg.param_list[4]), msg.param_list[5], msg.param_list[6]);
                        ans_d = "false";
                        if (ans)
                            ans_d = "true";
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "bool";
                        msg_send.param_list = new string[] { ans_d };
                        break;
                    case ("remove product from store"): // string username, string storeName, string productName, string manufacturer
                        ans = TradingSystem.ServiceLayer.UserController.removeProduct(msg.param_list[0], msg.param_list[1], msg.param_list[2], msg.param_list[3]);
                        ans_d = "false";
                        if (ans)
                            ans_d = "true";
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "bool";
                        msg_send.param_list = new string[] { ans_d };
                        break;
                    case ("edit product from store"): //string username, string storeName, string productName, double price, string manufacturer
                        ans = TradingSystem.ServiceLayer.UserController.editProduct(msg.param_list[0], msg.param_list[1], msg.param_list[2], double.Parse(msg.param_list[3]), msg.param_list[4]);
                        ans_d = "false";
                        if (ans)
                            ans_d = "true";
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "bool";
                        msg_send.param_list = new string[] { ans_d };
                        break;
                    case ("supply product"): //string username, string storeName, string productName, int amount, string manufacturer
                        ans = TradingSystem.ServiceLayer.UserController.supplyProduct(msg.param_list[0], msg.param_list[1], msg.param_list[2], int.Parse(msg.param_list[3]), msg.param_list[4]);
                        ans_d = "false";
                        if (ans)
                            ans_d = "true";
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "bool";
                        msg_send.param_list = new string[] { ans_d };
                        break;
                    case ("hire new manager"): //string username, string storeName, string userToHire
                        ans = TradingSystem.ServiceLayer.UserController.hireNewStoreManager(msg.param_list[0], msg.param_list[1], msg.param_list[2]);
                        ans_d = "false";
                        if (ans)
                            ans_d = "true";
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "bool";
                        msg_send.param_list = new string[] { ans_d };
                        break;
                    case ("edit manager"): //string username, string storeName, string userToHire, List<string> Permissions -> "pre1$pre2$pre3 ..."
                        ans = TradingSystem.ServiceLayer.UserController.editManagerPermissions(msg.param_list[0], msg.param_list[1], msg.param_list[2], StringToList(msg.param_list[3]));
                        ans_d = "false";
                        if (ans)
                            ans_d = "true";
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "bool";
                        msg_send.param_list = new string[] { ans_d };
                        break;
                    case ("hire new owner"): //string username, string storeName, string userToHire, List<string> Permissions -> "pre1$pre2$pre3 ..."
                        ans = TradingSystem.ServiceLayer.UserController.hireNewStoreOwner(msg.param_list[0], msg.param_list[1], msg.param_list[2], StringToList(msg.param_list[3]));
                        ans_d = "false";
                        if (ans)
                            ans_d = "true";
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "bool";
                        msg_send.param_list = new string[] { ans_d };
                        break;
                    case ("remove manager"): //string username, string storeName, string userToHire
                        ans = TradingSystem.ServiceLayer.UserController.removeManager(msg.param_list[0], msg.param_list[1], msg.param_list[2]);
                        ans_d = "false";
                        if (ans)
                            ans_d = "true";
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "bool";
                        msg_send.param_list = new string[] { ans_d };
                        break;
                    case ("leave feedback"): //string username, string storeName, string productName, string manufacturer, string comment
                        ans = TradingSystem.ServiceLayer.UserController.leaveFeedback(msg.param_list[0], msg.param_list[1], msg.param_list[2], msg.param_list[3], msg.param_list[4]);
                        ans_d = "false";
                        if (ans)
                            ans_d = "true";
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "bool";
                        msg_send.param_list = new string[] { ans_d };
                        break;
                    case ("get all feedbacks"): //string storeName, string productName, string manufacturer
                        var ans_afb = TradingSystem.ServiceLayer.UserController.getAllFeedbacks(msg.param_list[0], msg.param_list[1], msg.param_list[2]);
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "feedbacks";
                        msg_send.param_list = new string[] { feedbackToString(ans_afb) }; // feedback_feedback -> user#comment.    almog#what i think_gal#what he think
                        break; // feedbackToString
                    case ("username"):
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "string";
                        msg_send.param_list = new string[] { UserController.getUserName() };
                        break;
                    case ("get my stores"): //string username
                        var ans_gms = TradingSystem.ServiceLayer.UserController.GetMyStores(msg.param_list[0]);
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "string[]";
                        msg_send.param_list = ans_gms;
                        break;
                    case ("get my permission on store"): //string username, string storeName
                        var ans_gmp = TradingSystem.ServiceLayer.UserController.GetPermissions(msg.param_list[0], msg.param_list[1]);
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "string[]";
                        msg_send.param_list = ans_gmp;
                        break;
                }
            }

            return msg_send;
        }

    }

}
