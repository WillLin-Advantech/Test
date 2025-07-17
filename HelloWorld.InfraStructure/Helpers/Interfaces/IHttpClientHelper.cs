namespace HelloWorld.InfraStructure.Helpers.Interfaces;

/// <summary>
/// IHttpClientHelper 
/// </summary>
public interface IHttpClientHelper
{
    /// <summary>
    /// Get Method
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="requestUri"></param>
    /// <returns></returns>
    Task<T?> GetAsync<T>(Uri requestUri);
}