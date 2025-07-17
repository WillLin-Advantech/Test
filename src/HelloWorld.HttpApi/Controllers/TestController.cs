using HelloWorld.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using JWTAuthorizeLibrary;
using HelloWorld.Permissions;

namespace HelloWorld.Controllers;

[Route("api/testService")]
public class TestController : AbpController
{
    [HttpGet]
	[PermissionAuthorize(HelloWorldPermissions.RequestManager)]
	public string Hello(){
		
		return "hello";
	}
}
