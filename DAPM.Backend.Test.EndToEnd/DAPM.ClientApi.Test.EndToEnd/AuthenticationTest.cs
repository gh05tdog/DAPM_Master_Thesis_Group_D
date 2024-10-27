using System.Net;
using DAPM.Test.EndToEnd.TestUtilities;
using TestUtilities;

namespace DAPM.Test.EndToEnd;

[Collection("ApiHttpCollection")]
public class AuthenticationTest(ApiHttpFixture apiHttpFixture)
{
    [Fact]
    public async Task Given_unauthenticated_client_anonymous_returns_200_ok_anonymous()
    {
        using var client = apiHttpFixture.HttpClientFactory.CreateClientApiClient(Users.Test);
        var response = await client.GetAsync("test/authentication/anonymous");
        var message = await response.Content.ReadAsStringAsync();
        Assert.Equal("Anonymous", message);
    }
    
    [Fact]
    public async Task Given_unauthenticated_client_authenticated_throws_401_unauthorized()
    {
        using var client = apiHttpFixture.HttpClientFactory.CreateClientApiClient(Users.Test);
        var response = await client.GetAsync("test/authentication/authorize");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task Given_authenticated_client_authorize_returns_200_ok_authenticated()
    {
        var response = await apiHttpFixture.Client.GetAuthorizeAsync();
        Assert.Equal("Authorized", response);
    }
}