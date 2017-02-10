using MediatR;
using VendorCollection.Data;
using VendorCollection.Data.Models;
using VendorCollection.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VendorCollection.Features.Evaluations
{
    public class RemoveEvaluationCommand
    {
        public class RemoveEvaluationRequest : IRequest<RemoveEvaluationResponse>
        {
            public int Id { get; set; }
        }

        public class RemoveEvaluationResponse { }

        public class RemoveEvaluationHandler : IAsyncRequestHandler<RemoveEvaluationRequest, RemoveEvaluationResponse>
        {
            public RemoveEvaluationHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<RemoveEvaluationResponse> Handle(RemoveEvaluationRequest request)
            {
                var evaluation = await _dataContext.Evaluations.FindAsync(request.Id);
                evaluation.IsDeleted = true;
                await _dataContext.SaveChangesAsync();
                return new RemoveEvaluationResponse();
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }
    }
}
