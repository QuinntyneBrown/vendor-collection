using MediatR;
using VendorCollection.Data;
using VendorCollection.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.Evaluations
{
    public class GetEvaluationsQuery
    {
        public class GetEvaluationsRequest : IRequest<GetEvaluationsResponse> { }

        public class GetEvaluationsResponse
        {
            public ICollection<EvaluationApiModel> Evaluations { get; set; } = new HashSet<EvaluationApiModel>();
        }

        public class GetEvaluationsHandler : IAsyncRequestHandler<GetEvaluationsRequest, GetEvaluationsResponse>
        {
            public GetEvaluationsHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetEvaluationsResponse> Handle(GetEvaluationsRequest request)
            {
                var evaluations = await _dataContext.Evaluations.ToListAsync();
                return new GetEvaluationsResponse()
                {
                    Evaluations = evaluations.Select(x => EvaluationApiModel.FromEvaluation(x)).ToList()
                };
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
