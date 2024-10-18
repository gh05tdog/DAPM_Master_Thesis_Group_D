namespace DAPM.ClientApi.AccessControl;

public interface IApiHttpClientFactory
{
    HttpClient CreateClient();
}