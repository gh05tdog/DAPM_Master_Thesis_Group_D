using System.Net.Http.Headers;

namespace DAPM.Test.EndToEnd.TestUtilities;

public class AuthenticatedApiHttpClientFactory(Uri baseAddress, TokenFetcher tokenFetcher)
    : IApiHttpClientFactory
{

    public HttpClient CreateClient()
    {
        var client = new HttpClient();
        client.BaseAddress = baseAddress;

        var token = tokenFetcher.GetTokenAsync().GetAwaiter().GetResult();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        return client;
    }
}