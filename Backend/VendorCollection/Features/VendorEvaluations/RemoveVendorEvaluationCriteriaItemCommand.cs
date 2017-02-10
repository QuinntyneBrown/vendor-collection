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
    public class RemoveVendorEvaluationCriteriaItemCommand
    {
        public class RemoveVendorEvaluationCriteriaItemRequest : IRequest<RemoveVendorEvaluationCriteriaItemResponse>
        {
            public int Id { get; set; }
        }

        public class RemoveVendorEvaluationCriteriaItemResponse { }

        public class RemoveVendorEvaluationCriteriaItemHandler : IAsyncRequestHandler<RemoveVendorEvaluationCriteriaItemRequest, RemoveVendorEvaluationCriteriaItemResponse>
        {
            public RemoveVendorEvaluationCriteriaItemHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<RemoveVendorEvaluationCriteriaItemResponse> Handle(RemoveVendorEvaluationCriteriaItemRequest request)
            {
                var vendorEvaluationCriteriaItem = await _dataContext.VendorEvaluationCriteriaItems.FindAsync(request.Id);
                vendorEvaluationCriteriaItem.IsDeleted = true;
                await _dataContext.SaveChangesAsync();
                return new RemoveVendorEvaluationCriteriaItemResponse();
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }
    }
}
