using RabbitMQLibrary.Messages.AccessControl.Requests;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.ClientApi.AccessControl;

public class AccessControlService(IApiHttpClient apiHttpClient) : IAccessControlService
{
    public async Task<ICollection<PipelineDto>> GetUserPipelines(UserDto user)
    {
        var message = new GetPipelinesForUserRequestMessage
        {
            User = user
        };

        return (await apiHttpClient.GetPipelinesForUserAsync(message)).Pipelines;
    }

    public async Task<ICollection<RepositoryDto>> GetUserRepositories(UserDto user)
    {
        var message = new GetRepositoriesForUserRequestMessage
        {
            User = user
        };
        
        return (await apiHttpClient.GetRepositoriesForUserAsync(message)).Repositories;
    }

    public async Task<ICollection<ResourceDto>> GetUserResources(UserDto user)
    {
        var message = new GetResourcesForUserRequestMessage
        {
            User = user
        };
        
        return (await apiHttpClient.GetResourcesForUserAsync(message)).Resources;
    }

    public async Task<bool> AddUserToPipeline(UserDto user, PipelineDto pipeline)
    {
        var message = new AddUserPipelineRequestMessage
        {
            User = user,
            Pipeline = pipeline
        };
        
        return (await apiHttpClient.AddUserPipelineAsync(message)).Success;
    }

    public async Task<bool> AddUserToResource(UserDto user, ResourceDto resource)
    {
        var message = new AddUserResourceRequestMessage
        {
            User = user,
            Resource = resource
        };

        return (await apiHttpClient.AddUserResourceAsync(message)).Success;
    }

    public async Task<bool> AddUserToRepository(UserDto user, RepositoryDto repository)
    {
        var message = new AddUserRepositoryRequestMessage
        {
            User = user,
            Repository = repository
        };
        
        return (await apiHttpClient.AddUserRepositoryAsync(message)).Success;
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
}