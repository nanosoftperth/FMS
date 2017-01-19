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
        readonly Timer _timer;

        System.Threading.Thread t;

        public CalAMP_Receiver()
        {
            _timer = new Timer(5000) { AutoReset = true };
            //_timer.Elapsed += (sender, eventArgs) => Console.WriteLine("It is {0} and all is well", DateTime.Now);
        }
        public void Start()
        {
            _timer.Start();
            StartListener();
            t = new System.Threading.Thread(StartListener);
            t.Start();

        }
        public void Stop()
        {
            _timer.Stop();
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
                    byte[] bytes = listener.Receive(ref groupEP);
                    string hex = ByteArrayToString(bytes);

                    Console.WriteLine("Received broadcast from {0} :\n {1}\n", groupEP.ToString(), hex);

                    //create object from the received binary
                    CalAMP_Telegram recevied_telegram = new CalAMP_Telegram(bytes);


                    //store the telegram in a flie
                    string logFilePath = string.Format(@"{0}\{1}.log",
                            Properties.Settings.Default.LogDirectory, DateTime.Now.ToString("yyyyMMddHHmmss"));

                    string xml = recevied_telegram.GetXML();
                    System.IO.File.AppendAllText(logFilePath, hex + Environment.NewLine + xml);
                    byte[] responseBytes =  System.Text.Encoding.ASCII.GetBytes(xml);
                    
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

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
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
