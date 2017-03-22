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
    public class AddOrUpdateSelectionCriteriaCommand
    {
        public class AddOrUpdateSelectionCriteriaRequest : IRequest<AddOrUpdateSelectionCriteriaResponse>
        {
            public SelectionCriteriaApiModel SelectionCriteria { get; set; }
			public int? TenantId { get; set; }
        }

        public class AddOrUpdateSelectionCriteriaResponse { }

        public class AddOrUpdateSelectionCriteriaHandler : IAsyncRequestHandler<AddOrUpdateSelectionCriteriaRequest, AddOrUpdateSelectionCriteriaResponse>
        {
            public AddOrUpdateSelectionCriteriaHandler(VendorCollectionContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateSelectionCriteriaResponse> Handle(AddOrUpdateSelectionCriteriaRequest request)
            {
                var entity = await _context.SelectionCriterion
                    .SingleOrDefaultAsync(x => x.Id == request.SelectionCriteria.Id && x.TenantId == request.TenantId);
                if (entity == null) _context.SelectionCriterion.Add(entity = new SelectionCriteria());
                entity.Name = request.SelectionCriteria.Name;
                entity.Description = request.SelectionCriteria.Description;
				entity.TenantId = request.TenantId;

                await _context.SaveChangesAsync();

                return new AddOrUpdateSelectionCriteriaResponse();
            }

            private readonly VendorCollectionContext _context;
            private readonly ICache _cache;
        }

    }

}
