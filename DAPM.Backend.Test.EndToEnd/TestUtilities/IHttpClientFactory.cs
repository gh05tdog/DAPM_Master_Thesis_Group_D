namespace TestUtilities;

public interface IHttpClientFactory
{
    HttpClient CreateAccessControlClient(string role);
    HttpClient CreateClientApiClient(string role);
}