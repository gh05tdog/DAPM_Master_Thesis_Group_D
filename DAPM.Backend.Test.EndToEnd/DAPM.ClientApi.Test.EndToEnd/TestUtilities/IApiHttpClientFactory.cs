namespace DAPM.Test.EndToEnd.TestUtilities;

public interface IApiHttpClientFactory
{
    public HttpClient CreateClient();
}