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
    public class AddOrUpdateVendorEvaluationCommand
    {
        public class AddOrUpdateVendorEvaluationRequest : IRequest<AddOrUpdateVendorEvaluationResponse>
        {
            public VendorEvaluationApiModel VendorEvaluation { get; set; }
        }

        public class AddOrUpdateVendorEvaluationResponse
        {

        }

        public class AddOrUpdateVendorEvaluationHandler : IAsyncRequestHandler<AddOrUpdateVendorEvaluationRequest, AddOrUpdateVendorEvaluationResponse>
        {
            public AddOrUpdateVendorEvaluationHandler(VendorCollectionDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<AddOrUpdateVendorEvaluationResponse> Handle(AddOrUpdateVendorEvaluationRequest request)
            {
                var entity = await _dataContext.VendorEvaluations
                    .SingleOrDefaultAsync(x => x.Id == request.VendorEvaluation.Id && x.IsDeleted == false);
                if (entity == null) _dataContext.VendorEvaluations.Add(entity = new VendorEvaluation());
                entity.Name = request.VendorEvaluation.Name;
                await _dataContext.SaveChangesAsync();

                return new AddOrUpdateVendorEvaluationResponse()
                {

                };
            }

            private readonly VendorCollectionDataContext _dataContext;
            private readonly ICache _cache;
        }

    }

}
