using MediatR;
using VendorCollection.Data;
using VendorCollection.Data.Model;
using VendorCollection.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.Contacts
{
    public class AddOrUpdateContactCommand
    {
        public class AddOrUpdateContactRequest : IRequest<AddOrUpdateContactResponse>
        {
            public ContactApiModel Contact { get; set; }
			public int? TenantId { get; set; }
        }

        public class AddOrUpdateContactResponse { }

        public class AddOrUpdateContactHandler : IAsyncRequestHandler<AddOrUpdateContactRequest, AddOrUpdateContactResponse>
        {
            public AddOrUpdateContactHandler(VendorCollectionContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateContactResponse> Handle(AddOrUpdateContactRequest request)
            {
                var entity = await _context.Contacts
                    .SingleOrDefaultAsync(x => x.Id == request.Contact.Id && x.TenantId == request.TenantId);
                if (entity == null) _context.Contacts.Add(entity = new Contact());
                entity.Name = request.Contact.Name;
				entity.TenantId = request.TenantId;

                await _context.SaveChangesAsync();

                return new AddOrUpdateContactResponse();
            }

            private readonly VendorCollectionContext _context;
            private readonly ICache _cache;
        }

    }

}
