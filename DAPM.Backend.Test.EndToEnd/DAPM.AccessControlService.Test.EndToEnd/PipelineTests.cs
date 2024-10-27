using System.Net.Http.Json;
using DAPM.AccessControlService.Test.EndToEnd.Dtos;
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
        
        var request = new UserPipelineDto
        {
            UserId = Guid.NewGuid(),
            PipelineId = Guid.NewGuid()
        };
        
        var response = await client.PostAsJsonAsync(TestFixture.AddUserPipelineRoute , request);
        var result = await response.Content.ReadFromJsonAsync<bool>();
        
        Assert.True(result);
    }
    
    [Fact]
    public async Task AddPipelineForUserAndGetPipelinesForUserReturnsPipeline()
    {
        using var client = httpClientFactory.CreateClient();
        
        var addUserPipeline = new UserPipelineDto
        {
            UserId = Guid.NewGuid(),
            PipelineId = Guid.NewGuid()
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserPipelineRoute , addUserPipeline);
        
        var response = await client.GetAsync($"{TestFixture.GetUserPipelinesRoute}/{addUserPipeline.UserId}");
        var result = await response.Content.ReadFromJsonAsync<ICollection<PipelineDto>>();
        
        Assert.Contains(result, p => p.Id == addUserPipeline.PipelineId);
    }
    
    [Fact]
    public async Task AddPipelineForUserAndGetPipelinesForOtherUserReturnsNoPipelines()
    {
        using var client = httpClientFactory.CreateClient();
        
        var addUserPipeline = new UserPipelineDto
        {
            UserId = Guid.NewGuid(),
            PipelineId = Guid.NewGuid()
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserPipelineRoute, addUserPipeline);
        
        var response = await client.GetAsync($"{TestFixture.GetUserPipelinesRoute}/{Guid.NewGuid()}");
        var result = await response.Content.ReadFromJsonAsync<ICollection<PipelineDto>>();
        
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task RemoveUserPipelineSucceeds()
    {
        using var client = httpClientFactory.CreateClient();
        
        var addUserPipeline = new UserPipelineDto
        {
            UserId = Guid.NewGuid(),
            PipelineId = Guid.NewGuid()
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserPipelineRoute , addUserPipeline);
        
        var request = new UserPipelineDto
        {
            UserId = addUserPipeline.UserId, 
            PipelineId = addUserPipeline.PipelineId 
        };
        
        var response = await client.PostAsJsonAsync(TestFixture.RemoveUserPipelineRoute , request);
        var result = await response.Content.ReadFromJsonAsync<bool>();
        
        Assert.True(result);
    }
    
    [Fact]
    public async Task GetAllUserPipelinesReturnsPipelines()
    {
        using var client = httpClientFactory.CreateClient();
        
        var addUserPipeline = new UserPipelineDto
        {
            UserId = Guid.NewGuid(),
            PipelineId = Guid.NewGuid()
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserPipelineRoute , addUserPipeline);
        
        var response = await client.GetAsync(TestFixture.GetAllUserPipelinesRoute);
        var result = await response.Content.ReadFromJsonAsync<ICollection<UserPipelineDto>>();
        
        Assert.Contains(result, p => 
            p.PipelineId == addUserPipeline.PipelineId && 
            p.UserId == addUserPipeline.UserId);
    }
    
    [Fact]
    public async Task RemovePipelineForUserAndGetPipelinesForUserReturnsNoPipelines()
    {
        using var client = httpClientFactory.CreateClient();
        
        var addUserPipeline = new UserPipelineDto
        {
            UserId = Guid.NewGuid(),
            PipelineId = Guid.NewGuid()
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserPipelineRoute , addUserPipeline);
        
        var request = new UserPipelineDto
        {
            UserId = addUserPipeline.UserId,
            PipelineId = addUserPipeline.PipelineId
        };
        
        await client.PostAsJsonAsync(TestFixture.RemoveUserPipelineRoute , request);
        
        var response = await client.GetAsync($"{TestFixture.GetUserPipelinesRoute}/{addUserPipeline.UserId}");
        var result = await response.Content.ReadFromJsonAsync<ICollection<PipelineDto>>();
        
        Assert.DoesNotContain(addUserPipeline.PipelineId, result.Select(p => p.Id));
    }
}