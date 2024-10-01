using DAPM.Test.EndToEnd.TestUtilities;

namespace DAPM.Test.EndToEnd;

[Collection("ApiHttpCollection")]
public class OrganizationTest
{
    private readonly DapmClientApiHttpClient client;

    public OrganizationTest(ApiHttpFixture apiHttpFixture)
    {
        this.client = apiHttpFixture.Client;
    }
    
    [Fact]
    public async Task GetOrganizationsReturnSingleOrganization()
    {
        var organizations = await client.GetOrganizationsAsync();
        Assert.Single(organizations);
    }
    
    [Fact]
    public async Task GetOrganizationByIdReturnOrganization()
    {
        var organizations = await client.GetOrganizationsAsync();
        var id = organizations.First().Id;
        var organization = await client.GetOrganizationByIdAsync(id);
        Assert.NotNull(organization);
    }
    
    [Fact]
    public async Task CreateRepositoryReturnsNewRepositoryId()
    {
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
        var organizationId = (await client.GetOrganizationsAsync()).First().Id;
        var repositoryName = Guid.NewGuid().ToString();
        var postRepositoryResult = await client.PostRepositoryAsync(organizationId, repositoryName);
        var repositories = await client.GetRepositoriesAsync(organizationId);
        Assert.NotNull(repositories.Where(r => r.Id == postRepositoryResult.ItemIds.RepositoryId));
    }
}