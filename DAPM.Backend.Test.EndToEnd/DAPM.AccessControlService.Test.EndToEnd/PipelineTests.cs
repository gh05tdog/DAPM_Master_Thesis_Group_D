using System.Net.Http.Json;
using DAPM.AccessControlService.Test.EndToEnd.Dtos;
using DAPM.AccessControlService.Test.EndToEnd.Requests;
using DAPM.AccessControlService.Test.EndToEnd.Responses;
using DAPM.AccessControlService.Test.EndToEnd.Utilities;

namespace DAPM.AccessControlService.Test.EndToEnd;

[Collection("TestCollection")]
public class PipelineTests(TestFixture fixture)
{ 
    private readonly IHttpClientFactory httpClientFactory = fixture.HttpClientFactory;
    
    [Fact]
    public async Task AddUserPipelineSucceeds()
    {
        using var client = httpClientFactory.CreateClient();
        var request = new AddUserPipelineRequestMessage
        {
            User = new UserDto
            {
                Id = Guid.NewGuid()
            },
            Pipeline = new PipelineDto
            {
                Id = Guid.NewGuid()
            }
        };
        
        var response = await client.PostAsJsonAsync(TestFixture.AddUserPipelineRoute , request);
        var result = await response.Content.ReadFromJsonAsync<AddUserPipelineResponseMessage>();
        
        Assert.True(result.Success);
    }
    
    [Fact]
    public async Task AddPipelineForUserAndGetPipelinesForUserReturnsPipeline()
    {
        using var client = httpClientFactory.CreateClient();
        
        var addUserPipeline = new AddUserPipelineRequestMessage
        {
            User = new UserDto
            {
                Id = Guid.NewGuid()
            },
            Pipeline = new PipelineDto
            {
                Id = Guid.NewGuid()
            }
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserPipelineRoute , addUserPipeline);
        
        var request = new GetPipelinesForUserRequestMessage
        {
            User = new UserDto
            {
                Id = addUserPipeline.User.Id
            }
        };
        
        var response = await client.GetAsync($"{TestFixture.GetUserPipelinesRoute}/{request.User.Id}");
        var result = await response.Content.ReadFromJsonAsync<GetPipelinesForUserResponseMessage>();
        
        Assert.Contains(result.Pipelines, p => p.Id == addUserPipeline.Pipeline.Id);
    }
    
    [Fact]
    public async Task AddPipelineForUserAndGetPipelinesForOtherUserReturnsNoPipelines()
    {
        using var client = httpClientFactory.CreateClient();
        
        var addUserPipeline = new AddUserPipelineRequestMessage
        {
            User = new UserDto
            {
                Id = Guid.NewGuid()
            },
            Pipeline = new PipelineDto
            {
                Id = Guid.NewGuid()
            }
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserPipelineRoute , addUserPipeline);
        
        var request = new GetPipelinesForUserRequestMessage
        {
            User = new UserDto
            {
                Id = Guid.NewGuid()
            }
        };
        
        var response = await client.GetAsync($"{TestFixture.GetUserPipelinesRoute}/{request.User.Id}");
        var result = await response.Content.ReadFromJsonAsync<GetPipelinesForUserResponseMessage>();
        
        Assert.Empty(result.Pipelines);
    }
    
    [Fact]
    public async Task RemoveUserPipelineSucceeds()
    {
        using var client = httpClientFactory.CreateClient();
        
        var addUserPipeline = new AddUserPipelineRequestMessage
        {
            User = new UserDto
            {
                Id = Guid.NewGuid()
            },
            Pipeline = new PipelineDto
            {
                Id = Guid.NewGuid()
            }
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserPipelineRoute , addUserPipeline);
        
        var request = new RemoveUserPipelineRequestMessage
        {
            User = new UserDto
            {
                Id = addUserPipeline.User.Id
            },
            Pipeline = new PipelineDto
            {
                Id = addUserPipeline.Pipeline.Id
            }
        };
        
        var response = await client.PostAsJsonAsync(TestFixture.RemoveUserPipelineRoute , request);
        var result = await response.Content.ReadFromJsonAsync<RemoveUserPipelineResponseMessage>();
        
        Assert.True(result.Success);
    }
}