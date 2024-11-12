using System.Net.Http.Headers;

namespace DAPM.ClientApi.AccessControl;

public class ApiHttpClientFactory(AccessControlConfig accessControlConfig, ITokenFetcher tokenFetcher) : IApiHttpClientFactory
{
    public HttpClient CreateClient()
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };

        var client = new HttpClient(handler);
        client.BaseAddress = new Uri(accessControlConfig.AccessControlUrl);
        var token = tokenFetcher.GetTokenAsync().GetAwaiter().GetResult();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return client;
    }
}