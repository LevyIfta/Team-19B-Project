using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ClientWeb.Connection
{
    
    static class ConnectionManager
    {

        public static readonly int EOT = 4;

        private static string hostAdress = "192.168.56.1"; // 56.1
        private static int serverPort = 8888; //default
        private static int hostPort = 1234;
        public static string serveradress= "192.168.56.1";
        public static IPEndPoint serverIP;
        private static SslStream stream;
        private static Socket socket;
        private static string serverName = Environment.MachineName;


        static ConnectionManager()
            {
                Connect();
            }

        /// <summary>
        /// should be called staticly
        /// </summary>
        public static void Connect()
        {
        /*    try
            {*/
         
                serverIP = new IPEndPoint(IPAddress.Parse(serveradress), serverPort);
                socket = new Socket(IPAddress.Parse(hostAdress).AddressFamily, SocketType.Stream, ProtocolType.Tcp);

           
                socket.Connect(serverIP);
                socket.Blocking = true;

                stream = new SslStream(new NetworkStream(socket), false, new RemoteCertificateValidationCallback((object sender,
              X509Certificate certificate,
              X509Chain chain,
              SslPolicyErrors sslPolicyErrors) => true ), null);
                stream.AuthenticateAsClient(serverName);
                
                

         /*   }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
                
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }*/
        }


        public static void sendMessage(byte[] message)
        {
            try
            {
                stream.Write(message, 0, message.Length);
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

        public static void sendMessage(string message)
        {
            sendMessage(Encoding.ASCII.GetBytes(message));
        }

        public static byte[] readMessageCon()
        {
            List<byte> data = new List<byte>();
            try
            {
                while (true /*stream.DataAvailable*/)
                {
                 
                    char c = (char)stream.ReadByte();
                    if (c == EOT)
                        break;
                    data.Add(Convert.ToByte(c));
                    
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            return data.ToArray();
        }

    

        public static void disconnect()
        {
            stream.Close();
            socket.Close();
        }

    }
}
