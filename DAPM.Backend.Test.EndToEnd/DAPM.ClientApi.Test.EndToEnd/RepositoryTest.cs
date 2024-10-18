using DAPM.Test.EndToEnd.TestUtilities;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
namespace DAPM.Test.EndToEnd;

[Collection("ApiHttpCollection")]
public class RepositoryTest
{
    private readonly DapmClientApiHttpClient client;
    
    public RepositoryTest(ApiHttpFixture apiHttpFixture)
    {
        this.client = apiHttpFixture.AuthenticatedClient;
        
    }
    
    [Fact]
    public async Task GetRepositoryByIdReturnSingleRepository()
    {
        var organizationId = (await client.GetOrganizationsAsync()).First().Id;
        var repositoryName = Guid.NewGuid().ToString();
        var postRepositoryResult = await client.PostRepositoryAsync(organizationId, repositoryName);
        var repositoryId = postRepositoryResult.ItemIds.RepositoryId;
        var repositories = await client.GetRepositoryByIdAsync(organizationId, repositoryId);
        Assert.Single(repositories);
    }

    //There is a bug in the implementation. calling API with unknown ID, creates a ticket which never gets completed
    [Fact (Skip = "There is a bug in the implementation. calling API with unknown ID, creates a ticket which never gets completed")]
    public async Task GetRepositoryByWrongIdReturnNoRepository() 
    {
        var organizationId = (await client.GetOrganizationsAsync()).First().Id;
        var repositoryId = Guid.NewGuid();
        var repositories = await client.GetRepositoryByIdAsync(organizationId,repositoryId);
        Assert.Empty(repositories);
    }
    
    /*
    [Fact]
    public async Task CreateResourceReturnsNewResourceId()
    {
        var organizationId = (await client.GetOrganizationsAsync()).First().Id;
        var repositoryId = (await client.GetRepositoriesAsync(organizationId)).First().Id;
        var name= "testname";
        var type= "testtype";
        string filePath = "test.txt";
        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var fileContent = new StreamContent(fileStream);
        var resourceResult = await client.PostResourceAsync(organizationId,repositoryId,name,type,fileContent );
        var idIsNullOrDefault = resourceResult.ItemIds == null || resourceResult.ItemIds.ResourceId == default;
        Assert.False(idIsNullOrDefault);
    }
    */
    [Fact(Skip = "Need other user to create the repository")]
    public async Task GetResourcesReturns0Resources()
    {
        var organizationId = (await client.GetOrganizationsAsync()).First().Id;
        var repositoryId = (await client.GetRepositoriesAsync(organizationId)).First().Id;
        var resources = await client.GetResourcesAsync(organizationId,repositoryId);
        Assert.Empty(resources);
    }
    
    [Fact(Skip = "Need other user to create the pipelines")]
    public async Task GetPipelinesReturns0Pipelines()
    {
        var organizationId = (await client.GetOrganizationsAsync()).First().Id;
        var repositoryId = (await client.GetRepositoriesAsync(organizationId)).First().Id;
        var resources = await client.GetPipelinesAsync(organizationId,repositoryId);
        Assert.Empty(resources);
    }
    
    [Fact(Skip = "With the new access control the user has need to create the item to access it")]
    public async Task GetResourcesReturnsResources()
    {
        var organizationId = (await client.GetOrganizationsAsync()).First().Id;
        var repositoryId = (await client.GetRepositoriesAsync(organizationId)).First().Id;
        var resources = await client.GetResourcesAsync(organizationId,repositoryId);
        Assert.NotEmpty(resources);
    }
    
    [Fact(Skip = "With the new access control the user has need to create the item to access it")]
    public async Task GetPipelinesReturnsPipelines()
    {
        var organizationId = (await client.GetOrganizationsAsync()).First().Id;
        var repositoryId = (await client.GetRepositoriesAsync(organizationId)).First().Id;
        var resources = await client.GetPipelinesAsync(organizationId,repositoryId);
        Assert.NotEmpty(resources);
    }
}