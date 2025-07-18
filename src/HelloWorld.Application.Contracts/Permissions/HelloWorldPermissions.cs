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
    public const string TestManager2 = ParentRequest + ".Test2";
    public const string TestManager3 = ParentRequest + ".Test3";
    public const string TestManager4 = ParentRequest + ".Test4";
    public const string TestManager5 = ParentRequest + ".Test5";
}
