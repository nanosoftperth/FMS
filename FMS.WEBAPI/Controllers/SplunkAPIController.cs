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



namespace FMS.WEBAPI.Controllers
{ 
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
    }
}
 