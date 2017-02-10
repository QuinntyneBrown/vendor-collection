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
    public class RemoveEvaluationCriteriaItemCommand
    {
        public class RemoveEvaluationCriteriaItemRequest : IRequest<RemoveEvaluationCriteriaItemResponse>
        {
            public int Id { get; set; }
        }

        public class RemoveEvaluationCriteriaItemResponse { }

        public class RemoveEvaluationCriteriaItemHandler : IAsyncRequestHandler<RemoveEvaluationCriteriaItemRequest, RemoveEvaluationCriteriaItemResponse>
        {
            public RemoveEvaluationCriteriaItemHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<RemoveEvaluationCriteriaItemResponse> Handle(RemoveEvaluationCriteriaItemRequest request)
            {
                var evaluationCriteriaItem = await _dataContext.EvaluationCriteriaItems.FindAsync(request.Id);
                evaluationCriteriaItem.IsDeleted = true;
                await _dataContext.SaveChangesAsync();
                return new RemoveEvaluationCriteriaItemResponse();
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }
    }
}
