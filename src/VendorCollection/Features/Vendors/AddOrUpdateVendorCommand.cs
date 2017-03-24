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
                    .Include(x=>x.Contacts)
                    .SingleOrDefaultAsync(x => x.Id == request.Vendor.Id && x.TenantId == request.TenantId);

                if (entity == null) _context.Vendors.Add(entity = new Vendor());

                entity.Name = request.Vendor.Name;
				entity.TenantId = request.TenantId;

                entity.Contacts.Clear();

                foreach(var contact in request.Vendor.Contacts)
                {
                    var contactEntity = await _context.Contacts.FindAsync(contact.Id);

                    if(contactEntity == null) { contactEntity = new Contact(); }                    
                    contactEntity.VendorId = entity.Id;
                    contactEntity.TenantId = request.TenantId;
                    contactEntity.Email = contact.Email;
                    contactEntity.Firstname = contact.Firstname;
                    contactEntity.Lastname = contact.Lastname;
                    contactEntity.PhoneNumber = contact.PhoneNumber;
                    contactEntity.Email = contact.Email;
                    entity.Contacts.Add(contactEntity);
                }

                await _context.SaveChangesAsync();

                return new AddOrUpdateVendorResponse();
            }

            private readonly VendorCollectionContext _context;
            private readonly ICache _cache;
        }

    }

}
