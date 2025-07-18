using HelloWorld.Localization;
using HelloWorld.Permissions;
using JWTAuthorizeLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Authorization.Permissions;

namespace HelloWorld.Controllers;

[Route("api/testService")]
public class TestController : AbpController
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<TestController> _logger;
    private readonly IPermissionDefinitionManager _permissionDefinitionManager;

    public TestController(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        ILogger<TestController> logger,
        IPermissionDefinitionManager permissionDefinitionManager)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _logger = logger;
        _permissionDefinitionManager = permissionDefinitionManager;
    }

    [HttpGet]
	[PermissionAuthorize(HelloWorldPermissions.RequestManager)]
	public string Hello(){
		
		return "hello";
	}

	public async Task Test()
	{
        try
        {
            

            // 發送 HTTP 請求
            var httpClient = _httpClientFactory.CreateClient();
            var url = $"{_configuration["Url:AgsApiGateway"]}/api/app/authorization/permission";

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(new List<string>()),
                Encoding.UTF8,
                "application/json"
            );

            var response = await httpClient.PostAsync(url, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("API call failed with status {StatusCode}: {ErrorContent}",
                    response.StatusCode, errorContent);

            }
        }
        catch (HttpRequestException httpEx)
        {
            _logger.LogError(httpEx, "HTTP request failed during permission synchronization");

        }
        catch (JsonException jsonEx)
        {
            _logger.LogError(jsonEx, "JSON serialization failed during permission synchronization");

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during permission synchronization");

        }
    }
}
