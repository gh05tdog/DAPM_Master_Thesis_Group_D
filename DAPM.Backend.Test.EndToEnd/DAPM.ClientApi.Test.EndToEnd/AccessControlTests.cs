using System.Net;
using DAPM.Test.EndToEnd.TestUtilities;

namespace DAPM.Test.EndToEnd;

[Collection("ApiHttpCollection")]
public class AccessControlTests
{
    private readonly IApiHttpClientFactory apiHttpClientFactory;
    private readonly Guid organizationId = Guid.NewGuid();
    private readonly Guid repositoryId = Guid.NewGuid();
    private readonly Guid resourceId = Guid.NewGuid();
    private readonly Guid pipelineId = Guid.NewGuid();

    public AccessControlTests(ApiHttpFixture apiHttpFixture)
    {
        apiHttpClientFactory = apiHttpFixture.AuthenticatedHttpClientFactory;
        apiHttpFixture.AccessControlAdder.AddUserOrganizationAsync(TestHelper.UserId, organizationId).Wait();
        apiHttpFixture.AccessControlAdder.AddUserRepositoryAsync(TestHelper.UserId, repositoryId).Wait();
        apiHttpFixture.AccessControlAdder.AddUserResourceAsync(TestHelper.UserId, resourceId).Wait();
        apiHttpFixture.AccessControlAdder.AddUserPipelineAsync(TestHelper.UserId, pipelineId).Wait();
    }

    [Fact]
    public async Task GetOrganizationById_UserHasAccess_RequestIsAuthorized()
    {
        using var client = apiHttpClientFactory.CreateClient();
        var response = await client.GetAsync($"organizations/{organizationId}");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task GetOrganizationById_UserHasNoAccess_RequestIsUnauthorized()
    {
        using var client = apiHttpClientFactory.CreateClient();
        var response = await client.GetAsync($"organizations/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetRepositoryById_UserHasAccess_RequestIsAuthorized()
    {
        using var client = apiHttpClientFactory.CreateClient();
        var response = await client.GetAsync($"organizations/{organizationId}/repositories/{repositoryId}");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task GetRepositoryById_UserHasNoAccess_RequestIsUnauthorized()
    {
        using var client = apiHttpClientFactory.CreateClient();
        var response = await client.GetAsync($"organizations/{organizationId}/repositories/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task GetResourceById_UserHasAccess_RequestIsAuthorized()
    {
        using var client = apiHttpClientFactory.CreateClient();
        var response = await client.GetAsync($"organizations/{organizationId}/repositories/{repositoryId}/resources/{resourceId}");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task GetResourceById_UserHasNoAccess_RequestIsUnauthorized()
    {
        using var client = apiHttpClientFactory.CreateClient();
        var response = await client.GetAsync($"organizations/{organizationId}/repositories/{repositoryId}/resources/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task GetPipelineById_UserHasAccess_RequestIsAuthorized()
    {
        using var client = apiHttpClientFactory.CreateClient();
        var response = await client.GetAsync($"organizations/{organizationId}/repositories/{repositoryId}/pipelines/{pipelineId}");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task GetPipelineById_UserHasNoAccess_RequestIsUnauthorized()
    {
        using var client = apiHttpClientFactory.CreateClient();
        var response = await client.GetAsync($"organizations/{organizationId}/repositories/{repositoryId}/pipelines/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}