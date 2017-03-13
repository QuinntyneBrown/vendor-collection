using VendorCollection.Data.Model;
using VendorCollection.Features.Documents;

namespace VendorCollection.Features.Vendors
{
    public class VendorDocumentApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }
        public DocumentApiModel Document { get; set; }
        
        public static TModel FromVendorDocument<TModel>(VendorDocument vendorDocument) where
            TModel : VendorDocumentApiModel, new()
        {
            var model = new TModel();
            model.Id = vendorDocument.Id;
            model.TenantId = vendorDocument.TenantId;
            model.Document = DocumentApiModel.FromDocument(vendorDocument.Document);
            return model;
        }

        public static VendorDocumentApiModel FromVendorDocument(VendorDocument vendorDocument)
            => FromVendorDocument<VendorDocumentApiModel>(vendorDocument);

    }
}
