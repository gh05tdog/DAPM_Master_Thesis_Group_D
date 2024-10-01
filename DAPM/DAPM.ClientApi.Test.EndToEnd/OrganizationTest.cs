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
        var repositoryResult = await client.PostRepositoryAsync(organizationId, "Test");
        var idIsNullOrDefault = repositoryResult.ItemIds == null || repositoryResult.ItemIds.RepositoryId == default;
        Assert.False(idIsNullOrDefault);
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