using System.Net.Http.Headers;

namespace DAPM.ClientApi.AccessControl;

public class ApiHttpClientFactory(AccessControlConfig accessControlConfig, ITokenFetcher tokenFetcher) : IApiHttpClientFactory
{
    public HttpClient CreateClient()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(accessControlConfig.AccessControlUrl);
        var token = tokenFetcher.GetTokenAsync().GetAwaiter().GetResult();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return client;
    }
}