using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Topshelf;
using Topshelf.Logging;

namespace FMS.SimulatorService
{

    public class ServiceObject : ServiceControl
    {

        private System.Threading.Thread _t;

        private readonly LogWriter logWriter;

        public ServiceObject()
        {
            logWriter = HostLogger.Get<ServiceObject>();

            logWriter.Debug("starting application:" + DateTime.Now.ToLongTimeString());

            _t = new System.Threading.Thread(new System.Threading.ThreadStart(FMS.SimulatorService.Classes.Worker.DoWork));

            //Classes.MyLogManager.Instance.GetCurrentClassLogger().Debug(Console.WriteLine("It is {0} and all is well", DateTime.Now));
        }


        public bool Start(HostControl hostControl)
        {
            _t.Start();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _t.Abort();
            return false;
        }
    }


    class Program
    {


        static void Main(string[] args)
        {


            HostFactory.Run(x =>
            {
                x.Service<ServiceObject>();

                //x.UseNLog(FMS.SimulatorService.Classes.MyLogManager.Instance);
                x.UseNLog(Classes.MyLogManager.Instance);

                x.SetDescription("Sample Topshelf Host");
                x.SetDisplayName("Stuff");
                x.SetServiceName("Stuff");

                x.RunAsLocalSystem();
            });

        }
    }
}
