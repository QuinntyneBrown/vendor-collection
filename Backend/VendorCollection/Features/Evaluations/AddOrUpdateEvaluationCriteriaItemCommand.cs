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
    public class AddOrUpdateEvaluationCriteriaItemCommand
    {
        public class AddOrUpdateEvaluationCriteriaItemRequest : IRequest<AddOrUpdateEvaluationCriteriaItemResponse>
        {
            public EvaluationCriteriaItemApiModel EvaluationCriteriaItem { get; set; }
        }

        public class AddOrUpdateEvaluationCriteriaItemResponse
        {

        }

        public class AddOrUpdateEvaluationCriteriaItemHandler : IAsyncRequestHandler<AddOrUpdateEvaluationCriteriaItemRequest, AddOrUpdateEvaluationCriteriaItemResponse>
        {
            public AddOrUpdateEvaluationCriteriaItemHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<AddOrUpdateEvaluationCriteriaItemResponse> Handle(AddOrUpdateEvaluationCriteriaItemRequest request)
            {
                var entity = await _dataContext.EvaluationCriteriaItems
                    .SingleOrDefaultAsync(x => x.Id == request.EvaluationCriteriaItem.Id && x.IsDeleted == false);
                if (entity == null) _dataContext.EvaluationCriteriaItems.Add(entity = new EvaluationCriteriaItem());
                entity.Name = request.EvaluationCriteriaItem.Name;
                await _dataContext.SaveChangesAsync();

                return new AddOrUpdateEvaluationCriteriaItemResponse()
                {

                };
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
