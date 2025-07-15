using Volo.Abp.Modularity;

namespace HelloWorld;

[DependsOn(
    typeof(HelloWorldDomainModule),
    typeof(HelloWorldTestBaseModule)
)]
public class HelloWorldDomainTestModule : AbpModule
{

}
