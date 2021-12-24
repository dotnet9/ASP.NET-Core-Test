using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AcmeBookStoreWASM.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class AcmeBookStoreWASMDbContextFactory : IDesignTimeDbContextFactory<AcmeBookStoreWASMDbContext>
    {
        public AcmeBookStoreWASMDbContext CreateDbContext(string[] args)
        {
            AcmeBookStoreWASMEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<AcmeBookStoreWASMDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new AcmeBookStoreWASMDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../AcmeBookStoreWASM.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
