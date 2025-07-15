using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HelloWorld.Data;
using Volo.Abp.DependencyInjection;

namespace HelloWorld.EntityFrameworkCore;

public class EntityFrameworkCoreHelloWorldDbSchemaMigrator
    : IHelloWorldDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreHelloWorldDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the HelloWorldDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<HelloWorldDbContext>()
            .Database
            .MigrateAsync();
    }
}
