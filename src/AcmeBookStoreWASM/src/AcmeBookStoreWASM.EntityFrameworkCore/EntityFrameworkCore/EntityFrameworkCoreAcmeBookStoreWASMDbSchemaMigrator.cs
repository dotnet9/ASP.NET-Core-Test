using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AcmeBookStoreWASM.Data;
using Volo.Abp.DependencyInjection;

namespace AcmeBookStoreWASM.EntityFrameworkCore
{
    public class EntityFrameworkCoreAcmeBookStoreWASMDbSchemaMigrator
        : IAcmeBookStoreWASMDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreAcmeBookStoreWASMDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the AcmeBookStoreWASMDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<AcmeBookStoreWASMDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
