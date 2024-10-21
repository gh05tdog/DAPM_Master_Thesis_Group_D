using DAPM.AccessControlService.Core.Services.Abstractions;
using RabbitMQLibrary.Messages.AccessControl.Requests;
using RabbitMQLibrary.Messages.AccessControl.Responses;

namespace DAPM.AccessControlService.Infrastructure;

public class AccessControlFacade : IAccessControlFacade
{
    private readonly IPipelineService pipelineService;
    private readonly IResourceService resourceService;
    private readonly IRepositoryService repositoryService;
    
    public AccessControlFacade(IPipelineService pipelineService, IResourceService resourceService, IRepositoryService repositoryService)
    {
        this.pipelineService = pipelineService;
        this.resourceService = resourceService;
        this.repositoryService = repositoryService;
    }

    public async Task<AddUserPipelineResponseMessage> AddUserPipeline(AddUserPipelineRequestMessage message)
    {
        await pipelineService.AddUserPipeline(message.User, message.Pipeline);
        return new AddUserPipelineResponseMessage()
        {
            Success = true
        };
    }

    public async Task<AddUserRepositoryResponseMessage> AddUserRepository(AddUserRepositoryRequestMessage message)
    {
        await repositoryService.AddUserRepository(message.User, message.Repository);
        return new AddUserRepositoryResponseMessage()
        {
            Success = true
        };
    }

    public async Task<AddUserResourceResponseMessage> AddUserResource(AddUserResourceRequestMessage message)
    {
        await resourceService.AddUserResource(message.User, message.Resource);
        return new AddUserResourceResponseMessage()
        {
            Success = true
        };
    }

    public async Task<GetPipelinesForUserResponseMessage> GetPipelinesForUser(GetPipelinesForUserRequestMessage message)
    {
        var pipelines = await pipelineService.GetPipelinesForUser(message.User);
        return new GetPipelinesForUserResponseMessage()
        {
            Pipelines = pipelines
        };
    }

    public async Task<GetRepositoriesForUserResponseMessage> GetRepositoriesForUser(GetRepositoriesForUserRequestMessage message)
    {
        var repositories = await repositoryService.GetRepositoriesForUser(message.User);
        return new GetRepositoriesForUserResponseMessage()
        {
            Repositories = repositories
        };
    }

    public async Task<GetResourcesForUserResponseMessage> GetResourcesForUser(GetResourcesForUserRequestMessage message)
    {
        var resources = await resourceService.GetResourcesForUser(message.User);
        return new GetResourcesForUserResponseMessage()
        {
            Resources = resources
        };
    }
}