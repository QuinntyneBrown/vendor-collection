using MediatR;
using VendorCollection.Data;
using VendorCollection.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.VendorEvaluations
{
    public class GetVendorEvaluationCriteriaItemsQuery
    {
        public class GetVendorEvaluationCriteriaItemsRequest : IRequest<GetVendorEvaluationCriteriaItemsResponse> { }

        public class GetVendorEvaluationCriteriaItemsResponse
        {
            public ICollection<VendorEvaluationCriteriaItemApiModel> VendorEvaluationCriteriaItems { get; set; } = new HashSet<VendorEvaluationCriteriaItemApiModel>();
        }

        public class GetVendorEvaluationCriteriaItemsHandler : IAsyncRequestHandler<GetVendorEvaluationCriteriaItemsRequest, GetVendorEvaluationCriteriaItemsResponse>
        {
            public GetVendorEvaluationCriteriaItemsHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetVendorEvaluationCriteriaItemsResponse> Handle(GetVendorEvaluationCriteriaItemsRequest request)
            {
                var vendorEvaluationCriteriaItems = await _dataContext.VendorEvaluationCriteriaItems.ToListAsync();
                return new GetVendorEvaluationCriteriaItemsResponse()
                {
                    VendorEvaluationCriteriaItems = vendorEvaluationCriteriaItems.Select(x => VendorEvaluationCriteriaItemApiModel.FromVendorEvaluationCriteriaItem(x)).ToList()
                };
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
