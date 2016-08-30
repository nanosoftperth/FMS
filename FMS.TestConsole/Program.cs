using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace FMS.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {



            sr1.Uniqco_IntegratorClient svr1 = new sr1.Uniqco_IntegratorClient();


            ServiceAccess.WebServices.VINNumberRequest vnr = new ServiceAccess.WebServices.VINNumberRequest();

            //This is required ro ignore the fact that the certificate is "self signed"
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;


            DateTime startdate = DateTime.Parse("01 aug 2015");

            vnr.StartDate = startdate;
            vnr.EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);


            vnr.VINNumber = "WVWZZZ6RZGU025834";

           // int i = 0;

            string username = "webServiceAccount";
            string pwd = "asid7@#*du@";
           

            Console.WriteLine("\nSending request to server:\n");


            List<ServiceAccess.WebServices.VINNumberRequest> lst = new List<ServiceAccess.WebServices.VINNumberRequest>();

            lst.Add(vnr);


            ServiceAccess.WebServices.VINNumberRequest[] vnrs = lst.ToArray();

            object o = svr1.GetVehicleData(vnrs, username, pwd);



            //while (true)
            //{
            //    string d = ic.GetCurrentDateTest().ToString("dd/MMM/yyyy HH:mm:ss");
            //    Console.WriteLine(string.Format("{0}\t{1}",d,i));
            //    i++;
            //}
        }
    }
}
