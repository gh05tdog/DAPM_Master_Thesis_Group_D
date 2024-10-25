using System.Net.Http.Json;
using DAPM.AccessControlService.Test.EndToEnd.Dtos;
using DAPM.AccessControlService.Test.EndToEnd.Requests;
using DAPM.AccessControlService.Test.EndToEnd.Responses;
using DAPM.AccessControlService.Test.EndToEnd.Utilities;

namespace DAPM.AccessControlService.Test.EndToEnd;

[Collection("TestCollection")]
public class RepositoryTests(TestFixture fixture)
{ 
    private readonly IHttpClientFactory httpClientFactory = fixture.HttpClientFactory;

    [Fact]
    public async Task AddUserRepositorySucceeds()
    {
        using var client = httpClientFactory.CreateClient();
        var request = new AddUserRepositoryRequestMessage
        {
            User = new UserDto
            {
                Id = Guid.NewGuid()
            },
            Repository = new RepositoryDto
            {
                Id = Guid.NewGuid()
            }
        };
        
        var response = await client.PostAsJsonAsync(TestFixture.AddUserRepositoryRoute , request);
        var result = await response.Content.ReadFromJsonAsync<AddUserRepositoryResponseMessage>();
        
        Assert.True(result.Success);
    }
    
    [Fact]
    public async Task AddRepositoryForUserAndGetRepositoriesForUserReturnsRepository()
    {
        using var client = httpClientFactory.CreateClient();
        
        var addUserRepository = new AddUserRepositoryRequestMessage
        {
            User = new UserDto
            {
                Id = Guid.NewGuid()
            },
            Repository = new RepositoryDto
            {
                Id = Guid.NewGuid()
            }
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserRepositoryRoute , addUserRepository);
        
        var request = new GetRepositoriesForUserRequestMessage
        {
            User = new UserDto
            {
                Id = addUserRepository.User.Id
            }
        };
        
        var response = await client.GetAsync($"{TestFixture.GetUserRepositoriesRoute}/{request.User.Id}");
        var result = await response.Content.ReadFromJsonAsync<GetRepositoriesForUserResponseMessage>();
        
        Assert.True(result.Repositories.Any(r => r.Id == addUserRepository.Repository.Id));
    }
    
    [Fact]
    public async Task AddRepositoryForUserAndGetRepositoriesForUserReturnsRepository2()
    {
        using var client = httpClientFactory.CreateClient();
        
        var addUserRepository = new AddUserRepositoryRequestMessage
        {
            User = new UserDto
            {
                Id = Guid.NewGuid()
            },
            Repository = new RepositoryDto
            {
                Id = Guid.NewGuid()
            }
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserRepositoryRoute , addUserRepository);
        
        var request = new GetRepositoriesForUserRequestMessage
        {
            User = new UserDto
            {
                Id = addUserRepository.User.Id
            }
        };
        
        var response = await client.GetAsync($"{TestFixture.GetUserRepositoriesRoute}/{request.User.Id}");
        var result = await response.Content.ReadFromJsonAsync<GetRepositoriesForUserResponseMessage>();
        
        Assert.True(result.Repositories.Any(r => r.Id == addUserRepository.Repository.Id));
    }
    
    [Fact]
    public async Task RemoveRepositoryForUserAndGetRepositoriesForUserReturnsNoRepository()
    {
        using var client = httpClientFactory.CreateClient();
        
        var addUserRepository = new AddUserRepositoryRequestMessage
        {
            User = new UserDto
            {
                Id = Guid.NewGuid()
            },
            Repository = new RepositoryDto
            {
                Id = Guid.NewGuid()
            }
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserRepositoryRoute , addUserRepository);
        
        var request = new RemoveUserRepositoryRequestMessage
        {
            User = new UserDto
            {
                Id = addUserRepository.User.Id
            },
            Repository = new RepositoryDto
            {
                Id = addUserRepository.Repository.Id
            }
        };
        
        await client.PostAsJsonAsync(TestFixture.RemoveUserRepositoryRoute , request);
        
        var response = await client.GetAsync($"{TestFixture.GetUserRepositoriesRoute}/{request.User.Id}");
        var result = await response.Content.ReadFromJsonAsync<GetRepositoriesForUserResponseMessage>();
        
        Assert.False(result.Repositories.Any(r => r.Id == addUserRepository.Repository.Id));
    }
    
    [Fact]
    public async Task GetAllUserRepositoriesReturnsRepositories()
    {
        using var client = httpClientFactory.CreateClient();
        
        var addUserRepository = new AddUserRepositoryRequestMessage
        {
            User = new UserDto
            {
                Id = Guid.NewGuid()
            },
            Repository = new RepositoryDto
            {
                Id = Guid.NewGuid()
            }
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserRepositoryRoute , addUserRepository);
        
        var response = await client.GetAsync(TestFixture.GetAllUserRepositoriesRoute);
        var result = await response.Content.ReadFromJsonAsync<GetAllUserRepositoriesResponseMessage>();
        
        Assert.Contains(result.Repositories, p => p.RepositoryId == addUserRepository.Repository.Id);
    }
}