using MediatR;
using VendorCollection.Data;
using VendorCollection.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.Vendors
{
    public class GetVendorsQuery
    {
        public class GetVendorsRequest : IRequest<GetVendorsResponse> { 
            public int? TenantId { get; set; }		
		}

        public class GetVendorsResponse
        {
            public ICollection<VendorApiModel> Vendors { get; set; } = new HashSet<VendorApiModel>();
        }

        public class GetVendorsHandler : IAsyncRequestHandler<GetVendorsRequest, GetVendorsResponse>
        {
            public GetVendorsHandler(VendorCollectionContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetVendorsResponse> Handle(GetVendorsRequest request)
            {
                var vendors = await _context.Vendors
				    .Where( x => x.TenantId == request.TenantId )
                    .ToListAsync();

                return new GetVendorsResponse()
                {
                    Vendors = vendors.Select(x => VendorApiModel.FromVendor(x)).ToList()
                };
            }

            private readonly VendorCollectionContext _context;
            private readonly ICache _cache;
        }

    }

}
