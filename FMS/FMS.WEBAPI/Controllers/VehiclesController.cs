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
            return FMS.Business.DataObjects.Device.GetAvailableCANTags(vehicleID);
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
