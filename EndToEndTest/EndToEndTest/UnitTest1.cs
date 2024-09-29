using EndToEndTest.TestUtilities;

namespace EndToEndTest;

public class UnitTest1
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
}