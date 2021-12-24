using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace AcmeBookStoreWASM.Data
{
    /* This is used if database provider does't define
     * IAcmeBookStoreWASMDbSchemaMigrator implementation.
     */
    public class NullAcmeBookStoreWASMDbSchemaMigrator : IAcmeBookStoreWASMDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}