using AcmeBookStoreWASM.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace AcmeBookStoreWASM.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AcmeBookStoreWASMEntityFrameworkCoreModule),
        typeof(AcmeBookStoreWASMApplicationContractsModule)
        )]
    public class AcmeBookStoreWASMDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
