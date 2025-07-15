using Volo.Abp.Modularity;

namespace HelloWorld;

[DependsOn(
    typeof(HelloWorldApplicationModule),
    typeof(HelloWorldDomainTestModule)
)]
public class HelloWorldApplicationTestModule : AbpModule
{

}
