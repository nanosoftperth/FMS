using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Http;


namespace FMS.Datalistener.CalAmp.API
{
    public class CANReceiverController : ApiController
    {

        public static PISDK.PISDK pisdk = new PISDK.PISDK();
        public static PISDK.Server piserver = pisdk.Servers.DefaultServer;


        const string FILELOCATION = @"c:\temp\rodlogs\";

        public string Get(string truckid, string msg)
        {
            try
            {



                return "success";

            }
            catch (Exception ex)
            {
                return string.Format("Exception: {0}", ex);
            }
        }

        public string Get(string truckid, decimal lat, decimal lng, string time)
        {
            try
            {




                return "success";

            }
            catch (Exception ex)
            {
                return string.Format("Exception: {0}", ex);
            }
        }

        // GET api/values 
        public IEnumerable<string> Get()
        {

            List<string> arr = new List<string>();

            try
            {

                List<string> lst = System.IO.Directory.GetFiles(FILELOCATION, "*.txt")
                                                .ToList().OrderByDescending(x => x).Take(20).ToList();

                foreach (string fileLoc in lst)
                {

                    string fileContents = System.IO.File.ReadAllText(fileLoc);
                    arr.Add(fileContents);
                }

                return arr;

            }
            catch (Exception e)
            {

                return new string[] { @"Exception fired: " + e.Message };
            }
        }

        // GET api/values/5 
        public string Get(int id)
        {
            return "you send through id " + id;
        }



        // POST api/values 
        //[Route("Post/{solution}/{answer}")]
        [HttpPost]
        public string Post([FromBody]string value)
        {
            try
            {
                //example format for data to be received
                //deviceid:ABC
                //time:1492136660
                //183 69EFFF0F
                //286 000000006B000000
                //206 80

                //save the content of the post to a file (for the first few months whilst we test etc)
                string fileNameAndloc = FILELOCATION + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                System.IO.File.WriteAllText(fileNameAndloc, value);

                //grab a list of all the commands seperatley
                List<string> cmdList = value.Split('\n').ToList();

                //get the deviceid
                string deviceID = cmdList[0].Trim().Replace("deviceid:", string.Empty);

                DateTime currentDatetime;//this is used in the loop and updated when the datetime lineitem is found.

                for (int i = 1; 1 < cmdList.Count; i++)
                {

                    string[] cmds = cmdList[i].Split(' ');
                    string itmRow = cmdList[i];

                    if (itmRow.StartsWith("time:"))
                    {
                        string newTimeUTCstr = itmRow.Remove(0, 5);
                        currentDatetime = UnixTimeStampToDateTime(newTimeUTCstr);
                        continue;
                    }

                    //get the required data from the posted line (we can only presume this is a CANbus entry at this point)
                    string arb_id = cmds[1];
                    string hexData = cmds[2];

                    string tagName = string.Format("CAN_{0}_{1}", deviceID, arb_id);

                    //store the data as a double precision floating point as that is 8 bytes (same as float64 in pi)
                    double valueForHistorizing = hex2double(hexData);

                    //does tag name exist?
                    PISDK.PointList lst = piserver.GetPoints(string.Format("tag = '{0}'", tagName));

                    //if the tag does not exist, then create it
                    if (lst.Count < 1)
                    {
                        PISDKCommon.NamedValues namedValues = new PISDKCommon.NamedValues();
                        namedValues.Add("compressing", 0);
                        namedValues.Add("pointsource", "CANBus");

                        //create the pi point if it does not exist
                        piserver.PIPoints.Add(tagName, "classic", PISDK.PointTypeConstants.pttypFloat64, namedValues);
                    }

                    //get the pipoint, if we didnt already have it found initially
                    PISDK.PIPoint foundPiPoint = lst.Count < 1 ? piserver.PIPoints[tagName] : lst[1];

                    foundPiPoint.Data.UpdateValue(valueForHistorizing, DateTime.Now);
                    int count = lst.Count;

                }

                return "success";
            
            }
            catch (Exception ex)
            {
                //we will need to log the issue, NOT stop the device from sending.
                return "success";
            }

        }


        public static DateTime UnixTimeStampToDateTime(string unixTimeStamp)
        {
            double d = Convert.ToDouble(unixTimeStamp);
            return UnixTimeStampToDateTime(d);
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        double hex2double(string hex)
        {
            double result = 0.0;
            foreach (char c in hex)
            {
                double val = (double)System.Convert.ToInt32(c.ToString(), 16);
                result = result * 16.0 + val;
            }
            return result;
        }


        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/values/5 
        public void Delete(int id)
        {

        }
    }
}
