using VendorCollection.Data.Models;

namespace VendorCollection.Features.Evaluations
{
    public class EvaluationCriteriaItemApiModel
    {        
        public int Id { get; set; }
        public string Name { get; set; }

        public static TModel FromEvaluationCriteriaItem<TModel>(EvaluationCriteriaItem evaluationCriteriaItem) where
            TModel : EvaluationCriteriaItemApiModel, new()
        {
            var model = new TModel();
            model.Id = evaluationCriteriaItem.Id;
            return model;
        }

        public static EvaluationCriteriaItemApiModel FromEvaluationCriteriaItem(EvaluationCriteriaItem evaluationCriteriaItem)
            => FromEvaluationCriteriaItem<EvaluationCriteriaItemApiModel>(evaluationCriteriaItem);

    }
}
