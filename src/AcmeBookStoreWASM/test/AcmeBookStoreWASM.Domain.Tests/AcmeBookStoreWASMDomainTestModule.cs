using AcmeBookStoreWASM.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace AcmeBookStoreWASM
{
    [DependsOn(
        typeof(AcmeBookStoreWASMEntityFrameworkCoreTestModule)
        )]
    public class AcmeBookStoreWASMDomainTestModule : AbpModule
    {

    }
}