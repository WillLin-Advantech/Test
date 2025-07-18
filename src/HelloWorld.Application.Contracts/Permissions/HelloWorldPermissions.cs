namespace HelloWorld.Permissions;

public static class HelloWorldPermissions
{
    public const string GroupName = "HelloWorld";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
    public const string Default = GroupName;
    public const string ParentRequest = Default + ".Request";
    public const string RequestRead = ParentRequest + ".Read";
    public const string RequestManager = ParentRequest + ".Manager";
    public const string TestManager = ParentRequest + ".Test";
    public const string TargetManager = ParentRequest + ".Target";
    public const string WriteManager = ParentRequest + ".Write";
    public const string DeleteManager = ParentRequest + ".Delete";
}
