using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace VendorCollection.Features.Evaluations
{
    [Authorize]
    [RoutePrefix("api/evaluationCriteriaItem")]
    public class EvaluationCriteriaItemController : ApiController
    {
        public EvaluationCriteriaItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateEvaluationCriteriaItemCommand.AddOrUpdateEvaluationCriteriaItemResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateEvaluationCriteriaItemCommand.AddOrUpdateEvaluationCriteriaItemRequest request)
            => Ok(await _mediator.Send(request));

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateEvaluationCriteriaItemCommand.AddOrUpdateEvaluationCriteriaItemResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateEvaluationCriteriaItemCommand.AddOrUpdateEvaluationCriteriaItemRequest request)
            => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetEvaluationCriteriaItemsQuery.GetEvaluationCriteriaItemsResponse))]
        public async Task<IHttpActionResult> Get()
            => Ok(await _mediator.Send(new GetEvaluationCriteriaItemsQuery.GetEvaluationCriteriaItemsRequest()));

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetEvaluationCriteriaItemByIdQuery.GetEvaluationCriteriaItemByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetEvaluationCriteriaItemByIdQuery.GetEvaluationCriteriaItemByIdRequest request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveEvaluationCriteriaItemCommand.RemoveEvaluationCriteriaItemResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveEvaluationCriteriaItemCommand.RemoveEvaluationCriteriaItemRequest request)
            => Ok(await _mediator.Send(request));

        protected readonly IMediator _mediator;

    }
}
