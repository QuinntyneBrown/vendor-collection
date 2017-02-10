using VendorCollection.Data.Models;

namespace VendorCollection.Features.VendorEvaluations
{
    public class VendorEvaluationCriteriaItemApiModel
    {        
        public int Id { get; set; }
        public decimal Rating { get; set; }

        public static TModel FromVendorEvaluationCriteriaItem<TModel>(VendorEvaluationCriteriaItem vendorEvaluationCriteriaItem) where
            TModel : VendorEvaluationCriteriaItemApiModel, new()
        {
            var model = new TModel();
            model.Id = vendorEvaluationCriteriaItem.Id;
            return model;
        }

        public static VendorEvaluationCriteriaItemApiModel FromVendorEvaluationCriteriaItem(VendorEvaluationCriteriaItem vendorEvaluationCriteriaItem)
            => FromVendorEvaluationCriteriaItem<VendorEvaluationCriteriaItemApiModel>(vendorEvaluationCriteriaItem);

    }
}
