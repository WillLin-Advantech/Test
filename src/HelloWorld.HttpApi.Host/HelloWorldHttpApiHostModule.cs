using HelloWorld.EntityFrameworkCore;
using HelloWorld.InfraStructure;
using HelloWorld.MultiTenancy;
using JWTAuthorizeLibrary;
using JWTAuthorizeLibrary.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Autofac;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Security.Claims;
using Volo.Abp.Swashbuckle;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;

namespace HelloWorld;

[DependsOn(
    typeof(HelloWorldHttpApiModule),
    typeof(AbpAutofacModule),
    typeof(AbpAspNetCoreMultiTenancyModule),
    typeof(HelloWorldApplicationModule),
    typeof(HelloWorldEntityFrameworkCoreModule),
    typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
    typeof(AbpAccountWebOpenIddictModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule),
    typeof(HelloWorldInfraStructureModule)
)]
public class HelloWorldHttpApiHostModule : AbpModule
{

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        context.Services.AddHttpClient();
        ConfigureAuthentication(context, configuration);
        ConfigureBundles();
        ConfigureUrls(configuration);
        ConfigureConventionalControllers();
        ConfigureVirtualFileSystem(context);
        ConfigureCors(context, configuration);
        ConfigureSwaggerServices(context, configuration);
       
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
        context.Services.Configure<AbpClaimsPrincipalFactoryOptions>(options =>
        {
            options.IsDynamicClaimsEnabled = true;
        });
        context.Services.AddCustomJwtAuthentication(configuration);
    }

    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options.StyleBundles.Configure(
                LeptonXLiteThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );
        });
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"]?.Split(',') ?? Array.Empty<string>());

            options.Applications["Angular"].RootUrl = configuration["App:ClientUrl"];
            options.Applications["Angular"].Urls[AccountUrlNames.PasswordReset] = "account/reset-password";
        });
    }

    private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<HelloWorldDomainSharedModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}HelloWorld.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<HelloWorldDomainModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}HelloWorld.Domain"));
                options.FileSets.ReplaceEmbeddedByPhysical<HelloWorldApplicationContractsModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}HelloWorld.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<HelloWorldApplicationModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}HelloWorld.Application"));
            });
        }
    }

    private void ConfigureConventionalControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(HelloWorldApplicationModule).Assembly);
        });
    }

    private static void ConfigureSwaggerServices(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAbpSwaggerGenWithOAuth(
            configuration["AuthServer:Authority"]!,
            new Dictionary<string, string>
            {
                    {"HelloWorld", "HelloWorld API"}
            },
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "HelloWorld API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });
    }

    private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(configuration["App:CorsOrigins"]?
                        .Split(",", StringSplitOptions.RemoveEmptyEntries)
                        .Select(o => o.RemovePostFix("/"))
                        .ToArray() ?? Array.Empty<string>())
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();
        app.UseMiddleware<BearerTokenMiddleware>();
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();


            var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
            Task.Run(async () => await SyncPermissionToSecondAPI(context, configuration));
            app.UseErrorPage();
        

        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }
        app.UseUnitOfWork();
        app.UseDynamicClaims();
        app.UseAuthorization();

        app.Use(async (context, next) =>
        {
            if (context.Request.Path.StartsWithSegments("/testService/swagger/v1/swagger.json"))
            {
                context.Response.Redirect("/swagger/v1/swagger.json", true);
                return;
            }
            await next();
        });
        app.UseSwagger();
        app.UseAbpSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/testService/swagger/v1/swagger.json", "HelloWorld API");

            var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
            c.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            c.OAuthScopes("HelloWorld");
        });

        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }

    #region --Private

    /// <summary>
    /// Sync權限給驗證的API
    /// </summary>
    private async Task SyncPermissionToSecondAPI(ApplicationInitializationContext context, IConfiguration configuration)
    {
        using var scope = context.ServiceProvider.CreateScope();
        var permissionDefinitionManager = scope.ServiceProvider.GetRequiredService<IPermissionDefinitionManager>();
        var permissionGroups = (await permissionDefinitionManager.GetGroupsAsync()).Where(group => group.Name == "HelloWorld");
        var permissionList = new List<PermissionDefinitionRecord>();
        foreach (var group in permissionGroups)
        {
            foreach (var permission in group.Permissions)
            {
                foreach (var childPermission in permission.Children)
                {
                    var childParamterModel = new PermissionDefinitionRecord
                    {
                        Name = childPermission.Name,
                        ParentName = permission.Name,
                        DisplayName = childPermission.DisplayName.Localize(null!),
                        IsEnabled = childPermission.IsEnabled,
                        GroupName = group.Name,
                        MultiTenancySide = childPermission.MultiTenancySide
                    };
                    permissionList.Add(childParamterModel);
                }
                var parentParamterModel = new PermissionDefinitionRecord
                {
                    Name = permission.Name,
                    DisplayName = permission.DisplayName.Localize(null!),
                    IsEnabled = permission.IsEnabled,
                    GroupName = group.Name,
                    MultiTenancySide = permission.MultiTenancySide
                };
                permissionList.Add(parentParamterModel);
            }
        }
        try
        {
            var httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient("AgsApi");

            var url = $"{configuration["Url:AgsApiGateway"]}/api/app/authorization/permission";
            var cont = JsonSerializer.Serialize(permissionList);
            var jsonContent = new StringContent(JsonSerializer.Serialize(permissionList), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, jsonContent);
            response.EnsureSuccessStatusCode();
            throw new UserFriendlyException($"Insert failed.{url}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            throw new UserFriendlyException($"Insert failed.{ex.Message}");
            throw;
        }
    }

    #endregion --Private
}
