using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FMS.WEB.API.Controllers
{
    public class ApplicationsController : ApiController
    {
        // GET api/applications
        public IEnumerable<string> Get()
        {
            return new string[] { "app1", "app2" };
        }

        // GET api/applications/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/applications
        public void Post([FromBody]string value)
        {
            

        }

        // PUT api/applications/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/applications/5
        public void Delete(int id)
        {
        }
    }
}
