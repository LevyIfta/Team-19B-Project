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
namespace TradingSystem.ServiceLayer
{
    static class ServerConnectionManager
    {

        public static readonly int EOT = 4;

        private static int hostPort;
        private static int serverPort = 8888; //default
        public static string ipAdress = "192.168.56.1";
        private static NetworkStream stream;
        private static TcpClient client;
        private static Dictionary<Thread, Socket> threads;
        
        private static void act(DecodedMessge msg)
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
                        byte[] enc_r = TradingSystem.ServiceLayer.Encoder.encode(msg_send);
                        ServerConnectionManager.sendMessage(enc_r);
                        break;
                    case ("login"):
                        ans = TradingSystem.ServiceLayer.UserController.login(msg.param_list[0], msg.param_list[1]);
                        ans_d = "false";
                        if (ans)
                            ans_d = "true";
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "bool";
                        msg_send.param_list = new string[] { ans_d };
                        byte[] enc_l = TradingSystem.ServiceLayer.Encoder.encode(msg_send);
                        ServerConnectionManager.sendMessage(enc_l);
                        break;
                    case ("get online user"):
                        TradingSystem.ServiceLayer.UserController.getCorrentOnlineUser();
                        break;
                    case ("get online user name"):
                        ans1 = TradingSystem.ServiceLayer.UserController.getCorrentOnlineUserName();
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "string";
                        msg_send.param_list = new string[] { ans1 };
                        byte[] enc_name = TradingSystem.ServiceLayer.Encoder.encode(msg_send);
                        ServerConnectionManager.sendMessage(enc_name);
                        break;
                    case ("logout"):
                        ans = TradingSystem.ServiceLayer.UserController.logout();
                        ans_d = "false";
                        if (ans)
                            ans_d = "true";
                        msg_send.type = msgType.OBJ;
                        msg_send.name = "bool";
                        msg_send.param_list = new string[] { ans_d };
                        byte[] enc_lo = TradingSystem.ServiceLayer.Encoder.encode(msg_send);
                        ServerConnectionManager.sendMessage(enc_lo);
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
                        byte[] enc_os = TradingSystem.ServiceLayer.Encoder.encode(msg_send);
                        ServerConnectionManager.sendMessage(enc_os);
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
                }
            }
        }

        private static void sendMessage(byte[] enc)
        {
            throw new NotImplementedException();
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

        private static void threadsMain(object socketPar)
        {
            Socket socket = (Socket)socketPar;
            NetworkStream stream = new NetworkStream(socket);
            
            List<byte> data = new List<byte>();
            try
            {
                while (true)
                {
                    char c = (char)stream.ReadByte();
                    if (c == EOT)
                    {
                        act(Decoder.decode(data.ToArray()));
                        data = new List<byte>();
                    }
                    else
                        data.Add(Convert.ToByte(c));

                }
            }
            catch
            {
                socket.Close();
            }
        }

        private static Socket listner;
        public static void init()
        {
            

            listner = new Socket(IPAddress.Parse(ipAdress).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ipAdress), 8888);
            
            try
            {
                listner.Bind(endPoint);
                listner.Listen(10);
                while (true)
                {
                    
                    Socket Handler = listner.Accept();
                    Thread th = new Thread(new ParameterizedThreadStart(threadsMain));
                    th.Start(Handler);

                }
            }
            catch(Exception e)
            {
                throw e;
            }
          
        }

        public static void disconnect()
        {
            listner.Close();
            
        }



        

    }
}
