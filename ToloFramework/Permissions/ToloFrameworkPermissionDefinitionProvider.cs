using ToloFramework.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace ToloFramework.Permissions;

public class ToloFrameworkPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ToloFrameworkPermissions.GroupName);

        var booksPermission = myGroup.AddPermission(ToloFrameworkPermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(ToloFrameworkPermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(ToloFrameworkPermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(ToloFrameworkPermissions.Books.Delete, L("Permission:Books.Delete"));
        
        //Define your own permissions here. Example:
        //myGroup.AddPermission(ToloFrameworkPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ToloFrameworkResource>(name);
    }
}
