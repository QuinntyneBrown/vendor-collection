using MediatR;
using VendorCollection.Data;
using VendorCollection.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.VendorEvaluations
{
    public class GetVendorEvaluationsQuery
    {
        public class GetVendorEvaluationsRequest : IRequest<GetVendorEvaluationsResponse> { }

        public class GetVendorEvaluationsResponse
        {
            public ICollection<VendorEvaluationApiModel> VendorEvaluations { get; set; } = new HashSet<VendorEvaluationApiModel>();
        }

        public class GetVendorEvaluationsHandler : IAsyncRequestHandler<GetVendorEvaluationsRequest, GetVendorEvaluationsResponse>
        {
            public GetVendorEvaluationsHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<GetVendorEvaluationsResponse> Handle(GetVendorEvaluationsRequest request)
            {
                var vendorEvaluations = await _dataContext.VendorEvaluations.ToListAsync();
                return new GetVendorEvaluationsResponse()
                {
                    VendorEvaluations = vendorEvaluations.Select(x => VendorEvaluationApiModel.FromVendorEvaluation(x)).ToList()
                };
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
