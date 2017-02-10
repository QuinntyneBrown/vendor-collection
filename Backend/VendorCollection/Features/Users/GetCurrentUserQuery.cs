using MediatR;
using VendorCollection.Data;
using VendorCollection.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.Users
{
    public class GetCurrentUserQuery
    {
        public class GetCurrentUserRequest : IRequest<GetCurrentUserResponse>
        {
            public GetCurrentUserRequest()
            {

            }
        }

        public class GetCurrentUserResponse
        {
            public GetCurrentUserResponse()
            {

            }
        }

        public class GetCurrentUserHandler : IAsyncRequestHandler<GetCurrentUserRequest, GetCurrentUserResponse>
        {
            public GetCurrentUserHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetCurrentUserResponse> Handle(GetCurrentUserRequest request)
            {
				throw new System.NotImplementedException();
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
