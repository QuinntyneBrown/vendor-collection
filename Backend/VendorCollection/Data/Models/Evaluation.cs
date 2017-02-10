using System;
using System.Collections.Generic;

namespace VendorCollection.Data.Models
{
    public class Evaluation: ILoggable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<EvaluationCriteriaItem> Criteria { get; set; } = new HashSet<EvaluationCriteriaItem>();
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}