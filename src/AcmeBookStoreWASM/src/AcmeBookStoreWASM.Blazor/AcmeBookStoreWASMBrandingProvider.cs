using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace AcmeBookStoreWASM.Blazor
{
    [Dependency(ReplaceServices = true)]
    public class AcmeBookStoreWASMBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "AcmeBookStoreWASM";
    }
}
