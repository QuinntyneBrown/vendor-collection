using MediatR;
using VendorCollection.Data;
using VendorCollection.Data.Model;
using VendorCollection.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.SelectionCriterion
{
    public class RemoveSelectionCriteriaCommand
    {
        public class RemoveSelectionCriteriaRequest : IRequest<RemoveSelectionCriteriaResponse>
        {
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class RemoveSelectionCriteriaResponse { }

        public class RemoveSelectionCriteriaHandler : IAsyncRequestHandler<RemoveSelectionCriteriaRequest, RemoveSelectionCriteriaResponse>
        {
            public RemoveSelectionCriteriaHandler(VendorCollectionContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveSelectionCriteriaResponse> Handle(RemoveSelectionCriteriaRequest request)
            {
                var selectionCriteria = await _context.SelectionCriterion.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId);
                selectionCriteria.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveSelectionCriteriaResponse();
            }

            private readonly VendorCollectionContext _context;
            private readonly ICache _cache;
        }
    }
}
