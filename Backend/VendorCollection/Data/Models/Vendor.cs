using System.Collections.Generic;

namespace VendorCollection.Data.Models
{
    public class Vendor: ILoggable
    {
        public int Id { get; set; }
        public string CompanyName { get;set; }
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
        public bool IsDeleted { get; set; }
        
    }
}
