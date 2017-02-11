using System;
using System.Collections.Generic;
using VendorCollection.Data.Models;
using System.Linq;

namespace VendorCollection.Features.Vendors
{
    public class VendorApiModel
    {        
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public ICollection<EmployeeApiModel> Employees { get; set; } = new HashSet<EmployeeApiModel>();

        public static TModel FromVendor<TModel>(Vendor vendor) where
            TModel : VendorApiModel, new()
        {
            var model = new TModel();
            model.Id = vendor.Id;
            model.LastModifiedBy = vendor.LastModifiedBy;
            model.LastModifiedOn = vendor.LastModifiedOn;
            model.CreatedBy = vendor.CreatedBy;
            model.CreatedOn = vendor.CreatedOn;
            model.Employees = vendor.Employees.Select(x => EmployeeApiModel.FromEmployee(x)).ToList();
            return model;
        }

        public static VendorApiModel FromVendor(Vendor vendor)
            => FromVendor<VendorApiModel>(vendor);

    }
}
