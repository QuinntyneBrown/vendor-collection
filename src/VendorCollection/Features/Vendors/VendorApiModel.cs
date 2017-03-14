using System.Collections.Generic;
using VendorCollection.Data.Model;
using System.Linq;
using VendorCollection.Features.Contacts;

namespace VendorCollection.Features.Vendors
{
    public class VendorApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }
        public ICollection<VendorDocumentApiModel> VendorDocuments { get; set; } = new HashSet<VendorDocumentApiModel>();
        public ICollection<VendorSelectionCriteriaApiModel> VendorSelectionCriterion { get; set; } = new HashSet<VendorSelectionCriteriaApiModel>();
        public ICollection<ContactApiModel> Contacts { get; set; } = new HashSet<ContactApiModel>();
        public static TModel FromVendor<TModel>(Vendor vendor) where
            TModel : VendorApiModel, new()
        {
            var model = new TModel();
            model.Id = vendor.Id;
            model.TenantId = vendor.TenantId;
            model.Name = vendor.Name;
            model.VendorDocuments = vendor.VendorDocuments.Select(x => VendorDocumentApiModel.FromVendorDocument(x)).ToList();
            model.VendorSelectionCriterion = vendor.VendorSelectionCriterion.Select(x => VendorSelectionCriteriaApiModel.FromVendorSelectionCriteria(x)).ToList();
            model.Contacts = vendor.Contacts.Select(x => ContactApiModel.FromContact(x)).ToList();

            return model;
        }

        public static VendorApiModel FromVendor(Vendor vendor)
            => FromVendor<VendorApiModel>(vendor);
    }
}
