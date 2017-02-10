using MediatR;
using VendorCollection.Data;
using VendorCollection.Data.Models;
using VendorCollection.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.VendorEvaluations
{
    public class RemoveVendorEvaluationCommand
    {
        public class RemoveVendorEvaluationRequest : IRequest<RemoveVendorEvaluationResponse>
        {
            public int Id { get; set; }
        }

        public class RemoveVendorEvaluationResponse { }

        public class RemoveVendorEvaluationHandler : IAsyncRequestHandler<RemoveVendorEvaluationRequest, RemoveVendorEvaluationResponse>
        {
            public RemoveVendorEvaluationHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<RemoveVendorEvaluationResponse> Handle(RemoveVendorEvaluationRequest request)
            {
                var vendorEvaluation = await _dataContext.VendorEvaluations.FindAsync(request.Id);
                vendorEvaluation.IsDeleted = true;
                await _dataContext.SaveChangesAsync();
                return new RemoveVendorEvaluationResponse();
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }
    }
}
