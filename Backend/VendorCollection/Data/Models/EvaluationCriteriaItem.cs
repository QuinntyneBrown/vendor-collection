using System.Collections.Generic;

namespace VendorCollection.Data.Models
{
    public class EvaluationCriteriaItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public bool IsDeleted { get; set; }
    }
}
