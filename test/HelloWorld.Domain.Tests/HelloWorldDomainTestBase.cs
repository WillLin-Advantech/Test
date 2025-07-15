using Volo.Abp.Modularity;

namespace HelloWorld;

/* Inherit from this class for your domain layer tests. */
public abstract class HelloWorldDomainTestBase<TStartupModule> : HelloWorldTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
