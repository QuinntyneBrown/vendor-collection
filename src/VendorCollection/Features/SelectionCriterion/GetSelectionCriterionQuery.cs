using MediatR;
using VendorCollection.Data;
using VendorCollection.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.SelectionCriterion
{
    public class GetSelectionCriterionQuery
    {
        public class GetSelectionCriterionRequest : IRequest<GetSelectionCriterionResponse> { 
            public int? TenantId { get; set; }		
		}

        public class GetSelectionCriterionResponse
        {
            public ICollection<SelectionCriteriaApiModel> SelectionCriterion { get; set; } = new HashSet<SelectionCriteriaApiModel>();
        }

        public class GetSelectionCriterionHandler : IAsyncRequestHandler<GetSelectionCriterionRequest, GetSelectionCriterionResponse>
        {
            public GetSelectionCriterionHandler(VendorCollectionContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetSelectionCriterionResponse> Handle(GetSelectionCriterionRequest request)
            {
                var selectionCriterion = await _context.SelectionCriterion
				    .Where( x => x.TenantId == request.TenantId )
                    .ToListAsync();

                return new GetSelectionCriterionResponse()
                {
                    SelectionCriterion = selectionCriterion.Select(x => SelectionCriteriaApiModel.FromSelectionCriteria(x)).ToList()
                };
            }

            private readonly VendorCollectionContext _context;
            private readonly ICache _cache;
        }

    }

}
