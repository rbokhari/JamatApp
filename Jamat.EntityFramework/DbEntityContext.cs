using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Jamat.EntityFramework
{
    public class DbEntityContext : DbContext
    {
        public DbEntityContext() :base("JamatConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<DbEntityContext, JamatMigrationConfiguration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Validation> Validations { get; set; }
        public DbSet<ValidationDetail> ValidationDetails { get; set; }

        public DbSet<Tajneed> Tajneeds { get; set; }

        public DbSet<TajneedIncome> TajneedIncomes { get; set; }

        public DbSet<TajneedCard> TajneedCards { get; set; }

        public DbSet<Chanda> Chandas { get; set; }

        public DbSet<ChandaDetail> ChandaDetails { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Region> Regions { get; set; }


    }
}
