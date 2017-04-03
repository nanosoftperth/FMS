using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using System.Timers;
using NLog;
using NLog.Fluent;


namespace FMS.ReportService
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static void Main()
        {
            logger.Info("Application Start main constructor"); 
            try
            {
                HostFactory.Run(x =>
                {
                    x.Service<IService>(s =>
                    {
                        s.ConstructUsing(name => new EmailService());
                        s.WhenStarted(tc => tc.Start());
                        s.WhenStopped(tc => tc.Stop());
                    });
                    x.RunAsLocalSystem();

                    x.SetDescription("FMSReportService for Email Scheduler");
                    x.SetDisplayName("FMSReportService");
                    x.SetServiceName("FMSReportService");
                });
            }
            catch (Exception ex) 
            {
                logger.Error("Error with query {0} {1} {2}", "Main Constructor exception", ex.Message, ex.StackTrace);
            }
        }
    }
}
