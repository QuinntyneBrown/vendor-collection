using System.Collections.Generic;

namespace VendorCollection.Data.Models
{
    public class Developer: Employee
    {
        public override string Position { get; set; } = "Developer";
    }
}
