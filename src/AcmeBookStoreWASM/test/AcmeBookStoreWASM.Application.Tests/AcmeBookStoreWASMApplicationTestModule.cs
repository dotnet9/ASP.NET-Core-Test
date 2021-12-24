using Volo.Abp.Modularity;

namespace AcmeBookStoreWASM
{
    [DependsOn(
        typeof(AcmeBookStoreWASMApplicationModule),
        typeof(AcmeBookStoreWASMDomainTestModule)
        )]
    public class AcmeBookStoreWASMApplicationTestModule : AbpModule
    {

    }
}