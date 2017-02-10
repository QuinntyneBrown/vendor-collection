using System.Collections.Generic;

namespace VendorCollection.Data.Models
{
    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
