using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
            Socket socket = (Socket)input[0];
            NetworkStream stream = new NetworkStream(socket);
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
            catch(Exception e)
            {
                socket.Close();
                throw e;
            }
        }

        private static void sendHandler(object parameters)
        {
            object[] input = (object[])parameters;
            Socket socket = (Socket)input[0];
            NetworkStream stream = new NetworkStream(socket);
            Queue<DecodedMessge> qwewe = (Queue<DecodedMessge>)input[1];
            AutoResetEvent waitEvent = (AutoResetEvent)input[2];
            while (true)
            {
                while (qwewe.Count == 0)
                    waitEvent.WaitOne();
                ServerConnectionManager.sendMessage(Encoder.encode(qwewe.Dequeue()), stream);
            }

        }


        public static void threadsMain(object socketPar)
        {

            UserController.threadInit();
            Socket socket = (Socket)socketPar;
            socket.Blocking = true;

            AutoResetEvent waitEvent = new AutoResetEvent(false);
            Queue<DecodedMessge> qwewe = new Queue<DecodedMessge>();
            AutoResetEvent alarmLock = new AutoResetEvent(false);
            //Thread alarmHandler = new Thread(new ParameterizedThreadStart(AlarmHandler));
            Thread sendhandler = new Thread(new ParameterizedThreadStart(sendHandler));

            object[] parameters = new object[] { socket, qwewe, waitEvent };
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
                        ans = TradingSystem.ServiceLayer.UserController.register(msg.param_list[0], msg.param_list[1]);
                        ans_d = "false";
                        if (ans)
                            ans_d = "true";
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "bool";
                        msg_send.param_list = new string[] { ans_d };

                        //  byte[] enc_r = TradingSystem.ServiceLayer.Encoder.encode(msg_send);
                        //   ServerConnectionManager.sendMessage(enc_r);
                        break;
                    case ("login"):
                        ans = TradingSystem.ServiceLayer.UserController.login(msg.param_list[0], msg.param_list[1]);
                        ans_d = "false";
                        if (ans)
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
                        TradingSystem.ServiceLayer.UserController.browseStore(msg.param_list[0], msg.param_list[1]);
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
