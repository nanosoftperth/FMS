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

            

            var x = FMS.Business.DataObjects.Device.GetCANMessageDefinitions("Komatsu01");

            var dict = new Dictionary<string, string>();

            dict.Add("", "");

            string allvals = "61441,ffffffffffffffff|61443,d7fa1e00ffffffff|61444,f97d98d54400ffff|64970,ffffffffffffffff|64971,ff02f1ffffffffff|65164,28ff00ffffffffff|65183,ffffffffffffffff|65184,ffffffffffffffff|65188,ffff322bffffffff|65201,ffffffff75390200|65213,00f0ffffffffffff|	65244,16130000fb5a0000|	65247,89e02e7dffffffff|65252,ff3ffffcff3fffff|65253,9d300200149c0b00|65254,0f1c06030c88ffff|65257,e80a0400e80a0400|65262,7dffc22cffffffff|65263,ffffff78ffff3200|65265,ff0000fc33001fcf|65266,ab0200000000ffff|65269,caffffffffffffff|65270,ff4460ffffffffff|65271,ffffffff2a02ffff|65279,fcffffffffffffff";


            foreach (string v in allvals.Split('|'))
            {

                var pgn = v.Split(',')[0];
                var rawValue = v.Split(',')[1];
                var spn = string.Empty;
                var standard = "j1939";

                if (!string.IsNullOrEmpty(pgn))
                {
                    //get all the SPNs for the PGN
                    var messdefs = Business.DataObjects.CAN_MessageDefinition.GetForPGN(int.Parse(pgn), standard);

                    spn = string.Empty;

                    foreach (var s in messdefs)
                    {
                        spn += (!string.IsNullOrEmpty(spn) ? "," : "") + s.SPN;
                    }

                }

                //spn = "558";

                Business.DataObjects.CanValue cv = new Business.DataObjects.CanValue();

                cv.RawValue = rawValue;
                cv.Time = DateTime.Now;

                var cvl = new Business.DataObjects.CanDataPoint.CanValueList();


                foreach (string s in spn.Split(','))
                {

                    Business.DataObjects.CAN_MessageDefinition cmd = Business.DataObjects.CAN_MessageDefinition.GetForSPN(int.Parse(s), standard);

                    cvl.j1939(ref cv, cmd);

                    if (cv.IsValid)
                    {
                        Console.WriteLine("{0} is \"{1}\" {2}", cmd.Name, cv.ValueStr, cmd.Units);
                    }
                    else
                    {
                        //Console.WriteLine("{0} IS NOT AVIALABLE", cmd.Name, cv.ValueStr, cmd.Units);
                    }


                }


            }

            //var d =  Business.DataObjects.Device.GetFromDeviceID("uniqco10");

            //var x = Business.DataObjects.Device.GetCANMessageDefinitions("uniqco10");

            //var spn = "91";

            //var rawValue = "C72A0200497C0B00";
            //var rawValue = "e80a0400e80a0400";


            // var pgn = "65257";




            Console.ReadKey();


            ///FMS.Business.GoogleGeoCodeResponse.CannonTest();

            // return null;


            // sr1.Uniqco_IntegratorClient svr1 = new sr1.Uniqco_IntegratorClient();




            // ServiceAccess.WebServices.VINNumberRequest vnr = new ServiceAccess.WebServices.VINNumberRequest();

            // //This is required ro ignore the fact that the certificate is "self signed"
            // ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;


            // DateTime startdate = DateTime.Parse("7 May 2017");

            // vnr.StartDate = startdate;
            // vnr.EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);


            // vnr.VINNumber = @"WDD2452072J277009";

            //// int i = 0;

            // string username = "webServiceAccount";
            // string pwd = "asid7@#*du@";


            // Console.WriteLine("\nSending request to server:\n");


            // List<ServiceAccess.WebServices.VINNumberRequest> lst = new List<ServiceAccess.WebServices.VINNumberRequest>();

            // lst.Add(vnr);


            // ServiceAccess.WebServices.VINNumberRequest[] vnrs = lst.ToArray();
            // svr1.Endpoint.Binding.SendTimeout = new TimeSpan(0, 30, 0);
            // object o = svr1.GetVehicleData(vnrs, username, pwd);



            // string x = "asd";

            // //while (true)
            // //{
            // //    string d = ic.GetCurrentDateTest().ToString("dd/MMM/yyyy HH:mm:ss");
            // //    Console.WriteLine(string.Format("{0}\t{1}",d,i));
            // //    i++;
            // //}
        }
    }
}
