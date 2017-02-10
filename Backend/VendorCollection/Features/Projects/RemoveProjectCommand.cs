using MediatR;
using VendorCollection.Data;
using VendorCollection.Data.Models;
using VendorCollection.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.Projects
{
    public class RemoveProjectCommand
    {
        public class RemoveProjectRequest : IRequest<RemoveProjectResponse>
        {
            public int Id { get; set; }
        }

        public class RemoveProjectResponse { }

        public class RemoveProjectHandler : IAsyncRequestHandler<RemoveProjectRequest, RemoveProjectResponse>
        {
            public RemoveProjectHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<RemoveProjectResponse> Handle(RemoveProjectRequest request)
            {
                var project = await _dataContext.Projects.FindAsync(request.Id);
                project.IsDeleted = true;
                await _dataContext.SaveChangesAsync();
                return new RemoveProjectResponse();
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }
    }
}
