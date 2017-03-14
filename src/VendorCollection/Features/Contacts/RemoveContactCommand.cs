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
    public class RemoveContactCommand
    {
        public class RemoveContactRequest : IRequest<RemoveContactResponse>
        {
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class RemoveContactResponse { }

        public class RemoveContactHandler : IAsyncRequestHandler<RemoveContactRequest, RemoveContactResponse>
        {
            public RemoveContactHandler(VendorCollectionContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveContactResponse> Handle(RemoveContactRequest request)
            {
                var contact = await _context.Contacts.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId);
                contact.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveContactResponse();
            }

            private readonly VendorCollectionContext _context;
            private readonly ICache _cache;
        }
    }
}
