using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace AcmeBookStoreWASM
{
    [Dependency(ReplaceServices = true)]
    public class AcmeBookStoreWASMBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "AcmeBookStoreWASM";
    }
}
