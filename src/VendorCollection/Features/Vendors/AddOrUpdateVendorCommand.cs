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
    public class AddOrUpdateVendorCommand
    {
        public class AddOrUpdateVendorRequest : IRequest<AddOrUpdateVendorResponse>
        {
            public VendorApiModel Vendor { get; set; }
			public int? TenantId { get; set; }
        }

        public class AddOrUpdateVendorResponse { }

        public class AddOrUpdateVendorHandler : IAsyncRequestHandler<AddOrUpdateVendorRequest, AddOrUpdateVendorResponse>
        {
            public AddOrUpdateVendorHandler(VendorCollectionContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateVendorResponse> Handle(AddOrUpdateVendorRequest request)
            {
                var entity = await _context.Vendors
                    .SingleOrDefaultAsync(x => x.Id == request.Vendor.Id && x.TenantId == request.TenantId);
                if (entity == null) _context.Vendors.Add(entity = new Vendor());
                entity.Name = request.Vendor.Name;
				entity.TenantId = request.TenantId;

                await _context.SaveChangesAsync();

                return new AddOrUpdateVendorResponse();
            }

            private readonly VendorCollectionContext _context;
            private readonly ICache _cache;
        }

    }

}
