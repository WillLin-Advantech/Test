using HelloWorld.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using JWTAuthorizeLibrary;

namespace HelloWorld.Controllers;

[Route("api/hello")]
public class TestController : AbpController
{
    [HttpGet]
	//[PermissionAuthorize("test")]
	public string Hello(){
		
		return "hello";
	}
}
