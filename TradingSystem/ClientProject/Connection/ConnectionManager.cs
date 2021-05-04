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

        private static int hostPort;
        private static int serverPort = 8888; //default
        public static long serveradress;
        public static IPEndPoint serverIP;
        private static NetworkStream stream;
        private static TcpClient client;


        static ConnectionManager()
            {
            Connect();
            }

        /// <summary>
        /// should be called staticly
        /// </summary>
        public static void Connect( )
        {
            try
            {
         
                serverIP = new IPEndPoint(serveradress, serverPort);
                client = new TcpClient(serverIP);


                stream = client.GetStream();


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

        public static byte[] readMessage()
        {
            List<byte> data = new List<byte>();
            try
            {
                while (stream.DataAvailable)
                {
                    int current = stream.ReadByte();
                    if (current == EOT)
                        break;
                    data.AddRange(BitConverter.GetBytes(current) );
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
            client.Close();
        }

    }
}
