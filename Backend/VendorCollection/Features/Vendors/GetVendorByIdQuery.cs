using MediatR;
using VendorCollection.Data;
using VendorCollection.Utilities;
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
		}

        public class GetVendorByIdResponse
        {
            public VendorApiModel Vendor { get; set; } 
		}

        public class GetVendorByIdHandler : IAsyncRequestHandler<GetVendorByIdRequest, GetVendorByIdResponse>
        {
            public GetVendorByIdHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetVendorByIdResponse> Handle(GetVendorByIdRequest request)
            {                
                return new GetVendorByIdResponse()
                {
                    Vendor = VendorApiModel.FromVendor(await _dataContext.Vendors.FindAsync(request.Id))
                };
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
