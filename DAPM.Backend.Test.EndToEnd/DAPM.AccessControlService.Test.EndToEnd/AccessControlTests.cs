using System.Net;
using System.Net.Http.Json;
using DAPM.AccessControlService.Test.EndToEnd.Dtos;
using DAPM.AccessControlService.Test.EndToEnd.Utilities;

namespace DAPM.AccessControlService.Test.EndToEnd;

[Collection("TestCollection")]
public class AccessControlTests(TestFixture fixture)
{
    private readonly IHttpClientFactory httpClientFactory = fixture.HttpClientFactory;
    private readonly AccessControlAdder accessControlAdder = fixture.AccessControlAdder;
    
    [Fact]
    public async Task UserHasAccess_WhenUserHasAccessToAll_ReturnsTrue()
    {
        var userId = Guid.NewGuid();
        var resourceId = Guid.NewGuid();
        var pipelineId = Guid.NewGuid();
        var repositoryId = Guid.NewGuid();
        var organizationId = Guid.NewGuid();
        
        await accessControlAdder.AddUserResourceAsync(userId, resourceId);
        await accessControlAdder.AddUserPipelineAsync(userId, pipelineId);
        await accessControlAdder.AddUserRepositoryAsync(userId, repositoryId);
        await accessControlAdder.AddUserOrganizationAsync(userId, organizationId);
        
        var client = httpClientFactory.CreateClient();

        var request = new UserAccessRequestDto
        {
            Organization = new OrganizationDto { Id = organizationId },
            Repository = new RepositoryDto { Id = repositoryId },
            Pipeline = new PipelineDto { Id = pipelineId },
            Resource = new ResourceDto { Id = resourceId },
            User = new UserDto { Id = userId }
        };
        
        var response = await client.PostAsJsonAsync(TestFixture.AccessControlCheckAccessRoute, request);
        var result = await response.Content.ReadFromJsonAsync<UserAccessResponseDto>();
        
        Assert.True(result.HasOrganizationAccess);
        Assert.True(result.HasRepositoryAccess);
        Assert.True(result.HasPipelineAccess);
        Assert.True(result.HasResourceAccess);
    }
    
    [Fact]
    public async Task UserHasAccess_WhenUserHasNoAccessToAny_ReturnsFalse()
    {
        var userId = Guid.NewGuid();
        var resourceId = Guid.NewGuid();
        var pipelineId = Guid.NewGuid();
        var repositoryId = Guid.NewGuid();
        var organizationId = Guid.NewGuid();
        
        var client = httpClientFactory.CreateClient();

        var request = new UserAccessRequestDto
        {
            Organization = new OrganizationDto { Id = organizationId },
            Repository = new RepositoryDto { Id = repositoryId },
            Pipeline = new PipelineDto { Id = pipelineId },
            Resource = new ResourceDto { Id = resourceId },
            User = new UserDto { Id = userId }
        };
        
        var response = await client.PostAsJsonAsync(TestFixture.AccessControlCheckAccessRoute, request);
        var result = await response.Content.ReadFromJsonAsync<UserAccessResponseDto>();
        
        Assert.False(result.HasOrganizationAccess);
        Assert.False(result.HasRepositoryAccess);
        Assert.False(result.HasPipelineAccess);
        Assert.False(result.HasResourceAccess);
    }

    [Fact]
    public async Task UserHasAccess_UserIsNull_ReturnsBadRequest()
    {
        var client = httpClientFactory.CreateClient();

        var request = new UserAccessRequestDto
        {
            Organization = new OrganizationDto { Id = Guid.NewGuid() },
            Repository = new RepositoryDto { Id = Guid.NewGuid() },
            Pipeline = new PipelineDto { Id = Guid.NewGuid() },
            Resource = new ResourceDto { Id = Guid.NewGuid() },
            User = null
        };
        
        var response = await client.PostAsJsonAsync(TestFixture.AccessControlCheckAccessRoute, request);
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}