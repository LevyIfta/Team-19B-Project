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
    static class ServerConnectionManager
    {

        public static readonly int EOT = 4;

        private static int hostPort;
        private static int serverPort = 8888; //default
        public static string ipAdress = "192.168.56.1";
        
        private static TcpClient client;
        private static Dictionary<Thread, Socket> threads = new Dictionary<Thread, Socket>();
        
       

        public static void sendMessage(byte[] enc, NetworkStream stream)
        {
            
            try
            {
                stream.Write(enc, 0, enc.Length);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }


    

        private static Socket listner;
        public static void init()
        {

            UserController.init(new Func<object, bool>(ServerMessageManager.AlarmHandler));
            listner = new Socket(IPAddress.Parse(ipAdress).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ipAdress), 8888);
            
            try
            {
                listner.Bind(endPoint);
                listner.Listen(10);
                while (true)
                {
                    
                    Socket Handler = listner.Accept();
                    Thread th = new Thread(new ParameterizedThreadStart(ServerMessageManager.threadsMain));
                    threads.Add(th, Handler);
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
