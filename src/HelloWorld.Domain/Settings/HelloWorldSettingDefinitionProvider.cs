using Volo.Abp.Settings;

namespace HelloWorld.Settings;

public class HelloWorldSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(HelloWorldSettings.MySetting1));
    }
}
