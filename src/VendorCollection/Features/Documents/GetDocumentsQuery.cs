using MediatR;
using VendorCollection.Data;
using VendorCollection.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.Documents
{
    public class GetDocumentsQuery
    {
        public class GetDocumentsRequest : IRequest<GetDocumentsResponse> { 
            public int? TenantId { get; set; }		
		}

        public class GetDocumentsResponse
        {
            public ICollection<DocumentApiModel> Documents { get; set; } = new HashSet<DocumentApiModel>();
        }

        public class GetDocumentsHandler : IAsyncRequestHandler<GetDocumentsRequest, GetDocumentsResponse>
        {
            public GetDocumentsHandler(VendorCollectionContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetDocumentsResponse> Handle(GetDocumentsRequest request)
            {
                var documents = await _context.Documents
				    .Where( x => x.TenantId == request.TenantId )
                    .ToListAsync();

                return new GetDocumentsResponse()
                {
                    Documents = documents.Select(x => DocumentApiModel.FromDocument(x)).ToList()
                };
            }

            private readonly VendorCollectionContext _context;
            private readonly ICache _cache;
        }

    }

}
