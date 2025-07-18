using HelloWorld.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace HelloWorld.Permissions;

public class HelloWorldPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(HelloWorldPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(HelloWorldPermissions.MyPermission1, L("Permission:MyPermission1"));
        var parentRequest = myGroup.AddPermission(HelloWorldPermissions.ParentRequest);
        parentRequest.AddChild(HelloWorldPermissions.RequestRead);
        parentRequest.AddChild(HelloWorldPermissions.RequestManager);
        parentRequest.AddChild(HelloWorldPermissions.TestManager);
        parentRequest.AddChild(HelloWorldPermissions.TestManager2);
        parentRequest.AddChild(HelloWorldPermissions.TestManager3);
        parentRequest.AddChild(HelloWorldPermissions.TestManager4);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HelloWorldResource>(name);
    }
}
