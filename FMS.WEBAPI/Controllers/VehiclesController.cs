using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using FMS.Business.DataObjects;
using System.Web.Http;

namespace FMS.WEBAPI.Controllers
{
    public class VehiclesController : ApiController
    {
        // GET api/vehicles
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/vehicles/5
        public List<CAN_MessageDefinition> Get(string  vehicleID)
        {
            return FMS.Business.DataObjects.Device.GetAvailableCANTags(vehicleID,"zagro");
        }

        /// <summary>
        /// returns the last 10 mintes worth of data for an specific SPN
        /// </summary>
        /// <param name="vehicleID"></param>
        /// <param name="SPN">The Canbus protocol SPN</param>
        /// <param name="desc">the "flavor" of the SPN you require (matches the description of the "getAll" method
        /// this is used for custom CanOpen implementation a dn if you are expecting j1939 for instance , please ignore.</param>
        /// <returns></returns>    
        public CanDataPoint Get(string vehicleID, int SPN, string desc)
        {
            CanDataPoint cdp = FMS.Business.DataObjects.CanDataPoint.GetPoint(SPN, "zagro", vehicleID, desc);
            return cdp;
        }

        // POST api/vehicles
        public void Post([FromBody]string value)
        {
        }

        // PUT api/vehicles/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/vehicles/5
        public void Delete(int id)
        {
        }
    }
}
