using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Topshelf;

namespace FMS.SimulatorService
{

    public class TownCrier
    {
        readonly Timer _timer;
        public TownCrier()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => Console.WriteLine("It is {0} and all is well", DateTime.Now);

            Classes.MyLogManager.Instance.GetCurrentClassLogger().Debug(Console.WriteLine("It is {0} and all is well", DateTime.Now));
        }
        public void Start() { _timer.Start(); }
        public void Stop() { _timer.Stop(); }
    }


    class Program
    {



        static void Main(string[] args)
        {          


            HostFactory.Run(x =>
            {
                x.Service<TownCrier>(s =>
                {
                    s.ConstructUsing(name => new TownCrier());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });


                x.UseNLog(FMS.SimulatorService.Classes.MyLogManager.Instance);

                x.RunAsLocalSystem();

                x.SetDescription("Sample Topshelf Host");
                x.SetDisplayName("Stuff");
                x.SetServiceName("Stuff");
            });

        }
    }
}
