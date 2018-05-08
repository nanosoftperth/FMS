using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FMS.WEBAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //protected void Application_BeginRequest()
        //{

        //    //CORS
        //    if (Request.Headers.AllKeys.Contains("Origin"))
        //    {
        //        Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:8000");
        //        //Response.Headers.Add("Access-Control-Allow-Methods", "OPTIONS, GET, POST, PUT, DELETE");
        //        Response.Headers.Add("Access-Control-Allow-Methods", "*");
        //        //Response.Headers.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Methods, Access-Control-Allow-Origin, Content-Type, Accept, X-Requested-With, Session");
        //        Response.Headers.Add("Access-Control-Allow-Headers", "*");
        //        //handle CORS pre-flight requests
        //        if (Request.HttpMethod == "OPTIONS") 
        //            Response.Flush();
        //    }
        //}
    }
}
