using System.Net.Http.Json;
using TestUtilities;
using TestUtilities.Dtos;

namespace DAPM.AccessControlService.Test.EndToEnd;

[Collection("TestCollection")]
public class ResourceTests(TestFixture fixture)
{ 
    private readonly IHttpClientFactory httpClientFactory = fixture.HttpClientFactory;
    
    [Fact]
    public async Task AddUserResourceSucceeds()
    {
        using var client = httpClientFactory.CreateAccessControlClient(Users.ResourceManager);
        var request = new UserResourceDto
        {
            UserId = Guid.NewGuid(),
            ResourceId = Guid.NewGuid()
        };
        
        var response = await client.PostAsJsonAsync(TestFixture.AddUserResourceRoute , request);
        var result = await response.Content.ReadFromJsonAsync<bool>();
        
        Assert.True(result);
    }
    
    [Fact]
    public async Task AddResourceForUserAndGetResourcesForUserReturnsResource()
    {
        using var client = httpClientFactory.CreateAccessControlClient(Users.ResourceManager);
        
        var addUserResource = new UserResourceDto
        {
            UserId = Guid.NewGuid(),
            ResourceId = Guid.NewGuid()
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserResourceRoute , addUserResource);
        
        var response = await client.GetAsync($"{TestFixture.GetUserResourcesRoute}/{addUserResource.UserId}");
        var result = await response.Content.ReadFromJsonAsync<ICollection<ResourceDto>>();
        
        Assert.Contains(addUserResource.ResourceId, result.Select(r => r.Id));
    }
    
    [Fact]
    public async Task AddResourceForUserAndGetResourcesForOtherUserReturnsNoResource()
    {
        using var client = httpClientFactory.CreateAccessControlClient(Users.ResourceManager);
        
        var addUserResource = new UserResourceDto
        {
            UserId = Guid.NewGuid(),
            ResourceId = Guid.NewGuid()
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserResourceRoute , addUserResource);
        
        var response = await client.GetAsync($"{TestFixture.GetUserResourcesRoute}/{Guid.NewGuid()}");
        var result = await response.Content.ReadFromJsonAsync<ICollection<ResourceDto>>();
        
        Assert.DoesNotContain(addUserResource.ResourceId, result.Select(r => r.Id));
    }
    
    [Fact]
    public async Task RemoveUserResourceSucceeds()
    {
        using var client = httpClientFactory.CreateAccessControlClient(Users.ResourceManager);
        
        var addUserResource = new UserResourceDto
        {
            UserId = Guid.NewGuid(),
            ResourceId = Guid.NewGuid()
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserResourceRoute , addUserResource);
        
        var request = new UserResourceDto
        {
            UserId = addUserResource.UserId,
            ResourceId = addUserResource.ResourceId
        };
        
        var response = await client.PostAsJsonAsync(TestFixture.RemoveUserResourceRoute , request);
        var result = await response.Content.ReadFromJsonAsync<bool>();
        
        Assert.True(result);
    }
    
    [Fact]
    public async Task GetAllUserResourcesReturnsResources()
    {
        using var client = httpClientFactory.CreateAccessControlClient(Users.ResourceManager);
        
        var addUserResource = new UserResourceDto
        {
            UserId = Guid.NewGuid(),
            ResourceId = Guid.NewGuid()
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserResourceRoute , addUserResource);
        
        var response = await client.GetAsync(TestFixture.GetAllUserResourcesRoute);
        var result = await response.Content.ReadFromJsonAsync<ICollection<UserResourceDto>>();
        
        Assert.Contains(result, p => 
            p.ResourceId == addUserResource.ResourceId && 
            p.UserId == addUserResource.UserId);
    }
    
    [Fact]
    public async Task RemoveResourceForUserAndGetResourcesForUserReturnsNoResource()
    {
        using var client = httpClientFactory.CreateAccessControlClient(Users.ResourceManager);
        
        var addUserResource = new UserResourceDto
        {
            UserId = Guid.NewGuid(),
            ResourceId = Guid.NewGuid()
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserResourceRoute , addUserResource);
        
        var request = new UserResourceDto
        {
            UserId = addUserResource.UserId,
            ResourceId = addUserResource.ResourceId
        };
        
        await client.PostAsJsonAsync(TestFixture.RemoveUserResourceRoute , request);
        
        var response = await client.GetAsync($"{TestFixture.GetUserResourcesRoute}/{addUserResource.UserId}");
        var result = await response.Content.ReadFromJsonAsync<ICollection<ResourceDto>>();
        
        Assert.DoesNotContain(addUserResource.ResourceId, result.Select(r => r.Id));
    }
}