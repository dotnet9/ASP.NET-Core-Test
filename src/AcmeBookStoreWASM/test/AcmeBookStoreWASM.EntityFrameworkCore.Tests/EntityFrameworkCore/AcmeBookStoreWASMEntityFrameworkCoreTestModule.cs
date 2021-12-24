﻿using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.Modularity;

namespace AcmeBookStoreWASM.EntityFrameworkCore
{
    [DependsOn(
        typeof(AcmeBookStoreWASMEntityFrameworkCoreModule),
        typeof(AcmeBookStoreWASMTestBaseModule),
        typeof(AbpEntityFrameworkCoreSqliteModule)
        )]
    public class AcmeBookStoreWASMEntityFrameworkCoreTestModule : AbpModule
    {
        private SqliteConnection _sqliteConnection;

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureInMemorySqlite(context.Services);
        }

        private void ConfigureInMemorySqlite(IServiceCollection services)
        {
            _sqliteConnection = CreateDatabaseAndGetConnection();

            services.Configure<AbpDbContextOptions>(options =>
            {
                options.Configure(context =>
                {
                    context.DbContextOptions.UseSqlite(_sqliteConnection);
                });
            });
        }

        public override void OnApplicationShutdown(ApplicationShutdownContext context)
        {
            _sqliteConnection.Dispose();
        }

        private static SqliteConnection CreateDatabaseAndGetConnection()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<AcmeBookStoreWASMDbContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new AcmeBookStoreWASMDbContext(options))
            {
                context.GetService<IRelationalDatabaseCreator>().CreateTables();
            }

            return connection;
        }
    }
}
