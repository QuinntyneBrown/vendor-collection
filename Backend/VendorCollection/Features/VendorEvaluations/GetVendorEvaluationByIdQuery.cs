using MediatR;
using VendorCollection.Data;
using VendorCollection.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.VendorEvaluations
{
    public class GetVendorEvaluationByIdQuery
    {
        public class GetVendorEvaluationByIdRequest : IRequest<GetVendorEvaluationByIdResponse> { 
			public int Id { get; set; }
		}

        public class GetVendorEvaluationByIdResponse
        {
            public VendorEvaluationApiModel VendorEvaluation { get; set; } 
		}

        public class GetVendorEvaluationByIdHandler : IAsyncRequestHandler<GetVendorEvaluationByIdRequest, GetVendorEvaluationByIdResponse>
        {
            public GetVendorEvaluationByIdHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetVendorEvaluationByIdResponse> Handle(GetVendorEvaluationByIdRequest request)
            {                
                return new GetVendorEvaluationByIdResponse()
                {
                    VendorEvaluation = VendorEvaluationApiModel.FromVendorEvaluation(await _dataContext.VendorEvaluations.FindAsync(request.Id))
                };
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
