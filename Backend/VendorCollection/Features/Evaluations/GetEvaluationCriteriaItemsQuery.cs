using MediatR;
using VendorCollection.Data;
using VendorCollection.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.Evaluations
{
    public class GetEvaluationCriteriaItemsQuery
    {
        public class GetEvaluationCriteriaItemsRequest : IRequest<GetEvaluationCriteriaItemsResponse> { }

        public class GetEvaluationCriteriaItemsResponse
        {
            public ICollection<EvaluationCriteriaItemApiModel> EvaluationCriteriaItems { get; set; } = new HashSet<EvaluationCriteriaItemApiModel>();
        }

        public class GetEvaluationCriteriaItemsHandler : IAsyncRequestHandler<GetEvaluationCriteriaItemsRequest, GetEvaluationCriteriaItemsResponse>
        {
            public GetEvaluationCriteriaItemsHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetEvaluationCriteriaItemsResponse> Handle(GetEvaluationCriteriaItemsRequest request)
            {
                var evaluationCriteriaItems = await _dataContext.EvaluationCriteriaItems.ToListAsync();
                return new GetEvaluationCriteriaItemsResponse()
                {
                    EvaluationCriteriaItems = evaluationCriteriaItems.Select(x => EvaluationCriteriaItemApiModel.FromEvaluationCriteriaItem(x)).ToList()
                };
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
