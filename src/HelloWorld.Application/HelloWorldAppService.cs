using System;
using System.Collections.Generic;
using System.Text;
using HelloWorld.Localization;
using Volo.Abp.Application.Services;

namespace HelloWorld;

/* Inherit your application services from this class.
 */
public abstract class HelloWorldAppService : ApplicationService
{
    protected HelloWorldAppService()
    {
        LocalizationResource = typeof(HelloWorldResource);
    }
}
