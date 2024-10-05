using DAPM.Test.EndToEnd.TestUtilities;
using Microsoft.Extensions.Configuration;

namespace DAPM.Test.EndToEnd;

public class ApiHttpFixture : IDisposable
{
    public readonly DapmClientApiHttpClient Client;
    public readonly DapmClientApiHttpClient AuthenticatedClient;

    public ApiHttpFixture()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)  
            .Build();
        
        var baseAddress = new Uri(config["ClientApiSettings:BaseUrl"]);
        
        var keycloakBaseUrl = new Uri(config["Keycloak:BaseUrl"]);
        var keycloakRealm = config["Keycloak:realm"];
        var keycloakClientId = config["Keycloak:clientId"];
        var keycloakUsername = config["Keycloak:username"];
        var keycloakPassword = config["Keycloak:password"];
        var keycloakClientSecret = config["Keycloak:clientSecret"];

        var tokenFetcher = new TokenFetcher(keycloakBaseUrl, keycloakRealm, keycloakClientId,
            keycloakUsername, keycloakPassword, keycloakClientSecret);

        var apiHttpClientFactory = new ApiHttpClientFactory(baseAddress);
        var authenticatedApiHttpClientFactory = new AuthenticatedApiHttpClientFactory(baseAddress, tokenFetcher);
        
        Client = new DapmClientApiHttpClient(apiHttpClientFactory);
        AuthenticatedClient = new DapmClientApiHttpClient(authenticatedApiHttpClientFactory);
    }

    public void Dispose()
    {
    }
}