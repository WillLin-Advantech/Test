using HelloWorld.InfraStructure.Helpers.Interfaces;
using System.Text.Json;

namespace HelloWorld.InfraStructure.Helpers;

/// <summary>
/// HttpClientHelper
/// </summary>
public class HttpClientHelper
    (
    IHttpClientFactory httpClientFactory
    ) : IHttpClientHelper
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    /// <summary>
    /// GetMethod
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="requestUri"></param>
    /// <returns></returns>
    public async Task<T?> GetAsync<T>(Uri requestUri)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(jsonResponse) || jsonResponse == "[]")
        {
            return default;
        }
        var result = JsonSerializer.Deserialize<T>
            (
            jsonResponse,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        return result;
    }
}