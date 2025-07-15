using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace HelloWorld.Data;

/* This is used if database provider does't define
 * IHelloWorldDbSchemaMigrator implementation.
 */
public class NullHelloWorldDbSchemaMigrator : IHelloWorldDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
