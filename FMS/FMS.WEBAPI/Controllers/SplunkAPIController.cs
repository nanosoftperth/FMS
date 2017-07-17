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
        public IHttpActionResult SaveVehicleData()
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) =>
                {
                    return true;
                };

                var middleware = new HttpEventCollectorResendMiddleware(100);
                var ecSender = new HttpEventCollectorSender(new Uri("http://localhost:8088"),
                    "0C33C6E1-969A-43AD-8EBE-BA1D5D243556",
                    null,
                    HttpEventCollectorSender.SendMode.Sequential,
                    0,
                    0,
                    0,
                    middleware.Plugin
                );
                ecSender.OnError += o => Console.WriteLine(o.Message);
                 
                DateTime sd = DateTime.Parse("01/01/2017");
                DateTime ed = DateTime.Parse("07/17/2017"); 
                CanDataPoint cdp = FMS.Business.DataObjects.CanDataPoint.GetPointWithData(1, "demo1", "Zagro125", sd, ed);
                 
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
 