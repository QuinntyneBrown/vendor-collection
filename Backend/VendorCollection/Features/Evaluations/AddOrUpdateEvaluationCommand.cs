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
    public class AddOrUpdateEvaluationCommand
    {
        public class AddOrUpdateEvaluationRequest : IRequest<AddOrUpdateEvaluationResponse>
        {
            public EvaluationApiModel Evaluation { get; set; }
        }

        public class AddOrUpdateEvaluationResponse
        {

        }

        public class AddOrUpdateEvaluationHandler : IAsyncRequestHandler<AddOrUpdateEvaluationRequest, AddOrUpdateEvaluationResponse>
        {
            public AddOrUpdateEvaluationHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<AddOrUpdateEvaluationResponse> Handle(AddOrUpdateEvaluationRequest request)
            {
                var entity = await _dataContext.Evaluations
                    .SingleOrDefaultAsync(x => x.Id == request.Evaluation.Id && x.IsDeleted == false);
                if (entity == null) _dataContext.Evaluations.Add(entity = new Evaluation());
                entity.Name = request.Evaluation.Name;
                await _dataContext.SaveChangesAsync();

                return new AddOrUpdateEvaluationResponse()
                {

                };
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
