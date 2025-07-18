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

    public const string GroupName2 = "Test";
    public const string Default2 = GroupName2;
    public const string ParentRequest2 = Default2 + ".Request";
    public const string RequestRead2 = ParentRequest2 + ".Read";
    public const string RequestManager2 = ParentRequest2 + ".Manager";
    public const string TestManager2 = ParentRequest2 + ".Test";
    public const string TargetManager2 = ParentRequest2 + ".Target";

    public const string GroupName3 = "World";
    public const string Default3 = GroupName3;
    public const string ParentRequest3 = Default3 + ".Request";
    public const string RequestRead3 = ParentRequest3 + ".Read";
    public const string RequestManager3 = ParentRequest3 + ".Manager";
    public const string TestManager3 = ParentRequest3 + ".Test";
    public const string TargetManager3 = ParentRequest3 + ".Target";
}
