using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;


namespace FMS.WEBAPI.Models
{
    public class CustomAuhenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string AthenticationParam = string.Empty;
            string CallerToken = string.Empty;
            HttpRequestMessage re = new HttpRequestMessage();
            var headers = re.Headers;
            if (actionContext.Request.Headers.Contains("Token"))
            {
                CallerToken = actionContext.Request.Headers.GetValues("Token").FirstOrDefault();
            }
            if (actionContext.Request.Headers.Authorization == null)
            {
                // for testing purpose this code
                AthenticationParam = "aman";
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(AthenticationParam), null);
                //this code will be uncomment 
                //actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                AthenticationParam = actionContext.Request.Headers.Authorization.Parameter;
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(AthenticationParam), null);
            }
            //string ApiToken = new AuthorizeToken().Encrypttoken(AthenticationParam);
            //if (ApiToken != CallerToken)
            //    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

        }

    }
}