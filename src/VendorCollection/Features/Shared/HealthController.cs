using System.Web.Http;

namespace VendorCollection.Features.Shared
{
    [AllowAnonymous]
    [RoutePrefix("api/health")]
    public class HealthController : ApiController
    {
        [HttpGet]
        [Route("status")]
        public IHttpActionResult Status() => Ok();
    }
}