using MediatR;
using VendorCollection.Data;
using VendorCollection.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.Contacts
{
    public class GetContactByIdQuery
    {
        public class GetContactByIdRequest : IRequest<GetContactByIdResponse> { 
            public int Id { get; set; }
			public int? TenantId { get; set; }
        }

        public class GetContactByIdResponse
        {
            public ContactApiModel Contact { get; set; } 
        }

        public class GetContactByIdHandler : IAsyncRequestHandler<GetContactByIdRequest, GetContactByIdResponse>
        {
            public GetContactByIdHandler(VendorCollectionContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetContactByIdResponse> Handle(GetContactByIdRequest request)
            {                
                return new GetContactByIdResponse()
                {
                    Contact = ContactApiModel.FromContact(await _context.Contacts.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId))
                };
            }

            private readonly VendorCollectionContext _context;
            private readonly ICache _cache;
        }

    }

}
