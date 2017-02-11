using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendorCollection.Data.Models
{
    public class VendorEvaluation: ILoggable
    {
        public int Id { get; set; }
        [ForeignKey("Vendor")]
        public int? VendorId { get; set; }
        [ForeignKey("Evaluation")]
        public int? EvaluationId { get; set; }        
        public Vendor Vendor { get; set; }
        public Evaluation Evaluation { get; set; }
        public ICollection<VendorEvaluationCriteriaItem> Criteria { get; set; } = new HashSet<VendorEvaluationCriteriaItem>();
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
