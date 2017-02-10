using System.Collections.Generic;

namespace VendorCollection.Data.Models
{
    public class VendorEvaluationCriteriaItem
    {
        public int Id { get; set; }
        public decimal Rating { get; set; }
        public int? VendorEvaluationId  { get; set; }
        public int? EvaluationCriteriaItemId { get; set; }
        public EvaluationCriteriaItem EvaluationCriteriaItem { get; set; }
        public VendorEvaluation VendorEvaluation { get; set; }
        public bool IsDeleted { get; set; }
    }
}
