using System.Net.Http.Json;
using DAPM.AccessControlService.Test.EndToEnd.Dtos;
using DAPM.AccessControlService.Test.EndToEnd.Requests;
using DAPM.AccessControlService.Test.EndToEnd.Responses;
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
        var request = new AddUserOrganizationRequestMessage
        {
            User = new UserDto
            {
                Id = Guid.NewGuid()
            },
            Organization = new OrganizationDto
            {
                Id = Guid.NewGuid()
            }
        };
        
        var response = await client.PostAsJsonAsync(TestFixture.AddUserOrganizationRoute , request);
        var result = await response.Content.ReadFromJsonAsync<AddUserOrganizationResponseMessage>();
        
        Assert.True(result.Success);
    }
    
    [Fact]
    public async Task AddOrganizationForUserAndGetOrganizationsForUserReturnsOrganization()
    {
        using var client = httpClientFactory.CreateClient();
        
        var addUserOrganization = new AddUserOrganizationRequestMessage
        {
            User = new UserDto
            {
                Id = Guid.NewGuid()
            },
            Organization = new OrganizationDto
            {
                Id = Guid.NewGuid()
            }
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserOrganizationRoute , addUserOrganization);
        
        var request = new GetOrganizationsForUserRequestMessage
        {
            User = new UserDto
            {
                Id = addUserOrganization.User.Id
            }
        };
        
        var response = await client.GetAsync($"{TestFixture.GetUserOrganizationsRoute}/{request.User.Id}");
        var result = await response.Content.ReadFromJsonAsync<GetOrganizationsForUserResponseMessage>();
        
        Assert.Contains(result.Organizations, o => o.Id == addUserOrganization.Organization.Id);
    }

    [Fact]
    public async Task AddOrganizationForUserAndGetOrganizationsForOtherUserReturnsNoOrganization()
    {
        using var client = httpClientFactory.CreateClient();
        
        var addUserOrganization = new AddUserOrganizationRequestMessage()
        {
            User = new UserDto
            {
                Id = Guid.NewGuid()
            },
            Organization = new OrganizationDto()
            {
                Id = Guid.NewGuid()
            }
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserPipelineRoute , addUserOrganization);
        
        var request = new GetOrganizationsForUserRequestMessage()
        {
            User = new UserDto
            {
                Id = Guid.NewGuid()
            }
        };
        
        var response = await client.GetAsync($"{TestFixture.GetUserOrganizationsRoute}/{request.User.Id}");
        var result = await response.Content.ReadFromJsonAsync<GetOrganizationsForUserResponseMessage>();
        
        Assert.Empty(result.Organizations);
    }
    
    [Fact]
    public async Task RemoveUserOrganizationSucceeds()
    {
        using var client = httpClientFactory.CreateClient();
        
        var addUserOrganization = new AddUserOrganizationRequestMessage
        {
            User = new UserDto
            {
                Id = Guid.NewGuid()
            },
            Organization = new OrganizationDto
            {
                Id = Guid.NewGuid()
            }
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserOrganizationRoute , addUserOrganization);
        
        var request = new RemoveUserOrganizationRequestMessage
        {
            User = new UserDto
            {
                Id = addUserOrganization.User.Id
            },
            Organization = new OrganizationDto
            {
                Id = addUserOrganization.Organization.Id
            }
        };
        
        var response = await client.PostAsJsonAsync(TestFixture.RemoveUserOrganizationRoute , request);
        var result = await response.Content.ReadFromJsonAsync<RemoveUserOrganizationResponseMessage>();
        
        Assert.True(result.Success);
    }
}