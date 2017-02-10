using VendorCollection.Data.Models;

namespace VendorCollection.Features.VendorEvaluations
{
    public class VendorEvaluationApiModel
    {        
        public int Id { get; set; }
        public string Name { get; set; }

        public static TModel FromVendorEvaluation<TModel>(VendorEvaluation vendorEvaluation) where
            TModel : VendorEvaluationApiModel, new()
        {
            var model = new TModel();
            model.Id = vendorEvaluation.Id;
            return model;
        }

        public static VendorEvaluationApiModel FromVendorEvaluation(VendorEvaluation vendorEvaluation)
            => FromVendorEvaluation<VendorEvaluationApiModel>(vendorEvaluation);

    }
}
