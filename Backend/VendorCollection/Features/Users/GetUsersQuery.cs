using MediatR;
using VendorCollection.Data;
using VendorCollection.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.Users
{
    public class GetUsersQuery
    {
        public class GetUsersRequest : IRequest<GetUsersResponse> { }

        public class GetUsersResponse
        {
            public ICollection<UserApiModel> Users { get; set; } = new HashSet<UserApiModel>();
        }

        public class GetUsersHandler : IAsyncRequestHandler<GetUsersRequest, GetUsersResponse>
        {
            public GetUsersHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetUsersResponse> Handle(GetUsersRequest request)
            {
                var users = await _dataContext.Users.ToListAsync();
                return new GetUsersResponse()
                {
                    Users = users.Select(x => UserApiModel.FromUser(x)).ToList()
                };
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
