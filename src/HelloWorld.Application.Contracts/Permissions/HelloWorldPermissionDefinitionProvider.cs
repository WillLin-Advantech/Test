﻿using HelloWorld.Localization;
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
        parentRequest.AddChild(HelloWorldPermissions.TargetManager);
        parentRequest.AddChild(HelloWorldPermissions.WriteManager);
        parentRequest.AddChild(HelloWorldPermissions.DeleteManager);

        var myGroup2 = context.AddGroup(HelloWorldPermissions.GroupName2);
        var parentRequest2 = myGroup2.AddPermission(HelloWorldPermissions.ParentRequest2);
        parentRequest2.AddChild(HelloWorldPermissions.RequestRead2);
        parentRequest2.AddChild(HelloWorldPermissions.RequestManager2);
        parentRequest2.AddChild(HelloWorldPermissions.TestManager2);
        parentRequest2.AddChild(HelloWorldPermissions.TargetManager2);

        var myGrou3 = context.AddGroup(HelloWorldPermissions.GroupName3);
        var parentRequest3 = myGrou3.AddPermission(HelloWorldPermissions.ParentRequest3);
        parentRequest3.AddChild(HelloWorldPermissions.RequestRead3);
        parentRequest3.AddChild(HelloWorldPermissions.RequestManager3);
        parentRequest3.AddChild(HelloWorldPermissions.TestManager3);

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HelloWorldResource>(name);
    }
}
