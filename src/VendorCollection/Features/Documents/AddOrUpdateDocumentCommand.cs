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
    public class AddOrUpdateDocumentCommand
    {
        public class AddOrUpdateDocumentRequest : IRequest<AddOrUpdateDocumentResponse>
        {
            public DocumentApiModel Document { get; set; }
			public int? TenantId { get; set; }
        }

        public class AddOrUpdateDocumentResponse { }

        public class AddOrUpdateDocumentHandler : IAsyncRequestHandler<AddOrUpdateDocumentRequest, AddOrUpdateDocumentResponse>
        {
            public AddOrUpdateDocumentHandler(VendorCollectionContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateDocumentResponse> Handle(AddOrUpdateDocumentRequest request)
            {
                var entity = await _context.Documents
                    .SingleOrDefaultAsync(x => x.Id == request.Document.Id && x.TenantId == request.TenantId);
                if (entity == null) _context.Documents.Add(entity = new Document());
                entity.Name = request.Document.Name;
				entity.TenantId = request.TenantId;

                await _context.SaveChangesAsync();

                return new AddOrUpdateDocumentResponse();
            }

            private readonly VendorCollectionContext _context;
            private readonly ICache _cache;
        }

    }

}
