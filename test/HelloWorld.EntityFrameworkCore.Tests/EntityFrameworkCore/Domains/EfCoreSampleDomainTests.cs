using HelloWorld.Samples;
using Xunit;

namespace HelloWorld.EntityFrameworkCore.Domains;

[Collection(HelloWorldTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<HelloWorldEntityFrameworkCoreTestModule>
{

}
