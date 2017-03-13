using System;
using VendorCollection.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendorCollection.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class VendorSelectionCriteria: ILoggable
    {
        public int Id { get; set; }

        [ForeignKey("Vendor")]
        public int? VendorId { get; set; }

        [ForeignKey("SelectionCriteria")]
        public int? SelectionCriteriaId { get; set; }

        public float? Rating { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModifiedOn { get; set; }

        public string CreatedBy { get; set; }

        public string LastModifiedBy { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Vendor Vendor { get; set; }

        public virtual SelectionCriteria SelectionCriteria  { get; set; }
    }
}
