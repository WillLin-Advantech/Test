namespace HelloWorld.InfraStructure.ApiClient.Interface;

/// <summary>
/// IAgsAuthServiceClient
/// </summary>
public interface IAgsAuthServiceClient
{
    /// <summary>
    /// 取得使用者名稱
    /// </summary>
    /// <returns></returns>
    Task<string?> GetUserNameAsync(Guid userId);
}