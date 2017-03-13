using System.Data.Entity.Migrations;
using VendorCollection.Data;
using VendorCollection.Data.Model;

namespace VendorCollection.Migrations
{
    public class TenantConfiguration
    {
        public static void Seed(VendorCollectionContext context) {

            context.Tenants.AddOrUpdate(x => x.Name, new Tenant()
            {
                Name = "Default"
            });

            context.SaveChanges();
        }
    }
}
