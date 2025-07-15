using HelloWorld.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HelloWorld.Controllers;

[Route("api/hello")]
public class TestController : AbpController
{
    [HttpGet]
	public string Hello(){
		
		return "hello";
	}
}
