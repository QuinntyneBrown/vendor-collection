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
    public class AddOrUpdateVendorCommand
    {
        public class AddOrUpdateVendorRequest : IRequest<AddOrUpdateVendorResponse>
        {
            public VendorApiModel Vendor { get; set; }
        }

        public class AddOrUpdateVendorResponse
        {

        }

        public class AddOrUpdateVendorHandler : IAsyncRequestHandler<AddOrUpdateVendorRequest, AddOrUpdateVendorResponse>
        {
            public AddOrUpdateVendorHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<AddOrUpdateVendorResponse> Handle(AddOrUpdateVendorRequest request)
            {
                var entity = await _dataContext.Vendors
                    .SingleOrDefaultAsync(x => x.Id == request.Vendor.Id && x.IsDeleted == false);
                if (entity == null) _dataContext.Vendors.Add(entity = new Vendor());
                entity.Name = request.Vendor.Name;
                await _dataContext.SaveChangesAsync();

                return new AddOrUpdateVendorResponse()
                {

                };
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
