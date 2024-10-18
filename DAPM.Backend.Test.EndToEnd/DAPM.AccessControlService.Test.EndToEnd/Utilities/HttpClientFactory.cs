namespace DAPM.AccessControlService.Test.EndToEnd.Utilities;

public class HttpClientFactory(Uri baseAddress) : IHttpClientFactory
{
    public HttpClient CreateClient()
    {
        var client = new HttpClient();
        client.BaseAddress = baseAddress;
        return client;
    }
}