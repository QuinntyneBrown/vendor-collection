using MediatR;
using VendorCollection.Data;
using VendorCollection.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.Vendors
{
    public class GetVendorsQuery
    {
        public class GetVendorsRequest : IRequest<GetVendorsResponse> { }

        public class GetVendorsResponse
        {
            public ICollection<VendorApiModel> Vendors { get; set; } = new HashSet<VendorApiModel>();
        }

        public class GetVendorsHandler : IAsyncRequestHandler<GetVendorsRequest, GetVendorsResponse>
        {
            public GetVendorsHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetVendorsResponse> Handle(GetVendorsRequest request)
            {
                var vendors = await _dataContext.Vendors.ToListAsync();
                return new GetVendorsResponse()
                {
                    Vendors = vendors.Select(x => VendorApiModel.FromVendor(x)).ToList()
                };
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
