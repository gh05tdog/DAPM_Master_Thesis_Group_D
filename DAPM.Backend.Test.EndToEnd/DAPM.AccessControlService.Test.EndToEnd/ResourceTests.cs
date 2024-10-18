using System.Net.Http.Json;
using DAPM.AccessControlService.Test.EndToEnd.Dtos;
using DAPM.AccessControlService.Test.EndToEnd.Requests;
using DAPM.AccessControlService.Test.EndToEnd.Responses;
using DAPM.AccessControlService.Test.EndToEnd.Utilities;

namespace DAPM.AccessControlService.Test.EndToEnd;

[Collection("TestCollection")]
public class ResourceTests(TestFixture fixture)
{ 
    private readonly IHttpClientFactory httpClientFactory = fixture.HttpClientFactory;
    
    [Fact]
    public async Task AddUserResourceSucceeds()
    {
        using var client = httpClientFactory.CreateClient();
        var request = new AddUserResourceRequestMessage
        {
            User = new UserDto
            {
                Id = Guid.NewGuid()
            },
            Resource = new ResourceDto
            {
                Id = Guid.NewGuid()
            }
        };
        
        var response = await client.PostAsJsonAsync(TestFixture.AddUserResourceRoute , request);
        var result = await response.Content.ReadFromJsonAsync<AddUserResourceResponseMessage>();
        
        Assert.True(result.Success);
    }
    
    [Fact]
    public async Task AddResourceForUserAndGetResourcesForUserReturnsResource()
    {
        using var client = httpClientFactory.CreateClient();
        
        var addUserResource = new AddUserResourceRequestMessage
        {
            User = new UserDto
            {
                Id = Guid.NewGuid()
            },
            Resource = new ResourceDto
            {
                Id = Guid.NewGuid()
            }
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserResourceRoute , addUserResource);
        
        var request = new GetResourcesForUserRequestMessage
        {
            User = new UserDto
            {
                Id = addUserResource.User.Id
            }
        };
        
        var response = await client.GetAsync($"{TestFixture.GetUserResourcesRoute}/{request.User.Id}");
        var result = await response.Content.ReadFromJsonAsync<GetResourcesForUserResponseMessage>();
        
        Assert.Contains(addUserResource.Resource.Id, result.Resources.Select(r => r.Id));
    }
    
    [Fact]
    public async Task AddResourceForUserAndGetResourcesForUserReturnsResource2()
    {
        using var client = httpClientFactory.CreateClient();
        
        var addUserResource = new AddUserResourceRequestMessage
        {
            User = new UserDto
            {
                Id = Guid.NewGuid()
            },
            Resource = new ResourceDto
            {
                Id = Guid.NewGuid()
            }
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserResourceRoute , addUserResource);
        
        var request = new GetResourcesForUserRequestMessage
        {
            User = new UserDto
            {
                Id = addUserResource.User.Id
            }
        };
        
        var response = await client.GetAsync($"{TestFixture.GetUserResourcesRoute}/{request.User.Id}");
        var result = await response.Content.ReadFromJsonAsync<GetResourcesForUserResponseMessage>();
        
        Assert.Contains(addUserResource.Resource.Id, result.Resources.Select(r => r.Id));
    }
}