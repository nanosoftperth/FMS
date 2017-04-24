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
        /// <summary>
        /// Get vehicle values
        /// </summary>
        /// <returns>List of string</returns>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/vehicles/5
        /// <summary>
        /// Get available CAN tags by vehicleID
        /// </summary>
        /// <param name="vehicleID">The Vehicle Id</param>
        /// <returns>List of Can message definition</returns>
        public List<CAN_MessageDefinition> Get(string vehicleID)
        {

            return FMS.Business.DataObjects.ApplicationVehicle
                                .GetFromName(vehicleID).GetAvailableCANTags();

        }


        /// <summary>
        /// Get CAN data point by vehicleID, standard, SPN, startdate and enddate
        /// </summary>
        /// <param name="vehicleID">The vehicle Id</param>
        /// <param name="standard">The standard field</param>
        /// <param name="SPN">The SPN field</param>
        /// <param name="startdate">The startdate for the time period</param>
        /// <param name="enddate">The enddate for the time period</param>
        /// <returns>CanDataPoint</returns>
        public CanDataPoint Get(string vehicleID, string standard, int SPN, string startdate, string enddate)
        {
            //the standard of canbus is infered from the vehicleID.

            DateTime sd = DateTime.Parse(startdate);
            DateTime ed = DateTime.Parse(enddate);

            CanDataPoint cdp = FMS.Business.DataObjects.CanDataPoint.GetPointWithData(SPN, vehicleID, standard, sd, ed);

            return cdp;
        }

        // POST api/vehicles
        /// <summary>
        /// Post vehicles
        /// </summary>
        /// <param name="value">Values posted</param>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/vehicles/5
        /// <summary>
        /// Put vehicles
        /// </summary>
        /// <param name="id">by Id</param>
        /// <param name="value">String values</param>
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/vehicles/5
        /// <summary>
        /// Delete vehicle by Id
        /// </summary>
        /// <param name="id">Vehicle Id</param>
        public void Delete(int id)
        {
        }
    }
}
