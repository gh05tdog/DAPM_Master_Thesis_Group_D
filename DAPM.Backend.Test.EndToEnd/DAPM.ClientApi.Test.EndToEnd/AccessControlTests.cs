using System.Net;
using DAPM.Test.EndToEnd.TestUtilities;
using TestUtilities;

namespace DAPM.Test.EndToEnd;

[Collection("ApiHttpCollection")]
public class AccessControlTests
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly Guid organizationId = Guid.NewGuid();
    private readonly Guid repositoryId = Guid.NewGuid();
    private readonly Guid resourceId = Guid.NewGuid();
    private readonly Guid pipelineId = Guid.NewGuid();

    public AccessControlTests()
    {
        var fixture = new TestUtility();
        httpClientFactory = fixture.HttpClientFactory;

        var accessControlAdder = new AccessControlAdder(Users.Test);
        accessControlAdder.AddUserOrganizationAsync(TestHelper.UserId, organizationId).Wait();
        accessControlAdder.AddUserRepositoryAsync(TestHelper.UserId, repositoryId).Wait();
        accessControlAdder.AddUserResourceAsync(TestHelper.UserId, resourceId).Wait();
        accessControlAdder.AddUserPipelineAsync(TestHelper.UserId, pipelineId).Wait();
    }

    [Fact]
    public async Task GetOrganizationById_UserHasAccess_RequestIsAuthorized()
    {
        using var client = httpClientFactory.CreateClientApiClient(Users.Test);
        var response = await client.GetAsync($"organizations/{organizationId}");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task GetOrganizationById_UserHasNoAccess_RequestIsUnauthorized()
    {
        using var client = httpClientFactory.CreateClientApiClient(Users.Test);
        var response = await client.GetAsync($"organizations/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetRepositoryById_UserHasAccess_RequestIsAuthorized()
    {
        using var client = httpClientFactory.CreateClientApiClient(Users.Test);
        var response = await client.GetAsync($"organizations/{organizationId}/repositories/{repositoryId}");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task GetRepositoryById_UserHasNoAccess_RequestIsUnauthorized()
    {
        using var client = httpClientFactory.CreateClientApiClient(Users.Test);
        var response = await client.GetAsync($"organizations/{organizationId}/repositories/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task GetResourceById_UserHasAccess_RequestIsAuthorized()
    {
        using var client = httpClientFactory.CreateClientApiClient(Users.Test);
        var response = await client.GetAsync($"organizations/{organizationId}/repositories/{repositoryId}/resources/{resourceId}");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task GetResourceById_UserHasNoAccess_RequestIsUnauthorized()
    {
        using var client = httpClientFactory.CreateClientApiClient(Users.Test);
        var response = await client.GetAsync($"organizations/{organizationId}/repositories/{repositoryId}/resources/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task GetPipelineById_UserHasAccess_RequestIsAuthorized()
    {
        using var client = httpClientFactory.CreateClientApiClient(Users.Test);
        var response = await client.GetAsync($"organizations/{organizationId}/repositories/{repositoryId}/pipelines/{pipelineId}");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task GetPipelineById_UserHasNoAccess_RequestIsUnauthorized()
    {
        using var client = httpClientFactory.CreateClientApiClient(Users.Test);
        var response = await client.GetAsync($"organizations/{organizationId}/repositories/{repositoryId}/pipelines/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}