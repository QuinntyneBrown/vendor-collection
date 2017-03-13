using System;
using VendorCollection.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static VendorCollection.Constants;
using System.Collections.Generic;

namespace VendorCollection.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Vendor: ILoggable
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Tenant")]
        public int? TenantId { get; set; }

        [Index("NameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(MaxStringLength)]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModifiedOn { get; set; }

        public string CreatedBy { get; set; }

        public string LastModifiedBy { get; set; }

        public bool IsDeleted { get; set; }

		public virtual Tenant Tenant { get; set; }

        public ICollection<VendorSelectionCriteria> VendorSelectionCriterion { get; set; } = new HashSet<VendorSelectionCriteria>();
        public ICollection<VendorDocument> VendorDocuments { get; set; } = new HashSet<VendorDocument>();
    }
}
