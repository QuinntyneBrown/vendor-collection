using MediatR;
using VendorCollection.Data;
using VendorCollection.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.VendorEvaluations
{
    public class GetVendorEvaluationCriteriaItemByIdQuery
    {
        public class GetVendorEvaluationCriteriaItemByIdRequest : IRequest<GetVendorEvaluationCriteriaItemByIdResponse> { 
			public int Id { get; set; }
		}

        public class GetVendorEvaluationCriteriaItemByIdResponse
        {
            public VendorEvaluationCriteriaItemApiModel VendorEvaluationCriteriaItem { get; set; } 
		}

        public class GetVendorEvaluationCriteriaItemByIdHandler : IAsyncRequestHandler<GetVendorEvaluationCriteriaItemByIdRequest, GetVendorEvaluationCriteriaItemByIdResponse>
        {
            public GetVendorEvaluationCriteriaItemByIdHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetVendorEvaluationCriteriaItemByIdResponse> Handle(GetVendorEvaluationCriteriaItemByIdRequest request)
            {                
                return new GetVendorEvaluationCriteriaItemByIdResponse()
                {
                    VendorEvaluationCriteriaItem = VendorEvaluationCriteriaItemApiModel.FromVendorEvaluationCriteriaItem(await _dataContext.VendorEvaluationCriteriaItems.FindAsync(request.Id))
                };
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
