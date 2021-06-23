using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
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
        
        private static TcpListener listner;
        private static Dictionary<Thread, TcpClient> threads = new Dictionary<Thread, TcpClient>();
        
       

        public static void sendMessage(byte[] enc, SslStream stream)
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


    

 
        public static void init()
        {

            UserController.init(new Func<object, bool>(ServerMessageManager.AlarmHandler));
            
           
            listner = new TcpListener(IPAddress.Parse(ipAdress), serverPort);

            try
            {
                listner.Start();
                while (true)
                {
                    
                    TcpClient client = listner.AcceptTcpClient();
                    
                    Thread th = new Thread(new ParameterizedThreadStart(ServerMessageManager.threadsMain));
                    threads.Add(th, client);
                    th.Start(client);

                }
            }
            catch(Exception e)
            {
                throw e;
            }
          
        }



        public static void disconnect()
        {
            if(listner != null)
                listner.Stop();
            
        }



        

    }
}
