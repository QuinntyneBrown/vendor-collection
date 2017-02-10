using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace VendorCollection.Features.VendorEvaluations
{
    [Authorize]
    [RoutePrefix("api/vendorEvaluationCriteriaItem")]
    public class VendorEvaluationCriteriaItemController : ApiController
    {
        public VendorEvaluationCriteriaItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateVendorEvaluationCriteriaItemCommand.AddOrUpdateVendorEvaluationCriteriaItemResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateVendorEvaluationCriteriaItemCommand.AddOrUpdateVendorEvaluationCriteriaItemRequest request)
            => Ok(await _mediator.Send(request));

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateVendorEvaluationCriteriaItemCommand.AddOrUpdateVendorEvaluationCriteriaItemResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateVendorEvaluationCriteriaItemCommand.AddOrUpdateVendorEvaluationCriteriaItemRequest request)
            => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetVendorEvaluationCriteriaItemsQuery.GetVendorEvaluationCriteriaItemsResponse))]
        public async Task<IHttpActionResult> Get()
            => Ok(await _mediator.Send(new GetVendorEvaluationCriteriaItemsQuery.GetVendorEvaluationCriteriaItemsRequest()));

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetVendorEvaluationCriteriaItemByIdQuery.GetVendorEvaluationCriteriaItemByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetVendorEvaluationCriteriaItemByIdQuery.GetVendorEvaluationCriteriaItemByIdRequest request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveVendorEvaluationCriteriaItemCommand.RemoveVendorEvaluationCriteriaItemResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveVendorEvaluationCriteriaItemCommand.RemoveVendorEvaluationCriteriaItemRequest request)
            => Ok(await _mediator.Send(request));

        protected readonly IMediator _mediator;

    }
}
