using System.Net.Http.Json;
using TestUtilities.Dtos;

namespace TestUtilities;

public class AccessControlAdder
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly string user;

    public AccessControlAdder(string user)
    {
        this.user = user;
        var fixture = new TestUtility();
        httpClientFactory = fixture.HttpClientFactory;
    }
    
    public async Task AddUserResourceAsync(Guid userId, Guid resourceId)
    {
        using var client = httpClientFactory.CreateAccessControlClient(user);
        var request = new UserResourceDto
        {
            UserId = userId,
            ResourceId = resourceId
        };
        
        await client.PostAsJsonAsync(ApiRoutes.AddUserResourceRoute , request);
    }
    
    public async Task AddUserPipelineAsync(Guid userId, Guid pipelineId)
    {
        using var client = httpClientFactory.CreateAccessControlClient(user);
        var request = new UserPipelineDto
        {
            UserId = userId,
            PipelineId = pipelineId
        };
        
        await client.PostAsJsonAsync(ApiRoutes.AddUserPipelineRoute , request);
    }
    
    public async Task AddUserRepositoryAsync(Guid userId, Guid repositoryId)
    {
        using var client = httpClientFactory.CreateAccessControlClient(user);
        var request = new UserRepositoryDto
        {
            UserId = userId,
            RepositoryId = repositoryId
        };
        
        await client.PostAsJsonAsync(ApiRoutes.AddUserRepositoryRoute , request);
    }
    
    public async Task AddUserOrganizationAsync(Guid userId, Guid organizationId)
    {
        using var client = httpClientFactory.CreateAccessControlClient(user);
        var request = new UserOrganizationDto
        {
            UserId = userId,
            OrganizationId = organizationId
        };
        
        await client.PostAsJsonAsync(ApiRoutes.AddUserOrganizationRoute , request);
    }
}