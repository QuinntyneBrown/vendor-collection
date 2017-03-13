using MediatR;
using VendorCollection.Data;
using VendorCollection.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.Vendors
{
    public class GetVendorByIdQuery
    {
        public class GetVendorByIdRequest : IRequest<GetVendorByIdResponse> { 
            public int Id { get; set; }
			public int? TenantId { get; set; }
        }

        public class GetVendorByIdResponse
        {
            public VendorApiModel Vendor { get; set; } 
        }

        public class GetVendorByIdHandler : IAsyncRequestHandler<GetVendorByIdRequest, GetVendorByIdResponse>
        {
            public GetVendorByIdHandler(VendorCollectionContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetVendorByIdResponse> Handle(GetVendorByIdRequest request)
            {                
                return new GetVendorByIdResponse()
                {
                    Vendor = VendorApiModel.FromVendor(await _context.Vendors.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId))
                };
            }

            private readonly VendorCollectionContext _context;
            private readonly ICache _cache;
        }

    }

}
