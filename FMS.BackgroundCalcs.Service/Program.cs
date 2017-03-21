using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Topshelf;


namespace FMS.BackgroundCalcs.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<BackgroundCalcs>(s =>
                {
                    s.ConstructUsing(name => new BackgroundCalcs());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                x.RunAsLocalSystem();

                x.SetDescription("FMS background calculations");
                x.SetDisplayName("FMS background calculations");
                x.SetServiceName("FMS.Background.Calculations");
            });
        }
    }



    public class BackgroundCalcs
    {
        System.Threading.Thread t;
        
        public BackgroundCalcs()
        {
        }

        public void Start()
        {
            // StartListener();
            t = new System.Threading.Thread(StartCalcs);
            t.Start();
        }

        public void Stop()
        {
            t.Abort();
        }


        private static void StartCalcs()
        {
            //FMS.BackgroundCalcs.ConsoleApp
            FMS.BackgroundCalcs.ConsoleApp.MainMethod.ExecuteInfinateLoop();

        }

    }

}
