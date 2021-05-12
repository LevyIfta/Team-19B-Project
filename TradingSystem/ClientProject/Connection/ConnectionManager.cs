using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientProject.Connection
{
    static class ConnectionManager
    {

        public static readonly int EOT = 4;

        private static string hostAdress = "192.168.1.13"; // 56.1
        private static int serverPort = 8888; //default
        public static string serveradress= "192.168.56.1";
        public static IPEndPoint serverIP;
        private static NetworkStream stream;
        private static Socket socket;


        static ConnectionManager()
            {
                Connect();
            }

        /// <summary>
        /// should be called staticly
        /// </summary>
        public static void Connect()
        {
            try
            {
         
                serverIP = new IPEndPoint(IPAddress.Parse(serveradress), serverPort);
                socket = new Socket(IPAddress.Parse(hostAdress).AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                socket.Connect(serverIP);
                socket.Blocking = true;
                stream = new NetworkStream(socket);
                

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
                    data.Add(Convert.ToByte(c) );
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

        public static bool peek()
        {
            return stream.DataAvailable;
        }
    

        public static void disconnect()
        {
            stream.Close();
            socket.Close();
        }

    }
}
