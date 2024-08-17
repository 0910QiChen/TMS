using DomainLayer.DomainModels;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace RepositoryLayer.Contexts
{
    public class SystemContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Quotes> Quotes { get; set; }

        public SystemContext() : base("SystemContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
        }
    }
}