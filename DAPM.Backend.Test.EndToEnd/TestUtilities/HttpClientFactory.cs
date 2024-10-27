using System.Net.Http.Headers;

namespace TestUtilities;

public class HttpClientFactory(string accessControlBaseAddress, string clientApiBaseAddress, TokenFetcher tokenFetcher, 
    Dictionary<string, KeycloakUser> userCredentials) : IHttpClientFactory
{
    public HttpClient CreateAccessControlClient(string role)
        => CreateClient(accessControlBaseAddress, role);
    
    public HttpClient CreateClientApiClient(string role)
        => CreateClient(clientApiBaseAddress, role);
    
    private HttpClient CreateClient(string baseAddress, string role)
    {
        var client = new HttpClient();
        
        var token = tokenFetcher.GetTokenAsync(userCredentials[role]).GetAwaiter().GetResult();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        client.BaseAddress = new Uri(baseAddress);
        return client;
    }
}