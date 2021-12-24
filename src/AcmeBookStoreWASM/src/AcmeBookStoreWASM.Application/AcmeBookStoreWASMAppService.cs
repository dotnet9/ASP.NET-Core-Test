using System;
using System.Collections.Generic;
using System.Text;
using AcmeBookStoreWASM.Localization;
using Volo.Abp.Application.Services;

namespace AcmeBookStoreWASM
{
    /* Inherit your application services from this class.
     */
    public abstract class AcmeBookStoreWASMAppService : ApplicationService
    {
        protected AcmeBookStoreWASMAppService()
        {
            LocalizationResource = typeof(AcmeBookStoreWASMResource);
        }
    }
}
