using VendorCollection.Data.Model;

namespace VendorCollection.Features.Vendors
{
    public class VendorApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromVendor<TModel>(Vendor vendor) where
            TModel : VendorApiModel, new()
        {
            var model = new TModel();
            model.Id = vendor.Id;
            model.TenantId = vendor.TenantId;
            return model;
        }

        public static VendorApiModel FromVendor(Vendor vendor)
            => FromVendor<VendorApiModel>(vendor);

    }
}
