using HelloWorld.InfraStructure.ApiClient;
using HelloWorld.InfraStructure.ApiClient.Interface;
using HelloWorld.InfraStructure.Helpers;
using HelloWorld.InfraStructure.Helpers.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace HelloWorld.InfraStructure;

[DependsOn(
    typeof(HelloWorldDomainModule)
)]
public class HelloWorldInfraStructureModule : AbpModule
{

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<IHttpClientHelper, HttpClientHelper>();
        context.Services.AddTransient<IAgsAuthServiceClient, AgsAuthServiceClient>();
    }
}