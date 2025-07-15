using HelloWorld.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace HelloWorld.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(HelloWorldEntityFrameworkCoreModule),
    typeof(HelloWorldApplicationContractsModule)
    )]
public class HelloWorldDbMigratorModule : AbpModule
{
}
