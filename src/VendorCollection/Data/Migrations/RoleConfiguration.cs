using System.Data.Entity.Migrations;
using VendorCollection.Data;
using VendorCollection.Data.Model;
using VendorCollection.Features.Users;

namespace VendorCollection.Migrations
{
    public class RoleConfiguration
    {
        public static void Seed(VendorCollectionContext context) {

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.SYSTEM
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.PRODUCT
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.DEVELOPMENT
            });

            context.SaveChanges();
        }
    }
}
