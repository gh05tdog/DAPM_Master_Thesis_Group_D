using DAPM.Test.EndToEnd.TestUtilities;

namespace DAPM.Test.EndToEnd;

[Collection("ApiHttpCollection")]
public class AuthenticationTest(ApiHttpFixture apiHttpFixture)
{
    private readonly DapmClientApiHttpClient client = apiHttpFixture.Client;
    private readonly DapmClientApiHttpClient authenticatedClient = apiHttpFixture.AuthenticatedClient;

    [Fact]
    public async Task Given_unauthenticated_client_anonymous_returns_200_ok_anonymous()
    {
        var response = await client.GetAnonymousAsync();
        Assert.Equal("Anonymous", response);
    }
    
    [Fact]
    public async Task Given_unauthenticated_client_authenticated_throws_401_unauthorized()
    {
        var exception = await Assert.ThrowsAsync<HttpRequestException>(async () => await client.GetAuthorizeAsync());
        var response = exception.Message.Contains("401"); 
        Assert.True(response, "Expected a 401 status code, but did not receive it.");
    }
    
    [Fact]
    public async Task Given_authenticated_client_authorize_returns_200_ok_authenticated()
    {
        var response = await authenticatedClient.GetAuthorizeAsync();
        Assert.Equal("Authorized", response);
    }
}