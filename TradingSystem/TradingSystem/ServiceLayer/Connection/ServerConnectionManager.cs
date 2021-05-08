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
            
            System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(new Action (()=> {
                ((MainWindow)Application.Current.MainWindow).dothedo(msg);
            }));
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
