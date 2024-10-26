using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.ClientApi.AccessControl;

public class AccessControlService(IApiHttpClient apiHttpClient) : IAccessControlService
{
    public async Task<ICollection<PipelineDto>> GetUserPipelines(UserDto user)
    {
        return await apiHttpClient.GetPipelinesForUserAsync(user);
    }

    public async Task<ICollection<RepositoryDto>> GetUserRepositories(UserDto user)
    {
        return await apiHttpClient.GetRepositoriesForUserAsync(user);
    }

    public async Task<ICollection<ResourceDto>> GetUserResources(UserDto user)
    {
        return await apiHttpClient.GetResourcesForUserAsync(user);
    }
    
    public async Task<ICollection<OrganizationDto>> GetUserOrganizations(UserDto user)
    {
        return await apiHttpClient.GetOrganizationsForUserAsync(user);
    }

    public async Task<bool> AddUserToPipeline(UserDto user, PipelineDto pipeline)
    {
        return await apiHttpClient.AddUserPipelineAsync(new UserPipelineDto{ UserId = user.Id, PipelineId = pipeline.Id });
    }

    public async Task<bool> AddUserToResource(UserDto user, ResourceDto resource)
    {
        return await apiHttpClient.AddUserResourceAsync(new UserResourceDto{ UserId = user.Id, ResourceId = resource.Id });
    }

    public async Task<bool> AddUserToRepository(UserDto user, RepositoryDto repository)
    {
        return await apiHttpClient.AddUserRepositoryAsync(new UserRepositoryDto{ UserId = user.Id, RepositoryId = repository.Id });
    }
    
    public async Task<bool> AddUserToOrganization(UserDto user, OrganizationDto organization)
    {
        return await apiHttpClient.AddUserOrganizationAsync(new UserOrganizationDto{ UserId = user.Id, OrganizationId = organization.Id });
    }

    public async Task<bool> UserHasAccessToPipeline(UserDto user, PipelineDto pipeline)
    {
        var pipelines = await GetUserPipelines(user);
        return pipelines.Any(p => p.Id == pipeline.Id);
    }

    public async Task<bool> UserHasAccessToResource(UserDto user, ResourceDto resource)
    {
        var resources = await GetUserResources(user);
        return resources.Any(r => r.Id == resource.Id);        
    }

    public async Task<bool> UserHasAccessToRepository(UserDto user, RepositoryDto repository)
    {
        var repositories = await GetUserRepositories(user);
        return repositories.Any(r => r.Id == repository.Id);
    }
    
    public async Task<bool> UserHasAccessToOrganization(UserDto user, OrganizationDto organization)
    {
        var organizations = await GetUserOrganizations(user);
        return organizations.Any(o => o.Id == organization.Id);
    }
}