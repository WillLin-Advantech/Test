using HelloWorld.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace HelloWorld.Controllers;

[Route("api/hello")]
public class TestController : AbpController
{
    [HttpGet]
	[AllowAnonymous]
	public string Hello(){
		
		return "hello";
	}
}
