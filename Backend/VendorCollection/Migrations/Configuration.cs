namespace VendorCollection.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<VendorCollection.Data.VendorCollectionDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(VendorCollection.Data.VendorCollectionDataContext context)
        {

        }
    }
}
