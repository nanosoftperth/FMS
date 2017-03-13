using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Http;


namespace FMS.Datalistener.CalAmp.API
{
    public class DataAccessController : ApiController
    {

        public static PISDK.PISDK x = new PISDK.PISDK();
        public static PISDK.Server pis = x.Servers.DefaultServer;


        public string Get(string truckid, string msg)
        {
            try
            {

                string filename = DateTime.Now.ToString("yyyyMMddHH");
                //filename = string.Format(@"",filename)
                File.AppendAllText(@"c:\temp\" + filename + ".log", string.Format("{0} truckid:{1},msg:{2}{3},"
                               , DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), truckid, msg, Environment.NewLine));

                // System.Diagnostics.Debugger.Launch();
                //System.Diagnostics.Debugger.Log(1, "", time);               
                PISDK.PIPoint messagepip = pis.PIPoints["MessagesFromDevices"];
                string newmsg = string.Format("{0}: {1}", truckid, msg);

                PITimeServer.PITime pit = new PITimeServer.PITime();

                pit.LocalDate = DateTime.Now;

                messagepip.Data.UpdateValue(newmsg, pit, PISDK.DataMergeConstants.dmInsertDuplicates);

                PISDK.PIPoint logmsg = pis.PIPoints[String.Format("{0}_log", truckid)];
                logmsg.Data.UpdateValue(newmsg, pit, PISDK.DataMergeConstants.dmInsertDuplicates);

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


                string filename = DateTime.Now.ToString("yyyyMMddHH");

                File.AppendAllText(@"c:\temp\" + filename + ".log", string.Format("{0} truckid:{1},lat:{2},long:{3},time:{4}{5}", DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), truckid, lat, lng, time, Environment.NewLine));

                string latTagStr = string.Format("{0}_lat", truckid);
                string lngTagStr = string.Format("{0}_long", truckid);

                DateTime evntDate = DateTime.Parse(time);

                PISDK.PIPoint piTagLat = pis.PIPoints[latTagStr];
                PISDK.PIPoint piTagLng = pis.PIPoints[lngTagStr];

                PITimeServer.PITime pitime = new PITimeServer.PITime();

                pitime.LocalDate = evntDate;

                PISDK.PIValue piValLat = new PISDK.PIValue();
                piValLat.Value = lat;
                piValLat.TimeStamp = pitime;

                PISDK.PIValue piValLong = new PISDK.PIValue();
                piValLong.Value = lng;
                piValLong.TimeStamp = pitime;

                piTagLat.Data.UpdateValue(piValLat, pitime);
                piTagLng.Data.UpdateValue(piValLong, pitime);

                string msg = string.Format("time:{0},lat:{1},lng{2}", pitime.LocalDate.ToString("dd/MMM/yyyy HH:mm:ss"), lat, lng);

                PISDK.PIPoint messagepip = pis.PIPoints["MessagesFromDevices"];
                string newmsg = string.Format("{0}: {1}", truckid, msg);

                PITimeServer.PITime pit = new PITimeServer.PITime();

                pit.LocalDate = DateTime.Now;
                messagepip.Data.UpdateValue(newmsg, pit, PISDK.DataMergeConstants.dmInsertDuplicates);

                PISDK.PIPoint logmsg = pis.PIPoints[String.Format("{0}_log", truckid)];
                logmsg.Data.UpdateValue(newmsg, pit, PISDK.DataMergeConstants.dmInsertDuplicates);

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
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5 
        public string Get(int id)
        {
            return "value";
        }



        // POST api/values 
        public string Post([FromBody]string value)
        {
            try
            {
                string filename = DateTime.Now.ToString("yyyyMMddHHmmss");


                if (value.Length > 5000)
                {

                    //send email saying that a latge dataset was received to admin@nanosoft.com.au                       

                    File.AppendAllText(@"C:\temp\logs\devicedata\" + filename + ".log",
                                        string.Format("{0}{1}", value, Environment.NewLine));
                    return "success";
                }



                File.AppendAllText(@"C:\temp\" + DateTime.Now.ToString("yyyyMMddHH") + "_POST_.log",
                                            string.Format("{0}{1}", value, Environment.NewLine));

                foreach (string s in value.Split(','))
                {

                    string truckid = null;
                    decimal lat = 0, lng = 0;
                    DateTime time = new DateTime();
                    bool wasPArsedOK = false;

                    try
                    {

                        string qryStrs = s.Split('?')[1];
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
                    { this.Get(truckid, lat, lng, time.ToString("dd/MMM/yyyy HH:mm:ss")); }

                }

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
