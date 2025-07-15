using HelloWorld.Samples;
using Xunit;

namespace HelloWorld.EntityFrameworkCore.Applications;

[Collection(HelloWorldTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<HelloWorldEntityFrameworkCoreTestModule>
{

}
