using MediatR;
using VendorCollection.Data;
using VendorCollection.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.Evaluations
{
    public class GetEvaluationByIdQuery
    {
        public class GetEvaluationByIdRequest : IRequest<GetEvaluationByIdResponse> { 
			public int Id { get; set; }
		}

        public class GetEvaluationByIdResponse
        {
            public EvaluationApiModel Evaluation { get; set; } 
		}

        public class GetEvaluationByIdHandler : IAsyncRequestHandler<GetEvaluationByIdRequest, GetEvaluationByIdResponse>
        {
            public GetEvaluationByIdHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetEvaluationByIdResponse> Handle(GetEvaluationByIdRequest request)
            {                
                return new GetEvaluationByIdResponse()
                {
                    Evaluation = EvaluationApiModel.FromEvaluation(await _dataContext.Evaluations.FindAsync(request.Id))
                };
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
