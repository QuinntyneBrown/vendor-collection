using VendorCollection.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static VendorCollection.Features.Documents.AddOrUpdateDocumentCommand;
using static VendorCollection.Features.Documents.GetDocumentsQuery;
using static VendorCollection.Features.Documents.GetDocumentByIdQuery;
using static VendorCollection.Features.Documents.RemoveDocumentCommand;

namespace VendorCollection.Features.Documents
{
    [Authorize]
    [RoutePrefix("api/document")]
    public class DocumentController : ApiController
    {
        public DocumentController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateDocumentResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateDocumentRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateDocumentResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateDocumentRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetDocumentsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetDocumentsRequest();
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetDocumentByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetDocumentByIdRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveDocumentResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveDocumentRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
