using AcmeBookStoreWASM.Localization;
using Volo.Abp.AspNetCore.Components;

namespace AcmeBookStoreWASM.Blazor
{
    public abstract class AcmeBookStoreWASMComponentBase : AbpComponentBase
    {
        protected AcmeBookStoreWASMComponentBase()
        {
            LocalizationResource = typeof(AcmeBookStoreWASMResource);
        }
    }
}
