using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Datalistener.CalAmp.DataObjects
{
    class LogFileProcessor
    {

        public static void ProcessLogs()
        {

            while (true)
            {

                string logFileLocation = @"C:\temp\logs\devicedata\";

                List<string> newFiles = System.IO.Directory.GetFiles(logFileLocation, "*.log").ToList();


                var apiController = new API.DataAccessController();


                foreach (string filename in newFiles)
                {

                    try
                    {

                        string filecontents = System.IO.File.ReadAllText(filename);

                        List<string> geoCoords = filecontents.Split(',').ToList();

                        bool sendWarningEmail = false;

                        if (sendWarningEmail)
                        {
                            //Classes.EmailHelper.sendEmail("admin@nanosoft.com.au",
                            //       "large dataset received",
                            //       "this is a system warning saying that a large dataset was received , for example:" + geoCoords.First());
                        }

                        foreach (string geocord in geoCoords)
                        {

                            string truckid = null;
                            decimal lat = 0, lng = 0;
                            DateTime time = new DateTime();
                            bool wasPArsedOK = false;

                            try
                            {

                                string qryStrs = geocord.Split('?')[1];
                                Dictionary<string, string> dict = new Dictionary<string, string>();

                                foreach (string s2 in qryStrs.Split('&'))
                                {
                                    string[] stArr = s2.Split('=');
                                    dict.Add(stArr[0], stArr[1]);
                                    Console.WriteLine(s2);
                                }

                                truckid = dict["truckid"];
                                lat = decimal.Parse(dict["lat"]);
                                lng = decimal.Parse(dict["lng"]);
                                time = DateTime.Parse(dict["time"].Replace("%20", " "));

                                wasPArsedOK = true;
                            }

                            catch (Exception ex)
                            {
                                //if it wont parse, then ignore (who cares, could be anything), just log locally
                                //we dont want the reaspberry pi stalling. It has done its job.
                            }

                            if (wasPArsedOK == true && truckid.ToLower() != "should never = this")
                            { apiController.Get(truckid, lat, lng, time.ToString("dd/MMM/yyyy HH:mm:ss")); }

                            System.IO.File.Move(filename, filename + ".processed");

                        }

                    }
                    catch (Exception)
                    {

                        //nop
                    }
                }

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
            }


        }

    }
}
