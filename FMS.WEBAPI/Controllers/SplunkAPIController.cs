using Newtonsoft.Json.Linq;
using Splunk.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
//using FMS.Business.DataObjects;
using FMS.Business.DataObjects; 
using System.Web.Script.Serialization;
using System.Web.Http.Cors;
using Newtonsoft.Json;

namespace FMS.WEBAPI.Controllers
{ 
    [RoutePrefix("api/splunkapi")]
    public class SplunkAPIController : ApiController
    {
        /// <summary>
        /// Save JSON data to Splunk HTTP Event Collector
        /// </summary>
        [HttpPost]
        [Route("api/SplunkAPI/Vehicle")]
        [ResponseType(typeof(IHttpActionResult))]
        public IHttpActionResult SendGeoFenceDeviceCollision(string DeviceID, DateTime StartDate)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) =>
                {
                    return true;
                };

                var middleware = new HttpEventCollectorResendMiddleware(100);
                var ecSender = new HttpEventCollectorSender(new Uri("http://localhost:8088"),
                    "F018028D-5CFF-495F-A06C-1D3D2D80C379",
                    null,
                    HttpEventCollectorSender.SendMode.Sequential,
                    0,
                    0,
                    0,
                    middleware.Plugin
                );

                ecSender.OnError += o => Console.WriteLine(o.Message);
                 
                // ---- For testing
                //string strsd = StartDate.ToString("MM/dd/yyyy");
                //DateTime sd = DateTime.Parse(strsd);
                DateTime sd = StartDate;
                string DevID = DeviceID;
                //DateTime sd = DateTime.Parse("01/01/2017");
                //string DevID = "auto20";
                //////DateTime ed = DateTime.Parse("07/17/2017");
                ////var oVehicle = new FMS.Business.DataObjects.ApplicationVehicle();
                ////var oFBO = new FMS.Business.DataObjects.DashboardValue();
                ////var cdp = new List<FMS.Business.DataObjects.DashboardValue>();
                //////CanDataPoint cdp = FMS.Business.DataObjects.CanDataPoint.GetPointWithData(1, "demo1", "Zagro125", sd, ed);
                ////cdp = FMS.Business.DataObjects.DashboardValue.GetDataForDashboard("auto20");
                
                FMS.Business.DataObjects.GeoFenceDeviceCollision ListRow = new GeoFenceDeviceCollision();
                List<GeoFenceDeviceCollision> oList = new List<GeoFenceDeviceCollision>();
                
                var oAppVehicle = FMS.Business.DataObjects.ApplicationVehicle.GetFromDeviceID(DevID);
                var iAppID = oAppVehicle.ApplicationID;
                var oGFenceDevCol = FMS.Business.DataObjects.GeoFenceDeviceCollision.GetAllForApplication(iAppID, sd);

                foreach (var nRow in oGFenceDevCol)
                {
                    if (nRow.DeviceID == DevID)
                    {
                        ListRow.ApplicationGeoFenceID = nRow.ApplicationGeoFenceID;
                        ListRow.ApplicationID = nRow.ApplicationID;
                        ListRow.DeviceID = nRow.DeviceID;
                        ListRow.EndTime = nRow.EndTime;
                        ListRow.GeoFenceDeviceCollissionID = nRow.GeoFenceDeviceCollissionID;
                        ListRow.StartTime = nRow.StartTime;

                        oList.Add(ListRow);
                    }
                   
                }

                var cdp = oList;

                // ---- End of testing


                var json = new JavaScriptSerializer().Serialize(cdp); 

