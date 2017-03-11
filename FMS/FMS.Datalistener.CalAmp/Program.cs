using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Topshelf;
using FMS.Datalistener.CalAmp.DataObjects;
using System.Configuration;


namespace FMS.Datalistener.CalAmp
{
    class Program
    {
        static void Main(string[] args)
        {

            HostFactory.Run(x =>
            {
                x.Service<CalAMP_Receiver>(s =>
                {
                    s.ConstructUsing(name => new CalAMP_Receiver());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("FMS data receiver for CalAMP products");
                x.SetDisplayName("FMS Calamp Data Receiver");
                x.SetServiceName("FMS.Calamp.Receiver");
            });
        }

    }


    public class CalAMP_Receiver
    {

        System.Threading.Thread t;

        public CalAMP_Receiver()
        {
        }
        public void Start()
        {
            StartListener();
            t = new System.Threading.Thread(StartListener);
            t.Start();

        }
        public void Stop()
        {
            t.Abort();
        }

        private const int listenPort = 11000;

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        private static void StartListener()
        {
            bool done = false;

            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

            try
            {
                Console.WriteLine("Waiting for broadcast");

                while (!done)
                {


                    string logDir = Properties.Settings.Default.LogDirectory;

                    //listener (and gets data from LMU devices)
                    byte[] bytes = listener.Receive(ref groupEP);

                    //write to console if dta found
                    string hex = BitHelper.ByteArrayToString(bytes);
                    Console.WriteLine("Received broadcast from {0} :\n {1}\n", groupEP.ToString(), hex);

                    //create object from the received binary
                    CalAMP_Telegram recevied_telegram = new CalAMP_Telegram(bytes);


                    //store the telegram in a flie, create file
                    string logFilePath = string.Format(@"{0}\{1}.log",
                            Properties.Settings.Default.LogDirectory, DateTime.Now.ToString("yyyyMMddHHmmss"));

                    //get XML and received HEX values, save them to the new log file
                    string xml = recevied_telegram.GetXML();
                    System.IO.File.AppendAllText(logFilePath, hex + Environment.NewLine + xml);
                    //byte[] responseBytes =  System.Text.Encoding.ASCII.GetBytes(xml);

                    //create a response message 

                    recevied_telegram.MessageHeader.MessageType = MessageTypeEnum.ACK_NAK_Message;

                    recevied_telegram.MessageHeader.ServiceType = ServiceTypeEnum.ResponseToAcklowlegedRequest;

                    //get the binary representing the response message 
                    byte[] responseBytes = recevied_telegram.GetBytes();

                    string hexResponse = BitConverter.ToString(responseBytes, 0).Replace("-", " ");


                    System.IO.File.AppendAllText(logFilePath, "hex response" + hexResponse);

                    //parse the message here, then send a response
                    Send(responseBytes, groupEP.Address.ToString(), groupEP.Port);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                listener.Close();
            }
        }



        static void Send(byte[] Message, string ipaddr, int port)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress broadcast = IPAddress.Parse(ipaddr);
            //byte[] sendbuf = Encoding.ASCII.GetBytes(Message);
            IPEndPoint ep = new IPEndPoint(broadcast, port);
            s.SendTo(Message, ep);
        }


    }

}
