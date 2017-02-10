using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace VendorCollection.Features.Evaluations
{
    [Authorize]
    [RoutePrefix("api/evaluation")]
    public class EvaluationController : ApiController
    {
        public EvaluationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateEvaluationCommand.AddOrUpdateEvaluationResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateEvaluationCommand.AddOrUpdateEvaluationRequest request)
            => Ok(await _mediator.Send(request));

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateEvaluationCommand.AddOrUpdateEvaluationResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateEvaluationCommand.AddOrUpdateEvaluationRequest request)
            => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetEvaluationsQuery.GetEvaluationsResponse))]
        public async Task<IHttpActionResult> Get()
            => Ok(await _mediator.Send(new GetEvaluationsQuery.GetEvaluationsRequest()));

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetEvaluationByIdQuery.GetEvaluationByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetEvaluationByIdQuery.GetEvaluationByIdRequest request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveEvaluationCommand.RemoveEvaluationResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveEvaluationCommand.RemoveEvaluationRequest request)
            => Ok(await _mediator.Send(request));

        protected readonly IMediator _mediator;

    }
}
