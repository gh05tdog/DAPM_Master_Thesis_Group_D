using DAPM.Test.EndToEnd.TestUtilities;
using Microsoft.Extensions.Configuration;

namespace DAPM.Test.EndToEnd;

public class ApiHttpFixture
{
    public readonly DapmClientApiHttpClient Client;
    public readonly DapmClientApiHttpClient AuthenticatedClient;
    public readonly TokenFetcher TokenFetcher;
    public readonly string BaseUrl;
    public readonly AccessControlAdder AccessControlAdder;

    public ApiHttpFixture()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)  
            .Build();

        BaseUrl = config["ClientApiSettings:BaseUrl"];
        var baseAddress = new Uri(BaseUrl);
        
        var keycloakBaseUrl = new Uri(config["Keycloak:BaseUrl"]);
        var keycloakRealm = config["Keycloak:realm"];
        var keycloakClientId = config["Keycloak:clientId"];
        var keycloakUsername = config["Keycloak:username"];
        var keycloakPassword = config["Keycloak:password"];
        var keycloakClientSecret = config["Keycloak:clientSecret"];

        TokenFetcher = new TokenFetcher(keycloakBaseUrl, keycloakRealm, keycloakClientId,
            keycloakUsername, keycloakPassword, keycloakClientSecret);

        var apiHttpClientFactory = new ApiHttpClientFactory(baseAddress);
        var authenticatedApiHttpClientFactory = new AuthenticatedApiHttpClientFactory(baseAddress, TokenFetcher);
        
        Client = new DapmClientApiHttpClient(apiHttpClientFactory);
        AuthenticatedClient = new DapmClientApiHttpClient(authenticatedApiHttpClientFactory);
        AccessControlAdder = new AccessControlAdder(new Uri(config["ApiHttpClientFactorySettings:BaseUrl"]));
    }
}