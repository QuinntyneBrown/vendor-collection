using System.Data.Entity;
using VendorCollection.Data.Models;

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

        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<EvaluationCriteriaItem> EvaluationCriteriaItems { get; set; }
        public DbSet<VendorEvaluation> VendorEvaluations { get; set; }
        public DbSet<VendorEvaluationCriteriaItem> VendorEvaluationCriteriaItems { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        } 
    }
}
