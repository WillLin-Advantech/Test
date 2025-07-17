using HelloWorld.InfraStructure.ApiClient.Interface;
using HelloWorld.InfraStructure.ApiClient.Model.Responses.AgsAuthService;
using HelloWorld.InfraStructure.Options;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace HelloWorld.InfraStructure.ApiClient;

/// <summary>
/// AgsAuthServiceClient
/// </summary>
public class AgsAuthServiceClient
    (
    IOptions<UrlOption> options,
    IHttpClientFactory httpClientFactory
    ) : IAgsAuthServiceClient
{
    private readonly UrlOption _urlOption = options.Value;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    /// <summary>
    /// 取得使用者名稱
    /// </summary>
    /// <returns></returns>
    public async Task<string?> GetUserNameAsync(Guid userId)
    {
        var encodedUserId = Uri.EscapeDataString(userId.ToString());
        var url = $"{_urlOption.AgsAuthService}/api/app/authorization/user/{encodedUserId}";
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(jsonResponse))
        {
            return default;
        }
        var result = JsonSerializer.Deserialize<UserInformaionDto>
            (
            jsonResponse,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        return result?.UserName;
    }
}
