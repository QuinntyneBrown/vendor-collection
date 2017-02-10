using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace VendorCollection.Features.VendorEvaluations
{
    [Authorize]
    [RoutePrefix("api/vendorEvaluation")]
    public class VendorEvaluationController : ApiController
    {
        public VendorEvaluationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateVendorEvaluationCommand.AddOrUpdateVendorEvaluationResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateVendorEvaluationCommand.AddOrUpdateVendorEvaluationRequest request)
            => Ok(await _mediator.Send(request));

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateVendorEvaluationCommand.AddOrUpdateVendorEvaluationResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateVendorEvaluationCommand.AddOrUpdateVendorEvaluationRequest request)
            => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetVendorEvaluationsQuery.GetVendorEvaluationsResponse))]
        public async Task<IHttpActionResult> Get()
            => Ok(await _mediator.Send(new GetVendorEvaluationsQuery.GetVendorEvaluationsRequest()));

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetVendorEvaluationByIdQuery.GetVendorEvaluationByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetVendorEvaluationByIdQuery.GetVendorEvaluationByIdRequest request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveVendorEvaluationCommand.RemoveVendorEvaluationResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveVendorEvaluationCommand.RemoveVendorEvaluationRequest request)
            => Ok(await _mediator.Send(request));

        protected readonly IMediator _mediator;

    }
}
