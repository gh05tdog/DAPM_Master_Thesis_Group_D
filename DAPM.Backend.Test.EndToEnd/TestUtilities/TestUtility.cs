using Microsoft.Extensions.Configuration;

namespace TestUtilities;

public class TestUtility
{
    public readonly IHttpClientFactory HttpClientFactory;
    public readonly TokenFetcher TokenFetcher;

    public TestUtility()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)  
            .Build();
        
        var accessControlConfig = config.GetSection("AccessControl").Get<AccessControlConfig>();
        var clientApiConfig = config.GetSection("ClientApi").Get<ClientApiConfig>();
        var keycloakConfig = config.GetSection("Keycloak").Get<KeycloakConfig>();
        
        TokenFetcher = new TokenFetcher(new Uri(keycloakConfig.BaseUrl), keycloakConfig.Realm, keycloakConfig.ClientId);
        HttpClientFactory = new HttpClientFactory(accessControlConfig.BaseUrl, clientApiConfig.BaseUrl, TokenFetcher, keycloakConfig.Users);
    }
}