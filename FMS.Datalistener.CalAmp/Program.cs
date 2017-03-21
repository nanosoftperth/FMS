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
using System.Diagnostics;
using Microsoft.Owin.Hosting;
using Owin;


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

        private System.Threading.Thread t_ProcessLogs;

        private IDisposable _webApplication;

        public CalAMP_Receiver()
        {
        }
        public void Start()
        {
           // StartListener();
            t = new System.Threading.Thread(StartListener);
            t.Start();

            t_ProcessLogs = new System.Threading.Thread(DataObjects.LogFileProcessor.ProcessLogs);
            t_ProcessLogs.Start();

            string urlLocation = string.Format(@"http://*:{0}", Properties.Settings.Default.web_url_port);

            _webApplication = WebApp.Start<API.OwinConfiguration>(urlLocation);

        }
        public void Stop()
        {
            t.Abort();
            t_ProcessLogs.Abort();
            _webApplication.Dispose();
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

            API.DataAccessController dac = new API.DataAccessController();

            try
            {
                Console.WriteLine("Waiting for broadcast");

                while (!done)
                {


                    string logDir = Properties.Settings.Default.LogDirectory;

                    //listener (and gets data from LMU devices)
                    byte[] bytes = listener.Receive(ref groupEP);

                    //write to console if data found
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

                    listener.Send(responseBytes, responseBytes.Count(), groupEP);

                    recevied_telegram.MessageBody.Updatetime += TimeSpan.FromHours(8);//make perth time

                    if (recevied_telegram.MessageBody.Lattitude > 0 )
                            dac.Get(recevied_telegram.OptionsHeader.MobileID, 
                                recevied_telegram.MessageBody.Lattitude, 
                                recevied_telegram.MessageBody.Longtiude, 
                                recevied_telegram.MessageBody.Updatetime.ToString("dd/MMM/yyyy HH:mm:ss"));                                  


                    System.IO.File.AppendAllText(logFilePath, "hex response" + hexResponse);

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




    }

}
