using System.Net.Http.Json;
using DAPM.AccessControlService.Test.EndToEnd.Dtos;

namespace DAPM.AccessControlService.Test.EndToEnd.Utilities;

public class AccessControlAdder
{
    private readonly IHttpClientFactory httpClientFactory;

    public AccessControlAdder(Uri baseUri)
    {
        this.httpClientFactory = new HttpClientFactory(baseUri);
    }
    
    public AccessControlAdder(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }
    
    public async Task AddUserResourceAsync(Guid userId, Guid resourceId)
    {
        using var client = httpClientFactory.CreateClient();
        var request = new UserResourceDto
        {
            UserId = userId,
            ResourceId = resourceId
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserResourceRoute , request);
    }
    
    public async Task AddUserPipelineAsync(Guid userId, Guid pipelineId)
    {
        using var client = httpClientFactory.CreateClient();
        var request = new UserPipelineDto
        {
            UserId = userId,
            PipelineId = pipelineId
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserPipelineRoute , request);
    }
    
    public async Task AddUserRepositoryAsync(Guid userId, Guid repositoryId)
    {
        using var client = httpClientFactory.CreateClient();
        var request = new UserRepositoryDto
        {
            UserId = userId,
            RepositoryId = repositoryId
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserRepositoryRoute , request);
    }
    
    public async Task AddUserOrganizationAsync(Guid userId, Guid organizationId)
    {
        using var client = httpClientFactory.CreateClient();
        var request = new UserOrganizationDto
        {
            UserId = userId,
            OrganizationId = organizationId
        };
        
        await client.PostAsJsonAsync(TestFixture.AddUserOrganizationRoute , request);
    }
}