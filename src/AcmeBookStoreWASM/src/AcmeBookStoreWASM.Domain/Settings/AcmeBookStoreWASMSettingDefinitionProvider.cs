using Volo.Abp.Settings;

namespace AcmeBookStoreWASM.Settings
{
    public class AcmeBookStoreWASMSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(AcmeBookStoreWASMSettings.MySetting1));
        }
    }
}
