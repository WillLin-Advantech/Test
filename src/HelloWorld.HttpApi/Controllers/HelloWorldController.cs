using HelloWorld.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HelloWorld.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class HelloWorldController : AbpControllerBase
{
    protected HelloWorldController()
    {
        LocalizationResource = typeof(HelloWorldResource);
    }
}
