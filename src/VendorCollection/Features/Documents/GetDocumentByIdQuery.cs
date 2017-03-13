using MediatR;
using VendorCollection.Data;
using VendorCollection.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.Documents
{
    public class GetDocumentByIdQuery
    {
        public class GetDocumentByIdRequest : IRequest<GetDocumentByIdResponse> { 
            public int Id { get; set; }
			public int? TenantId { get; set; }
        }

        public class GetDocumentByIdResponse
        {
            public DocumentApiModel Document { get; set; } 
        }

        public class GetDocumentByIdHandler : IAsyncRequestHandler<GetDocumentByIdRequest, GetDocumentByIdResponse>
        {
            public GetDocumentByIdHandler(VendorCollectionContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetDocumentByIdResponse> Handle(GetDocumentByIdRequest request)
            {                
                return new GetDocumentByIdResponse()
                {
                    Document = DocumentApiModel.FromDocument(await _context.Documents.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId))
                };
            }

            private readonly VendorCollectionContext _context;
            private readonly ICache _cache;
        }

    }

}
