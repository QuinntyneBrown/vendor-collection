using System;
using VendorCollection.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendorCollection.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Contact: ILoggable
    {
        public int Id { get; set; }

        [ForeignKey("Tenant")]
        public int? TenantId { get; set; }

        [ForeignKey("Vendor")]
        public int? VendorId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Title { get; set; }

        public string Email { get; set; }

        public string Twitter { get; set; }

        public string LinkedIn { get; set; }

        public string Mobile { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModifiedOn { get; set; }

        public string CreatedBy { get; set; }

        public string LastModifiedBy { get; set; }

        public bool IsDeleted { get; set; }

 		public virtual Tenant Tenant { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
