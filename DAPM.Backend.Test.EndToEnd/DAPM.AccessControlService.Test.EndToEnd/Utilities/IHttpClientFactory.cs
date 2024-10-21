namespace DAPM.AccessControlService.Test.EndToEnd.Utilities;

public interface IHttpClientFactory
{
    HttpClient CreateClient();
}