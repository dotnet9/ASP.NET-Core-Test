using AcmeBookStoreWASM.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace AcmeBookStoreWASM.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class AcmeBookStoreWASMController : AbpControllerBase
    {
        protected AcmeBookStoreWASMController()
        {
            LocalizationResource = typeof(AcmeBookStoreWASMResource);
        }
    }
}