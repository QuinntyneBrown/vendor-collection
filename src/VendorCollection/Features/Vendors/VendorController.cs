using VendorCollection.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static VendorCollection.Features.Vendors.AddOrUpdateVendorCommand;
using static VendorCollection.Features.Vendors.GetVendorsQuery;
using static VendorCollection.Features.Vendors.GetVendorByIdQuery;
using static VendorCollection.Features.Vendors.RemoveVendorCommand;

namespace VendorCollection.Features.Vendors
{
    [Authorize]
    [RoutePrefix("api/vendor")]
    public class VendorController : ApiController
    {
        public VendorController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateVendorResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateVendorRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateVendorResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateVendorRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetVendorsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            //var request = new GetVendorsRequest() { TenantId = (await _userManager.GetUserAsync(User)).TenantId };
            var request = new GetVendorsRequest() { TenantId = 1 };
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetVendorByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetVendorByIdRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveVendorResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveVendorRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
