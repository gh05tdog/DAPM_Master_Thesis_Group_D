using System.Net.Http.Json;
using DAPM.AccessControlService.Test.EndToEnd.Dtos;
using DAPM.AccessControlService.Test.EndToEnd.Utilities;

namespace DAPM.AccessControlService.Test.EndToEnd;

[Collection("TestCollection")]
public class OrganizationTests(TestFixture fixture)
{
    private readonly IHttpClientFactory httpClientFactory = fixture.HttpClientFactory;
    
    [Fact]
    public async Task AddUserOrganizationSucceeds()
    {
        using var client = httpClientFactory.CreateClient();
        var request = new UserOrganizationDto
        {
            UserId = Guid.NewGuid(),
            OrganizationId = Guid.NewGuid()
        };
        
        var response = await client.PostAsJsonAsync(TestFixture.AddUserOrganizationRoute , request);
        var result = await response.Content.ReadFromJsonAsync<bool>();
        
        Assert.True(result);
    }
    
    [Fact]
    public async Task AddOrganizationForUserAndGetOrganizationsForUserReturnsOrganization()
    {
        using var client = httpClientFactory.CreateClient();
        
        var addUserOrganization = new UserOrganizationDto
        {
            UserId = Guid.NewGuid(),
            OrganizationId = Guid.NewGuid()
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserOrganizationRoute , addUserOrganization);
        
        var response = await client.GetAsync($"{TestFixture.GetUserOrganizationsRoute}/{addUserOrganization.UserId}");
        var result = await response.Content.ReadFromJsonAsync<ICollection<OrganizationDto>>();
        
        Assert.Contains(result, o => o.Id == addUserOrganization.OrganizationId);
    }

    [Fact]
    public async Task AddOrganizationForUserAndGetOrganizationsForOtherUserReturnsNoOrganization()
    {
        using var client = httpClientFactory.CreateClient();
        
        var addUserOrganization = new UserOrganizationDto
        {
            UserId = Guid.NewGuid(),
            OrganizationId = Guid.NewGuid()
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserPipelineRoute , addUserOrganization);
        
        var response = await client.GetAsync($"{TestFixture.GetUserOrganizationsRoute}/{Guid.NewGuid()}");
        var result = await response.Content.ReadFromJsonAsync<ICollection<OrganizationDto>>();
        
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task RemoveUserOrganizationSucceeds()
    {
        using var client = httpClientFactory.CreateClient();
        
        var addUserOrganization = new UserOrganizationDto
        {
            UserId = Guid.NewGuid(),
            OrganizationId = Guid.NewGuid()
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserOrganizationRoute , addUserOrganization);
        
        var request = new UserOrganizationDto
        {
            UserId = addUserOrganization.UserId,
            OrganizationId = addUserOrganization.OrganizationId
        };
        
        var response = await client.PostAsJsonAsync(TestFixture.RemoveUserOrganizationRoute , request);
        var result = await response.Content.ReadFromJsonAsync<bool>();
        
        Assert.True(result);
    }
    
    [Fact]
    public async Task GetAllUserOrganizationsReturnsOrganizations()
    {
        using var client = httpClientFactory.CreateClient();
        
        var addUserOrganization = new UserOrganizationDto
        {
            UserId = Guid.NewGuid(),
            OrganizationId = Guid.NewGuid()
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserOrganizationRoute , addUserOrganization);
        
        var response = await client.GetAsync(TestFixture.GetAllUserOrganizationsRoute);
        var result = await response.Content.ReadFromJsonAsync<ICollection<UserOrganizationDto>>();
        
        Assert.Contains(result, o => 
            o.OrganizationId == addUserOrganization.OrganizationId && 
            o.UserId == addUserOrganization.UserId);
    }
    
    [Fact]
    public async Task RemoveOrganizationForUserAndGetOrganizationsForUserReturnsNoOrganization()
    {
        using var client = httpClientFactory.CreateClient();
        
        var addUserOrganization = new UserOrganizationDto
        {
            UserId = Guid.NewGuid(),
            OrganizationId = Guid.NewGuid()
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserOrganizationRoute , addUserOrganization);
        
        var request = new UserOrganizationDto
        {
            UserId = addUserOrganization.UserId,
            OrganizationId = addUserOrganization.OrganizationId
        };
        
        await client.PostAsJsonAsync(TestFixture.RemoveUserOrganizationRoute , request);
        
        var response = await client.GetAsync($"{TestFixture.GetUserOrganizationsRoute}/{addUserOrganization.UserId}");
        var result = await response.Content.ReadFromJsonAsync<ICollection<OrganizationDto>>();
        
        Assert.DoesNotContain(addUserOrganization.OrganizationId, result.Select(o => o.Id));
    }
}