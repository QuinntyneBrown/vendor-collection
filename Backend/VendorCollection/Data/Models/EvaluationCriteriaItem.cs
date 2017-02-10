using System;
using System.Collections.Generic;

namespace VendorCollection.Data.Models
{
    public class EvaluationCriteriaItem: ILoggable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
