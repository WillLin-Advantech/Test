using Volo.Abp.Modularity;

namespace HelloWorld;

public abstract class HelloWorldApplicationTestBase<TStartupModule> : HelloWorldTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
