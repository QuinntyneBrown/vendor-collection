using MediatR;
using VendorCollection.Data;
using VendorCollection.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.SelectionCriterion
{
    public class GetSelectionCriteriaByIdQuery
    {
        public class GetSelectionCriteriaByIdRequest : IRequest<GetSelectionCriteriaByIdResponse> { 
            public int Id { get; set; }
			public int? TenantId { get; set; }
        }

        public class GetSelectionCriteriaByIdResponse
        {
            public SelectionCriteriaApiModel SelectionCriteria { get; set; } 
        }

        public class GetSelectionCriteriaByIdHandler : IAsyncRequestHandler<GetSelectionCriteriaByIdRequest, GetSelectionCriteriaByIdResponse>
        {
            public GetSelectionCriteriaByIdHandler(VendorCollectionContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetSelectionCriteriaByIdResponse> Handle(GetSelectionCriteriaByIdRequest request)
            {                
                return new GetSelectionCriteriaByIdResponse()
                {
                    SelectionCriteria = SelectionCriteriaApiModel.FromSelectionCriteria(await _context.SelectionCriterion.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId))
                };
            }

            private readonly VendorCollectionContext _context;
            private readonly ICache _cache;
        }

    }

}
