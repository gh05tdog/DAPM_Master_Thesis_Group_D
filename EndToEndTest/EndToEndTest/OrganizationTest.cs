using EndToEndTest.TestUtilities;

namespace EndToEndTest;

public class OrganizationTest
{
    [Fact]
    public async Task GetOrganizationsReturnSingleOrganization()
    {
        var client = new DapmClientApiHttpClient();
        var organizations = await client.GetOrganizationsAsync();
        Assert.Single(organizations);
    }
    
    [Fact]
    public async Task GetOrganizationByIdReturnOrganization()
    {
        var client = new DapmClientApiHttpClient();
        var organizations = await client.GetOrganizationsAsync();
        var id = organizations.First().Id;
        var organization = await client.GetOrganizationByIdAsync(id);
        Assert.NotNull(organization);
    }
    
    [Fact]
    public async Task CreateRepositoryReturnsNewRepositoryId()
    {
        var client = new DapmClientApiHttpClient();
        var organizationId = (await client.GetOrganizationsAsync()).First().Id;
        var repositoryResult = await client.PostRepositoryAsync(organizationId, "Test");
        Assert.NotNull(repositoryResult.ItemIds);
    }
    
    [Fact]
    public async Task GetRepositoryReturnsNewRepositoryId()
    {
        var client = new DapmClientApiHttpClient();
        var organizationId = (await client.GetOrganizationsAsync()).First().Id;
        var repositoryName = Guid.NewGuid().ToString();
        var postRepositoryResult = await client.PostRepositoryAsync(organizationId, repositoryName);
        var repositories = await client.GetRepositoriesAsync(organizationId);
        Assert.NotNull(repositories.Where(r => r.Id == postRepositoryResult.ItemIds.RepositoryId));
    }
}