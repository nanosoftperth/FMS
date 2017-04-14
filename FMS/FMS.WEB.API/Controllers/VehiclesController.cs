using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FMS.WEB.API.Controllers
{
    public class VehiclesController : ApiController
    {
        // GET api/vehicles
        public IEnumerable<string> Get()
        {
            return new string[] { "vehicle1", "vehicle2" };
        }

        // GET api/vehicles/5
        public string Get(int id)
        {
            return "value";
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
