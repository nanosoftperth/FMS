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

        public static PISDK.PISDK x = new PISDK.PISDK();
        public static PISDK.Server pis = x.Servers.DefaultServer;


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
            return "jonathan";
        }



        // POST api/values 
        //[Route("Post/{solution}/{answer}")]
        [HttpPost]
        public string Post([FromBody]string value)
        {
            try
            {
                //data packet needs to contain: canbus data (multiple rows or however you get it working)
                //other fields: "usrname, password, companyname

                //receive canbus data 

                string fileNameAndloc = FILELOCATION + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";               

                System.IO.File.WriteAllText(fileNameAndloc, value);

                //look to see if the PI point exists, if not CREATE IT

                //int64 data type (8 bytes)

                //save the RAW data into PI 

                return "success";

            }
            catch (Exception ex) { return "Exception" + ex.Message; }

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
