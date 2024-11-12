using System.Net.Http.Json;
using TestUtilities;
using TestUtilities.Dtos;

namespace DAPM.AccessControlService.Test.EndToEnd;

[Collection("TestCollection")]
public class RepositoryTests(TestFixture fixture)
{ 
    private readonly IHttpClientFactory httpClientFactory = fixture.HttpClientFactory;

    [Fact]
    public async Task AddUserRepositorySucceeds()
    {
        using var client = httpClientFactory.CreateAccessControlClient(Users.RepositoryManager);
        
        var request = new UserRepositoryDto
        {
            UserId = Guid.NewGuid(),
            RepositoryId = Guid.NewGuid()
        };
        
        var response = await client.PostAsJsonAsync(TestFixture.AddUserRepositoryRoute , request);
        var result = await response.Content.ReadFromJsonAsync<bool>();
        
        Assert.True(result);
    }
    
    [Fact]
    public async Task AddRepositoryForUserAndGetRepositoriesForUserReturnsRepository()
    {
        using var client = httpClientFactory.CreateAccessControlClient(Users.RepositoryManager);
        
        var addUserRepository = new UserRepositoryDto
        {
            UserId = Guid.NewGuid(),
            RepositoryId = Guid.NewGuid()
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserRepositoryRoute , addUserRepository);
        
        var response = await client.GetAsync($"{TestFixture.GetUserRepositoriesRoute}/{addUserRepository.UserId}");
        var result = await response.Content.ReadFromJsonAsync<ICollection<RepositoryDto>>();
        
        Assert.True(result.Any(r => r.Id == addUserRepository.RepositoryId));
    }
    
    [Fact]
    public async Task AddRepositoryForUserAndGetRepositoriesForOtherUserReturnsNoRepository()
    {
        using var client = httpClientFactory.CreateAccessControlClient(Users.RepositoryManager);
        
        var addUserRepository = new UserRepositoryDto
        {
            UserId = Guid.NewGuid(),
            RepositoryId = Guid.NewGuid()
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserRepositoryRoute , addUserRepository);
        
        var response = await client.GetAsync($"{TestFixture.GetUserRepositoriesRoute}/{Guid.NewGuid()}");
        var result = await response.Content.ReadFromJsonAsync<ICollection<RepositoryDto>>();
        
        Assert.False(result.Any(r => r.Id == addUserRepository.RepositoryId));
    }
    
    [Fact]
    public async Task RemoveRepositoryForUserAndGetRepositoriesForUserReturnsNoRepository()
    {
        using var client = httpClientFactory.CreateAccessControlClient(Users.RepositoryManager);
        
        var addUserRepository = new UserRepositoryDto
        {
            UserId = Guid.NewGuid(),
            RepositoryId = Guid.NewGuid()
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserRepositoryRoute, addUserRepository);
        
        var request = new UserRepositoryDto
        {
            UserId = addUserRepository.UserId,
            RepositoryId = addUserRepository.RepositoryId
        };
        
        await client.PostAsJsonAsync(TestFixture.RemoveUserRepositoryRoute, request);
        
        var response = await client.GetAsync($"{TestFixture.GetUserRepositoriesRoute}/{addUserRepository.UserId}");
        var result = await response.Content.ReadFromJsonAsync<ICollection<RepositoryDto>>();
        
        Assert.False(result.Any(r => r.Id == addUserRepository.RepositoryId));
    }
    
    [Fact]
    public async Task RemoveUserRepositorySucceeds()
    {
        using var client = httpClientFactory.CreateAccessControlClient(Users.RepositoryManager);
        
        var addUserRepository = new UserRepositoryDto
        {
            UserId = Guid.NewGuid(),
            RepositoryId = Guid.NewGuid()
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserRepositoryRoute , addUserRepository);
        
        var request = new UserRepositoryDto
        {
            UserId = addUserRepository.UserId,
            RepositoryId = addUserRepository.RepositoryId
        };
        
        var response = await client.PostAsJsonAsync(TestFixture.RemoveUserRepositoryRoute , request);
        var result = await response.Content.ReadFromJsonAsync<bool>();
        
        Assert.True(result);
    }
    
    [Fact]
    public async Task GetAllUserRepositoriesReturnsRepositories()
    {
        using var client = httpClientFactory.CreateAccessControlClient(Users.RepositoryManager);
        
        var addUserRepository = new UserRepositoryDto
        {
            UserId = Guid.NewGuid(),
            RepositoryId = Guid.NewGuid()
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserRepositoryRoute , addUserRepository);
        
        var response = await client.GetAsync(TestFixture.GetAllUserRepositoriesRoute);
        var result = await response.Content.ReadFromJsonAsync<ICollection<UserRepositoryDto>>();
        
        Assert.Contains(result, p => 
            p.RepositoryId == addUserRepository.RepositoryId && 
            p.UserId == addUserRepository.UserId);
    }
}