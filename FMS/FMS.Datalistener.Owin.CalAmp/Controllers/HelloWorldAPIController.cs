using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Builder;
using System.ComponentModel;
//using Topshelf;
using System.Diagnostics;
using Microsoft.Owin.Hosting;
using Owin;

namespace FMS.Datalistener.CalAmp.API
{
    public class HelloWorldApiController : ApiController
    {
        [HttpGet]
        public string Get()
        {
            return "Hello World";
        }
    }

    public class WebApiConfig
    {
        public static HttpConfiguration Register()
        {
            var config = new HttpConfiguration();


            config.Formatters.Add(new System.Net.Http.Formatting.JsonMediaTypeFormatter());
            config.Formatters.Add(new TextMediaTypeFormatter());//);

            config.Routes.MapHttpRoute("DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
            return config;
        }
    }

    public class OwinConfiguration
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWebApi(WebApiConfig.Register());
        }
    }

    


}
