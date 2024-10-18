namespace DAPM.Test.EndToEnd.TestUtilities;

public class ApiHttpClientFactory(Uri baseAddress) : IApiHttpClientFactory
{
    public HttpClient CreateClient()
    {
        var client = new HttpClient();
        client.BaseAddress = baseAddress;
        return client;
    }
}