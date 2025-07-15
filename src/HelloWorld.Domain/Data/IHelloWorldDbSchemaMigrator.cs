using System.Threading.Tasks;

namespace HelloWorld.Data;

public interface IHelloWorldDbSchemaMigrator
{
    Task MigrateAsync();
}
