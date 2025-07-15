using Xunit;

namespace HelloWorld.EntityFrameworkCore;

[CollectionDefinition(HelloWorldTestConsts.CollectionDefinitionName)]
public class HelloWorldEntityFrameworkCoreCollection : ICollectionFixture<HelloWorldEntityFrameworkCoreFixture>
{

}
