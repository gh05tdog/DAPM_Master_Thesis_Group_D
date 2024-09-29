using EndToEndTest.TestUtilities;
using System.Collections.Generic;

namespace EndToEndTest;

public class OrganizationTest
{
    private static Dictionary<string, System.Uri> environments= new Dictionary<string, System.Uri>{
        {"local", new Uri("http://localhost:5000")},
        {"online", new Uri("http://se2-d.compute.dtu.dk:5000/")}
    };
    
    private static Uri environment= environments["local"];
    
    [Fact]
    public async Task GetOrganizationsReturnSingleOrganization()
    {
        var client = new DapmClientApiHttpClient(environment);
        var organizations = await client.GetOrganizationsAsync();
        Assert.Single(organizations);
    }
    
    [Fact]
    public async Task GetOrganizationByIdReturnOrganization()
    {
        var client = new DapmClientApiHttpClient(environment);
        var organizations = await client.GetOrganizationsAsync();
        var id = organizations.First().Id;
        var organization = await client.GetOrganizationByIdAsync(id);
        Assert.NotNull(organization);
    }
    
    [Fact]
    public async Task CreateRepositoryReturnsNewRepositoryId()
    {
        var client = new DapmClientApiHttpClient(environment);
        var organizationId = (await client.GetOrganizationsAsync()).First().Id;
        var repositoriesBefore = (await client.GetRepositoriesAsync(organizationId)).Count;
        var repositoryResult = await client.PostRepositoryAsync(organizationId, "Test");
        Assert.NotNull(repositoryResult.ItemIds);
        var repositoriesAfter = (await client.GetRepositoriesAsync(organizationId)).Count;
        Assert.Equal(repositoriesBefore+1,repositoriesAfter);
    }
    
    [Fact]
    public async Task GetRepositoryReturnsNewRepositoryId()
    {
        var client = new DapmClientApiHttpClient(environment);
        var organizationId = (await client.GetOrganizationsAsync()).First().Id;
        var repositoryName = Guid.NewGuid().ToString();
        var postRepositoryResult = await client.PostRepositoryAsync(organizationId, repositoryName);
        var repositories = await client.GetRepositoriesAsync(organizationId);
        Assert.NotNull(repositories.Where(r => r.Id == postRepositoryResult.ItemIds.RepositoryId));
    }

}