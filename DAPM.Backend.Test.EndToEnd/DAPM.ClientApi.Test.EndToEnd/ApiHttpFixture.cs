using DAPM.Test.EndToEnd.TestUtilities;
using TestUtilities;

namespace DAPM.Test.EndToEnd;

public class ApiHttpFixture
{
    public readonly DapmClientApiHttpClient Client;
    public readonly TokenFetcher TokenFetcher;
    public readonly AccessControlAdder AccessControlAdder;
    public readonly IHttpClientFactory HttpClientFactory;

    public ApiHttpFixture()
    {
        var fixture = new TestUtility();
        HttpClientFactory = fixture.HttpClientFactory;

        Client = new DapmClientApiHttpClient(HttpClientFactory);
        AccessControlAdder = new AccessControlAdder(Users.Test);
    }
}