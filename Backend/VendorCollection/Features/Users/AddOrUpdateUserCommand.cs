using MediatR;
using VendorCollection.Data;
using VendorCollection.Data.Models;
using VendorCollection.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.Users
{
    public class AddOrUpdateUserCommand
    {
        public class AddOrUpdateUserRequest : IRequest<AddOrUpdateUserResponse>
        {
            public UserApiModel User { get; set; }
        }

        public class AddOrUpdateUserResponse
        {

        }

        public class AddOrUpdateUserHandler : IAsyncRequestHandler<AddOrUpdateUserRequest, AddOrUpdateUserResponse>
        {
            public AddOrUpdateUserHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<AddOrUpdateUserResponse> Handle(AddOrUpdateUserRequest request)
            {
                var entity = await _dataContext.Users
                    .SingleOrDefaultAsync(x => x.Id == request.User.Id && x.IsDeleted == false);
                if (entity == null) _dataContext.Users.Add(entity = new User());
                entity.Name = request.User.Name;
                await _dataContext.SaveChangesAsync();

                return new AddOrUpdateUserResponse()
                {

                };
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
