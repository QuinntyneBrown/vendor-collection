using VendorCollection.Data.Models;

namespace VendorCollection.Features.Evaluations
{
    public class EvaluationApiModel
    {        
        public int Id { get; set; }
        public string Name { get; set; }

        public static TModel FromEvaluation<TModel>(Evaluation evaluation) where
            TModel : EvaluationApiModel, new()
        {
            var model = new TModel();
            model.Id = evaluation.Id;
            return model;
        }

        public static EvaluationApiModel FromEvaluation(Evaluation evaluation)
            => FromEvaluation<EvaluationApiModel>(evaluation);

    }
}
