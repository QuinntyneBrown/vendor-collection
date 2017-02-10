using System.Data.Entity;

namespace VendorCollection.Data
{
    public interface IVendorCollectionDataContext
    {

    }

    public class VendorCollectionDataContext: DbContext, IVendorCollectionDataContext
    {
        public VendorCollectionDataContext()
            : base(nameOrConnectionString: "VendorCollectionDataContext")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = true;
        }

        public DbSet<Models.Vendor> Vendors { get; set; }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Role> Roles { get; set; }
        public DbSet<Models.Evaluation> Evaluations { get; set; }
        public DbSet<Models.EvaluationCriteriaItem> EvaluationCriteriaItems { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        } 
    }
}
