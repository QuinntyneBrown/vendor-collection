using MediatR;
using VendorCollection.Data;
using VendorCollection.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.Contacts
{
    public class GetContactsQuery
    {
        public class GetContactsRequest : IRequest<GetContactsResponse> { 
            public int? TenantId { get; set; }		
		}

        public class GetContactsResponse
        {
            public ICollection<ContactApiModel> Contacts { get; set; } = new HashSet<ContactApiModel>();
        }

        public class GetContactsHandler : IAsyncRequestHandler<GetContactsRequest, GetContactsResponse>
        {
            public GetContactsHandler(VendorCollectionContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetContactsResponse> Handle(GetContactsRequest request)
            {
                var contacts = await _context.Contacts
				    .Where( x => x.TenantId == request.TenantId )
                    .ToListAsync();

                return new GetContactsResponse()
                {
                    Contacts = contacts.Select(x => ContactApiModel.FromContact(x)).ToList()
                };
            }

            private readonly VendorCollectionContext _context;
            private readonly ICache _cache;
        }

    }

}
