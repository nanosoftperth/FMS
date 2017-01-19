using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Datalistener.CalampTest
{
    class Program
    {

        private static readonly int listenPort = 11000;

        static void Main(string[] args)
        {
            //Send("TEST STRING");

            // UdpClient listener = new UdpClient(listenPort);
            ////IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

            ////listener.Client.ReceiveTimeout = 1000;

            while (true)
            {

                string msgToSend = Console.ReadLine();

                Send(msgToSend, "45.76.114.191", 11000);

            }


        }
        static void Send(string Message, string ipaddr, int port)
        {
            try
            {
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPAddress broadcast = IPAddress.Parse(ipaddr);
                IPEndPoint ep = new IPEndPoint(broadcast, port);
                byte[] sendbuf = Encoding.ASCII.GetBytes(Message);

                //from doco
                string hexString = "83 02 F9 99 01 01 01 02 00 0A 3F B5 4B 33 3F B5 4B 33 13 B2 95 3C BA 18 EB A3 00 00 00 00 00 00 00 02 00 D1 04 00 00 04 FF BF 0F 23 07 00 01 01 00 00".Replace(" ", string.Empty);
                //string hexString = "83 05 16 63 02 99 83 01 01 01 02 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 6C 00 00 FF 8F 00 00 6E 08 19 19 00 0083 05 16 63 02 99 83 01 01 01 02 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 6C 00 00 FF 8F 00 00 6E 08 19 19 00 0083 05 16 63 02 99 83 01 01 01 02 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 6C 00 00 FF 8F 00 00 6E 08 19 19 00 00 83 05 16 63 02 99 83 01 01 01 02 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 6C 00 00 FF 8F 00 00 6E 08 19 19 00 00".Replace(" ", string.Empty);
                
                byte[] defaultSend = StringToByteArray(hexString);


                s.SendTo(string.IsNullOrEmpty(Message) ? defaultSend : sendbuf, ep);

                byte[] bytes = new byte[10240];

                int bytesRec = s.Receive(bytes);
                string str = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                string hex = BitConverter.ToString(bytes, 0, bytesRec).Replace("-", " ");


                Console.WriteLine("received: {0}", str);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

    }
}
