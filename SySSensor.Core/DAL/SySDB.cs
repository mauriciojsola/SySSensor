using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SySSensor.Core.Entities;

namespace SySSensor.Core.DAL
{
    /// <summary>
    /// http://whiteknight.github.io/2013/01/26/efcodeonlymigrations.html
    /// </summary>
    public class SySDB : DbContext
    {
        public SySDB() : base("Repository")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<SySDB>());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<SySDB, Migrations.MigrationsConfiguration>());
            //var migrator = new DbMigrator(new Migrations.MigrationsConfiguration());
            //migrator.Update();
        }

        public DbSet<SensorLog> SensorLogs { get; set; }
        public DbSet<LogFile> LogFiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }



    }
}
