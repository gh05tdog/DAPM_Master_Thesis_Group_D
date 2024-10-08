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
        this.client = apiHttpFixture.Client;
        
    }
    
    [Fact]
    public async Task GetReposityByIdReturnSingleRepository()
    {
        var organizationId = (await client.GetOrganizationsAsync()).First().Id;
        var repositoryId = (await client.GetRepositoriesAsync(organizationId)).First().Id;
        var repositories = await client.GetRepositoryByIdAsync(organizationId,repositoryId);
        Assert.Single(repositories);
    }

    //There is a bug in the implementation. calling API with unknown ID, creates a ticket which never gets completed
    /*
    [Fact]
    public async Task GetReposityByWrongIdReturnNoRepository() 
    {
        var organizationId = (await client.GetOrganizationsAsync()).First().Id;
        var repositoryId = Guid.NewGuid();
        var repositories = await client.GetRepositoryByIdAsync(organizationId,repositoryId);
        Assert.Empty(repositories);
    }
    */
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
    [Fact]
    public async Task GetResourcesReturnsResources()
    {
        var organizationId = (await client.GetOrganizationsAsync()).First().Id;
        var repositoryId = (await client.GetRepositoriesAsync(organizationId)).First().Id;
        var resources = await client.GetResourcesAsync(organizationId,repositoryId);
        Assert.NotNull(resources);
    }
    [Fact]
    public async Task GetPipelinesReturnsPipelines()
    {
        var organizationId = (await client.GetOrganizationsAsync()).First().Id;
        var repositoryId = (await client.GetRepositoriesAsync(organizationId)).First().Id;
        var resources = await client.GetPipelinesAsync(organizationId,repositoryId);
        Assert.NotNull(resources);
    }
}