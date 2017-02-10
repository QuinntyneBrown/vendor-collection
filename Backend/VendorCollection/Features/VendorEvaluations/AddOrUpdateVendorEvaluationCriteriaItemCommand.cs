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
    public class AddOrUpdateVendorEvaluationCriteriaItemCommand
    {
        public class AddOrUpdateVendorEvaluationCriteriaItemRequest : IRequest<AddOrUpdateVendorEvaluationCriteriaItemResponse>
        {
            public VendorEvaluationCriteriaItemApiModel VendorEvaluationCriteriaItem { get; set; }
        }

        public class AddOrUpdateVendorEvaluationCriteriaItemResponse
        {

        }

        public class AddOrUpdateVendorEvaluationCriteriaItemHandler : IAsyncRequestHandler<AddOrUpdateVendorEvaluationCriteriaItemRequest, AddOrUpdateVendorEvaluationCriteriaItemResponse>
        {
            public AddOrUpdateVendorEvaluationCriteriaItemHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<AddOrUpdateVendorEvaluationCriteriaItemResponse> Handle(AddOrUpdateVendorEvaluationCriteriaItemRequest request)
            {
                var entity = await _dataContext.VendorEvaluationCriteriaItems
                    .SingleOrDefaultAsync(x => x.Id == request.VendorEvaluationCriteriaItem.Id && x.IsDeleted == false);
                if (entity == null) _dataContext.VendorEvaluationCriteriaItems.Add(entity = new VendorEvaluationCriteriaItem());
                entity.Rating = request.VendorEvaluationCriteriaItem.Rating;
                await _dataContext.SaveChangesAsync();

                return new AddOrUpdateVendorEvaluationCriteriaItemResponse()
                {

                };
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
