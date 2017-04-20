
// Add the following usings:
using Owin;
using System.Web.Http;

namespace FMS.Datalistener.Owin.CalAmp
{
    public class Startup
    {
        // This method is required by Katana:
        public void Configuration(IAppBuilder app)
        {
            var webApiConfiguration = ConfigureWebApi();

            // Use the extension method provided by the WebApi.Owin library:
            app.UseWebApi(webApiConfiguration);
        }


        private HttpConfiguration ConfigureWebApi()
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{action}/{truckid}",
                new { id = RouteParameter.Optional });
            config.Routes.MapHttpRoute(
                "DefaultApi0",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
            config.Routes.MapHttpRoute(
                "DefaultApi1",
                "api/{controller}");

            config.Routes.MapHttpRoute(
                "DefaultApi2",
                "api/{controller}/{truckid}/{msg}");

            config.Routes.MapHttpRoute(
                "DefaultApi3",
                "api/{controller}/{truckid}/{lat}/{lng}/{time}");
            return config;
        }
    }
}
