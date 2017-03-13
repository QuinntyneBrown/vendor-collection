using VendorCollection.Data.Model;
using VendorCollection.Features.SelectionCriterion;

namespace VendorCollection.Features.Vendors
{
    public class VendorSelectionCriteriaApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public VendorApiModel Vendor { get; set; }
        public SelectionCriteriaApiModel SelectionCriteria { get; set; }

        public static TModel FromVendorSelectionCriteria<TModel>(VendorSelectionCriteria vendorSelectionCriteria) where
            TModel : VendorSelectionCriteriaApiModel, new()
        {
            var model = new TModel();
            model.Id = vendorSelectionCriteria.Id;
            model.SelectionCriteria = SelectionCriteriaApiModel.FromSelectionCriteria(vendorSelectionCriteria.SelectionCriteria);
            return model;
        }

        public static VendorSelectionCriteriaApiModel FromVendorSelectionCriteria(VendorSelectionCriteria vendorSelectionCriteria)
            => FromVendorSelectionCriteria<VendorSelectionCriteriaApiModel>(vendorSelectionCriteria);
    }
}
