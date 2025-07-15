using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace HelloWorld;

[Dependency(ReplaceServices = true)]
public class HelloWorldBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "HelloWorld";
}
