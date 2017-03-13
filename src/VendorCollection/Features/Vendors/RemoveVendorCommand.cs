using MediatR;
using VendorCollection.Data;
using VendorCollection.Data.Model;
using VendorCollection.Features.Core;
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
            public int? TenantId { get; set; }
        }

        public class RemoveVendorResponse { }

        public class RemoveVendorHandler : IAsyncRequestHandler<RemoveVendorRequest, RemoveVendorResponse>
        {
            public RemoveVendorHandler(VendorCollectionContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveVendorResponse> Handle(RemoveVendorRequest request)
            {
                var vendor = await _context.Vendors.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId);
                vendor.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveVendorResponse();
            }

            private readonly VendorCollectionContext _context;
            private readonly ICache _cache;
        }
    }
}
