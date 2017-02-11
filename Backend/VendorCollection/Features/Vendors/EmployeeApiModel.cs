using System;
using VendorCollection.Data.Models;

namespace VendorCollection.Features.Vendors
{
    public class EmployeeApiModel
    {        
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public virtual string Position { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }

        public static TModel FromEmployee<TModel>(Employee employee) where
            TModel : EmployeeApiModel, new()
        {
            var model = new TModel();
            model.Id = employee.Id;
            model.Firstname = employee.Firstname;
            model.Lastname = employee.Lastname;
            model.LastModifiedBy = employee.LastModifiedBy;
            model.LastModifiedOn = employee.LastModifiedOn;
            model.CreatedBy = employee.CreatedBy;
            model.CreatedOn = employee.CreatedOn;
            return model;
        }

        public static EmployeeApiModel FromEmployee(Employee employee)
            => FromEmployee<EmployeeApiModel>(employee);

    }
}