                dynamic obj = new JObject();
                obj.VehicleData = json;
                ecSender.Send(Guid.NewGuid().ToString(), "INFO", null, (JObject)obj); 
                ecSender.FlushAsync();                  
            }
            catch (Exception ex) { }  
            return Ok("Success"); 
        }
        /// <summary>
        /// Send Can Values
        /// </summary>
        /// <param name="application"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("SendCanValues/{application}/{dateStart}/{dateEnd}")]
        public IHttpActionResult SendCanValues(string application, string dateStart, string dateEnd)
        {
            try
            {
                List<testData> lstDt = new List<testData>
                {
                    new testData { id=11111, description="test onexyza"},
                    new testData { id=22222, description="test twoxyza"}
                };

                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) =>
                {
                    return true;
                };
                var middleware = new HttpEventCollectorResendMiddleware(100);
                //var ecSender = new HttpEventCollectorSender(new Uri("http://demo.nanosoft.com.au:8088"),
                var ecSender = new HttpEventCollectorSender(new Uri("http://localhost:8088"),
                    "4399e478-c15f-47df-9f00-17d035773ae7",
                    null,
                    HttpEventCollectorSender.SendMode.Sequential,
                    0,
                    0,
                    0,
                    middleware.Plugin
                );



                //var jsonx = new JavaScriptSerializer().Serialize(new { Foo = lstDt });
                var jsonx = JsonConvert.SerializeObject(new { foo = lstDt });
                
                ecSender.OnError += o => Console.WriteLine(o.Message);
                ecSender.Send(Guid.NewGuid().ToString(), "INFO", null, jsonx);
                ecSender.FlushAsync();
                return Ok(string.Format("successfully catched application:{0}, start date:{1}, end date:{2}", application, dateStart, dateEnd));
            }
            catch(Exception ex)
            {
                return Ok(string.Format("error:{0}" , ex.Message.ToString()));
            }
            //return Ok("success...");
        }
        public object GetCanValues(CanValueProperties cvProp, DateTime startDate, DateTime endDate)
        {
            string vehicleName = cvProp.vehicleName;
            string standard = cvProp.standard;
            int spn = cvProp.spn;
            string spnName = cvProp.spnName;
            var cDPoints = CanDataPoint.GetPointWithData(spn, vehicleName, standard, startDate, endDate);
            List<LogToDisplay> ldList = new List<LogToDisplay>();
            foreach(var cDPoint in cDPoints.CanValues)
            {
                LogToDisplay ld = new LogToDisplay();
                ld.vehicleName = vehicleName;
                ld.standard = standard;
                ld.spn = spn;
                ld.spnName = spnName;
                ld.logDescription = cDPoints.MessageDefinition.Description;
                ld.valueStr = cDPoint.ValueStr;
                ld.logTime = cDPoint.Time;
                ldList.Add(ld);
            }
            return null;
        }

        public string getLogData(string appName)
        {
            var app = Business.DataObjects.Application.GetFromApplicationName(appName);
            var vehicles = Business.DataObjects.ApplicationVehicle.GetAll(app.ApplicationID);

            //For Each av As Business.DataObjects.ApplicationVehicle In vehicles

            //    If(Not av.CAN_Protocol_Type = "j1939") Then

            //       For Each x In av.GetAvailableCANTags

            //           Dim strTag = String.Format("{0}>{1}>{2}>{3}", av.Name, x.Standard, x.SPN, IIf(x.Name = Nothing, "", x.Name))
            //            Dim TagValues = GetSPNValues(strTag, sd, ed)

            //            For Each ntag In TagValues
            //                Dim nRowSplunk As New SplunkTable

            //                nRowSplunk.VehicleName = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).VehicleName
            //                nRowSplunk.Standard = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).Standard
            //                nRowSplunk.SPN = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).SPN
            //                nRowSplunk.SPNValue = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).SPNValue
            //                nRowSplunk.Description = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).Description
            //                nRowSplunk.Time = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).Time

            //                tblSplunk.Add(nRowSplunk)
            //            Next

            //        Next

            //    End If
            //Next

            return "";
        }

        public class CanValueProperties
        {
            public string vehicleName { get; set; }
            public string standard { get; set; }
            public int spn { get; set; }
            public string spnName { get; set; }
        }
        public class LogToDisplay:CanValueProperties
        {
            public string valueStr { get; set; }
            public DateTime logTime { get; set; }
            public string logDescription { get; set; }
        }

        private class testData
        {
            public int id { get; set; }
            public string description { get; set; }
        }
    }
}
 