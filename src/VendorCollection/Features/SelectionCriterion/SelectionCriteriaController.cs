using VendorCollection.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static VendorCollection.Features.SelectionCriterion.AddOrUpdateSelectionCriteriaCommand;
using static VendorCollection.Features.SelectionCriterion.GetSelectionCriterionQuery;
using static VendorCollection.Features.SelectionCriterion.GetSelectionCriteriaByIdQuery;
using static VendorCollection.Features.SelectionCriterion.RemoveSelectionCriteriaCommand;

namespace VendorCollection.Features.SelectionCriterion
{
    [Authorize]
    [RoutePrefix("api/selectionCriteria")]
    public class SelectionCriteriaController : ApiController
    {
        public SelectionCriteriaController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateSelectionCriteriaResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateSelectionCriteriaRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateSelectionCriteriaResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateSelectionCriteriaRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetSelectionCriterionResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetSelectionCriterionRequest();
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetSelectionCriteriaByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetSelectionCriteriaByIdRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveSelectionCriteriaResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveSelectionCriteriaRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
