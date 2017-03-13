using VendorCollection.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static VendorCollection.Features.Users.AddOrUpdateUserCommand;
using static VendorCollection.Features.Users.GetUsersQuery;
using static VendorCollection.Features.Users.GetUserByIdQuery;
using static VendorCollection.Features.Users.RemoveUserCommand;
using static VendorCollection.Features.Users.GetUserByUsernameQuery;

namespace VendorCollection.Features.Users
{
    [Authorize]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        public UserController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateUserResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateUserRequest request)
        {
            var user = await _userManager.GetUserAsync(User);
            request.TenantId = user.TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateUserResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateUserRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetUsersResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetUsersQuery.GetUsersRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetUserByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetUserByIdRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveUserResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveUserRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("current")]
        [HttpGet]
        [AllowAnonymous]
        [ResponseType(typeof(GetUserByUsernameResponse))]
        public async Task<IHttpActionResult> Current()
        {
            if (!User.Identity.IsAuthenticated)
                return Ok();

            var request = new GetUserByUsernameRequest() { Username = User.Identity.Name };            
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
