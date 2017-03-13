using MediatR;
using VendorCollection.Data;
using VendorCollection.Data.Model;
using VendorCollection.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.Documents
{
    public class RemoveDocumentCommand
    {
        public class RemoveDocumentRequest : IRequest<RemoveDocumentResponse>
        {
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class RemoveDocumentResponse { }

        public class RemoveDocumentHandler : IAsyncRequestHandler<RemoveDocumentRequest, RemoveDocumentResponse>
        {
            public RemoveDocumentHandler(VendorCollectionContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveDocumentResponse> Handle(RemoveDocumentRequest request)
            {
                var document = await _context.Documents.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId);
                document.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveDocumentResponse();
            }

            private readonly VendorCollectionContext _context;
            private readonly ICache _cache;
        }
    }
}
