using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SySSensor.Core.Entities;

namespace SySSensor.Core.DAL
{
    public class SySDB : DbContext
    {
        public SySDB() : base("Repository")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<SySDB>());
        }

        public DbSet<SensorLog> SensorLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
