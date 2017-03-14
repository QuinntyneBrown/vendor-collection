using VendorCollection.Data.Model;

namespace VendorCollection.Features.Contacts
{
    public class ContactApiModel
    {        
        public int Id { get; set; }

        public int? TenantId { get; set; }

        public int? VendorId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Title { get; set; }

        public string Email { get; set; }

        public string Twitter { get; set; }

        public string LinkedIn { get; set; }

        public string Mobile { get; set; }

        public string PhoneNumber { get; set; }
        

        public static TModel FromContact<TModel>(Contact contact) where
            TModel : ContactApiModel, new()
        {
            var model = new TModel();
            model.Id = contact.Id;
            model.TenantId = contact.TenantId;
            model.Firstname = contact.Firstname;
            model.Lastname = contact.Lastname;
            model.Title = contact.Title;
            model.Email = contact.Email;
            model.Twitter = contact.Twitter;
            model.LinkedIn = contact.LinkedIn;
            model.Mobile = contact.Mobile;
            model.PhoneNumber = contact.PhoneNumber;
            return model;
        }

        public static ContactApiModel FromContact(Contact contact)
            => FromContact<ContactApiModel>(contact);

    }
}
