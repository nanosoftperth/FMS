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

    public class TownCrier : ServiceControl
    {
        readonly Timer _timer;

        private readonly LogWriter logWriter;

        public TownCrier()
        {
            logWriter = HostLogger.Get<TownCrier>();

            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => writeMessages(String.Format("It is {0} and all is well", DateTime.Now));
            
            //Classes.MyLogManager.Instance.GetCurrentClassLogger().Debug(Console.WriteLine("It is {0} and all is well", DateTime.Now));
        }

        private void writeMessages(string msg)
        {
            Console.WriteLine("It is {0} and all is well", DateTime.Now);
            logWriter.Debug("Tick:" + DateTime.Now.ToLongTimeString());

        }

        public bool Start(HostControl hostControl)
        {
            _timer.Start();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _timer.Stop();
            return false;
        }
    }


    class Program
    {


        static void Main(string[] args)
        {


            HostFactory.Run(x =>
            {
                x.Service<TownCrier>();

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
