
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace HelloWorld.InfraStructure;

[DependsOn(
    typeof(HelloWorldDomainModule)
)]
public class HelloWorldInfraStructureModule : AbpModule
{

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}