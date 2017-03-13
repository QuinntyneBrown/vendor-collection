using VendorCollection.Data.Model;

namespace VendorCollection.Features.Documents
{
    public class DocumentApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromDocument<TModel>(Document document) where
            TModel : DocumentApiModel, new()
        {
            var model = new TModel();
            model.Id = document.Id;
            model.TenantId = document.TenantId;
            model.Name = document.Name;
            return model;
        }

        public static DocumentApiModel FromDocument(Document document)
            => FromDocument<DocumentApiModel>(document);

    }
}
