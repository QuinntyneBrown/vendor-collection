using VendorCollection.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static VendorCollection.Features.Contacts.AddOrUpdateContactCommand;
using static VendorCollection.Features.Contacts.GetContactsQuery;
using static VendorCollection.Features.Contacts.GetContactByIdQuery;
using static VendorCollection.Features.Contacts.RemoveContactCommand;

namespace VendorCollection.Features.Contacts
{
    [Authorize]
    [RoutePrefix("api/contact")]
    public class ContactController : ApiController
    {
        public ContactController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateContactResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateContactRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateContactResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateContactRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetContactsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetContactsRequest();
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetContactByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetContactByIdRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveContactResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveContactRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
