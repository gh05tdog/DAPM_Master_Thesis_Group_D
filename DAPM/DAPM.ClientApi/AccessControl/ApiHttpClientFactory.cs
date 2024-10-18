using Microsoft.Extensions.Options;

namespace DAPM.ClientApi.AccessControl;

public class ApiHttpClientFactory(IOptions<ApiHttpClientFactorySettings> settings) : IApiHttpClientFactory
{
    private readonly ApiHttpClientFactorySettings settings = settings.Value;

    public HttpClient CreateClient()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(settings.BaseUrl);
        return client;
    }
}