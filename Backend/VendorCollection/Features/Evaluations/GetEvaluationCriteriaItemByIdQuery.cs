using MediatR;
using VendorCollection.Data;
using VendorCollection.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.Evaluations
{
    public class GetEvaluationCriteriaItemByIdQuery
    {
        public class GetEvaluationCriteriaItemByIdRequest : IRequest<GetEvaluationCriteriaItemByIdResponse> { 
			public int Id { get; set; }
		}

        public class GetEvaluationCriteriaItemByIdResponse
        {
            public EvaluationCriteriaItemApiModel EvaluationCriteriaItem { get; set; } 
		}

        public class GetEvaluationCriteriaItemByIdHandler : IAsyncRequestHandler<GetEvaluationCriteriaItemByIdRequest, GetEvaluationCriteriaItemByIdResponse>
        {
            public GetEvaluationCriteriaItemByIdHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetEvaluationCriteriaItemByIdResponse> Handle(GetEvaluationCriteriaItemByIdRequest request)
            {                
                return new GetEvaluationCriteriaItemByIdResponse()
                {
                    EvaluationCriteriaItem = EvaluationCriteriaItemApiModel.FromEvaluationCriteriaItem(await _dataContext.EvaluationCriteriaItems.FindAsync(request.Id))
                };
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
