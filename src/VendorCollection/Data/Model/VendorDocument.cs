using System;
using VendorCollection.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendorCollection.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class VendorDocument: ILoggable
    {
        public int Id { get; set; }

        [ForeignKey("Tenant")]
        public int? TenantId { get; set; }

        [ForeignKey("Document")]
        public int? DocumentId { get; set; }

        [ForeignKey("Vendor")]
        public int? VendorId { get; set; }

        public DateTime? CompletionDate { get; set; }

        [Index("NameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }

		public virtual Tenant Tenant { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual Document Document { get; set; }
    }
}
