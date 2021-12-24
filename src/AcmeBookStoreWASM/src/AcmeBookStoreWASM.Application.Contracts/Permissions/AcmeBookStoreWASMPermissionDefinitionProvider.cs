using AcmeBookStoreWASM.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace AcmeBookStoreWASM.Permissions
{
    public class AcmeBookStoreWASMPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(AcmeBookStoreWASMPermissions.GroupName);
            //Define your own permissions here. Example:
            //myGroup.AddPermission(AcmeBookStoreWASMPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AcmeBookStoreWASMResource>(name);
        }
    }
}
