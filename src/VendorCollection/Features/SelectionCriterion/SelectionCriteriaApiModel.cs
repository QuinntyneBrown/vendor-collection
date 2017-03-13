using VendorCollection.Data.Model;

namespace VendorCollection.Features.SelectionCriterion
{
    public class SelectionCriteriaApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromSelectionCriteria<TModel>(SelectionCriteria selectionCriteria) where
            TModel : SelectionCriteriaApiModel, new()
        {
            var model = new TModel();
            model.Id = selectionCriteria.Id;
            model.TenantId = selectionCriteria.TenantId;
            model.Name = selectionCriteria.Name;
            return model;
        }

        public static SelectionCriteriaApiModel FromSelectionCriteria(SelectionCriteria selectionCriteria)
            => FromSelectionCriteria<SelectionCriteriaApiModel>(selectionCriteria);

    }
}
