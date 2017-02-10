using System;
using System.Collections.Generic;

namespace VendorCollection.Data.Models
{
    public class VendorEvaluationCriteriaItem: ILoggable
    {
        public int Id { get; set; }
        public decimal Rating { get; set; }
        public int? VendorEvaluationId  { get; set; }
        public int? EvaluationCriteriaItemId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public EvaluationCriteriaItem EvaluationCriteriaItem { get; set; }
        public VendorEvaluation VendorEvaluation { get; set; }
        public bool IsDeleted { get; set; }
    }
}
