using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace VendorCollection.Features.Vendors
{
    [Authorize]
    [RoutePrefix("api/vendor")]
    public class VendorController : ApiController
    {
        public VendorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateVendorCommand.AddOrUpdateVendorResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateVendorCommand.AddOrUpdateVendorRequest request)
            => Ok(await _mediator.Send(request));

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateVendorCommand.AddOrUpdateVendorResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateVendorCommand.AddOrUpdateVendorRequest request)
            => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetVendorsQuery.GetVendorsResponse))]
        public async Task<IHttpActionResult> Get()
            => Ok(await _mediator.Send(new GetVendorsQuery.GetVendorsRequest()));

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetVendorByIdQuery.GetVendorByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetVendorByIdQuery.GetVendorByIdRequest request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveVendorCommand.RemoveVendorResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveVendorCommand.RemoveVendorRequest request)
            => Ok(await _mediator.Send(request));

        protected readonly IMediator _mediator;

    }
}
