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
        public static bool AlarmHandler(Object parameters)
        {
            object[] input = (object[])parameters;
           // Socket socket = (Socket)input[0]; //unused
            //NetworkStream stream = new NetworkStream(socket); //unused
            Queue<DecodedMessge> qwewe = (Queue<DecodedMessge>)input[1];
            AutoResetEvent waitEvent = (AutoResetEvent)input[2];
            AutoResetEvent alarmLock = (AutoResetEvent)input[3];
            Func<bool> isAlarmsEmpty = (Func<bool>)input[4];
            Func<Tuple<string, string>> fetchAlarm = (Func<Tuple<string, string>>)input[5];


            while (true)
            {
                while (isAlarmsEmpty())
                    alarmLock.WaitOne();

                Tuple<string, string> content = fetchAlarm();
                DecodedMessge msg = new DecodedMessge();
                msg.type = msgType.ALARM;

                msg.name = content.Item1;
                msg.param_list = new string[] { content.Item2 };
                lock (qwewe)
                {
                    qwewe.Enqueue(msg);
                    waitEvent.Set();
                }
            }

        }

        private static void messagesHandler(object parameters)
        {
            object[] input = (object[])parameters;
        
            SslStream stream = (SslStream)input[0];
            Queue<DecodedMessge> qwewe = (Queue<DecodedMessge>)input[1];
            AutoResetEvent waitEvent = (AutoResetEvent)input[2];

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
            UserController.estblishAlarmHandler(qwewe, waitEvent, alarmLock);
            sendhandler.Start(parameters);
            messagesHandler(parameters);
        }


        private static List<string> StringToList(string str)
        {
            List<string> list = new List<string>();
            string[] Permissions = str.Split(' ');
            foreach (string pre in Permissions)
            {
                list.Add(pre);
            }
            return list;

        }


        private static Dictionary<string, int> StringToDictionary(string str)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            string[] products = str.Split(' ');
            foreach (string product in products)
            {
                string[] ans = product.Split(':');
                dic[ans[0]] = int.Parse(ans[1]);
            }
            return dic;
        }

        private static DecodedMessge act(DecodedMessge msg)
        {

            bool ans = false;
            string ans1 = "";
            string ans_d = "";
            DecodedMessge msg_send = new DecodedMessge();
            if (msg.type == msgType.FUNC)
            {
                switch (msg.name)
                {
                    case ("register"):
                        string[] ans_r = TradingSystem.ServiceLayer.UserController.register(msg.param_list[0], msg.param_list[1]);
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "string[]";
                        msg_send.param_list = ans_r;

                        //  byte[] enc_r = TradingSystem.ServiceLayer.Encoder.encode(msg_send);
                        //   ServerConnectionManager.sendMessage(enc_r);
                        break;
                    case ("login"):
                        string[] ans2 = TradingSystem.ServiceLayer.UserController.login(msg.param_list[0], msg.param_list[1]);
                        ans_d = "false";
                        if (ans2[0].Equals("true"))
                            ans_d = "true";
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "bool";
                        msg_send.param_list = new string[] { ans_d };
                        //   byte[] enc_l = TradingSystem.ServiceLayer.Encoder.encode(msg_send);
                        // ServerConnectionManager.sendMessage(enc_l);
                        break;
                    case ("get online user"):
                        TradingSystem.ServiceLayer.UserController.getCorrentOnlineUser();
                        break;
                    case ("get online user name"):
                        ans1 = TradingSystem.ServiceLayer.UserController.getCorrentOnlineUserName();
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "string";
                        msg_send.param_list = new string[] { ans1 };
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
                    case ("save product"): // Products : "name1:15 name2:30 ..."
                        TradingSystem.ServiceLayer.UserController.saveProduct(msg.param_list[0], msg.param_list[1], msg.param_list[2], StringToDictionary(msg.param_list[3]));
                        break;
                    case ("remove product"): // Products : "name1:15 name2:30 ..."
                        TradingSystem.ServiceLayer.UserController.removeProduct(msg.param_list[0], msg.param_list[1], msg.param_list[2], StringToDictionary(msg.param_list[3]));
                        break;
                    case ("get basket"): // string username, string storeName
                        TradingSystem.ServiceLayer.UserController.getBasket(msg.param_list[0], msg.param_list[1]);
                        break;
                    case ("get cart"):
                        TradingSystem.ServiceLayer.UserController.getCart(msg.param_list[0]);
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
                    case ("purchase"): // string username, string paymentName
                        TradingSystem.ServiceLayer.UserController.checkPrice(msg.param_list[0]);
                        TradingSystem.ServiceLayer.UserController.purchase(msg.param_list[0], msg.param_list[1]);
                        break;
                    case ("browse products"): // string username, string productName, string manufacturer
                        TradingSystem.ServiceLayer.UserController.browseProducts(msg.param_list[0], msg.param_list[1], msg.param_list[2]);
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
                        TradingSystem.ServiceLayer.UserController.getReceiptsHistory(msg.param_list[0], msg.param_list[1]);
                        break;
                    case ("get all receipts in store"):
                        TradingSystem.ServiceLayer.UserController.getAllReceiptsHistory(msg.param_list[0], msg.param_list[1]);
                        break;
                    case ("get all my receipts"):
                        TradingSystem.ServiceLayer.UserController.getAllMyReceiptHistory(msg.param_list[0]);
                        break;
                    case ("get my employees"):
                        TradingSystem.ServiceLayer.UserController.getInfoEmployees(msg.param_list[0], msg.param_list[1]);
                        break;
                    case ("add new product to store"): // string username, string storeName, string productName, double price, int amount, string category, string manufacturer
                        TradingSystem.ServiceLayer.UserController.addNewProduct(msg.param_list[0], msg.param_list[1], msg.param_list[2], double.Parse(msg.param_list[3]), int.Parse(msg.param_list[4]), msg.param_list[5], msg.param_list[6]);
                        break;
                    case ("remove product from store"): // string username, string storeName, string productName, string manufacturer
                        TradingSystem.ServiceLayer.UserController.removeProduct(msg.param_list[0], msg.param_list[1], msg.param_list[2], msg.param_list[3]);
                        break;
                    case ("edit product from store"): //string username, string storeName, string productName, double price, string manufacturer
                        TradingSystem.ServiceLayer.UserController.editProduct(msg.param_list[0], msg.param_list[1], msg.param_list[2], double.Parse(msg.param_list[3]), msg.param_list[4]);
                        break;
                    case ("hire new manager"): //string username, string storeName, string userToHire
                        TradingSystem.ServiceLayer.UserController.hireNewStoreManager(msg.param_list[0], msg.param_list[1], msg.param_list[2]);
                        break;
                    case ("edit manager"): //string username, string storeName, string userToHire, List<string> Permissions -> "pre1 pre2 pre3 ..."
                        TradingSystem.ServiceLayer.UserController.editManagerPermissions(msg.param_list[0], msg.param_list[1], msg.param_list[2], StringToList(msg.param_list[3]));
                        break;
                    case ("hire new owner"): //string username, string storeName, string userToHire, List<string> Permissions -> "pre1 pre2 pre3 ..."
                        TradingSystem.ServiceLayer.UserController.hireNewStoreOwner(msg.param_list[0], msg.param_list[1], msg.param_list[2], StringToList(msg.param_list[3]));
                        break;
                    case ("remove manager"): //string username, string storeName, string userToHire
                        TradingSystem.ServiceLayer.UserController.removeManager(msg.param_list[0], msg.param_list[1], msg.param_list[2]);
                        break;
                    case ("leave feedback"): //string username, string storeName, string productName, string manufacturer, string comment
                        TradingSystem.ServiceLayer.UserController.leaveFeedback(msg.param_list[0], msg.param_list[1], msg.param_list[2], msg.param_list[3], msg.param_list[4]);
                        break;
                    case ("get all feedbacks"): //string storeName, string productName, string manufacturer
                        TradingSystem.ServiceLayer.UserController.getAllFeedbacks(msg.param_list[0], msg.param_list[1], msg.param_list[2]);
                        break;
                    case ("username"):
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "string";
                        msg_send.param_list = new string[] { UserController.getUserName() };
                        break;

                }
            }

            return msg_send;
        }

    }

}
