using MediatR;
using VendorCollection.Data;
using VendorCollection.Data.Models;
using VendorCollection.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.Vendors
{
    public class RemoveVendorCommand
    {
        public class RemoveVendorRequest : IRequest<RemoveVendorResponse>
        {
            public int Id { get; set; }
        }

        public class RemoveVendorResponse { }

        public class RemoveVendorHandler : IAsyncRequestHandler<RemoveVendorRequest, RemoveVendorResponse>
        {
            public RemoveVendorHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<RemoveVendorResponse> Handle(RemoveVendorRequest request)
            {
                var vendor = await _dataContext.Vendors.FindAsync(request.Id);
                vendor.IsDeleted = true;
                await _dataContext.SaveChangesAsync();
                return new RemoveVendorResponse();
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }
    }
}
