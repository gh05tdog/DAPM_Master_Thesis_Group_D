using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Core.Services.Abstractions;
using RabbitMQLibrary.Messages.AccessControl.Requests;
using RabbitMQLibrary.Messages.AccessControl.Responses;

namespace DAPM.AccessControlService.Infrastructure;

public class AccessControlFacade : IAccessControlFacade
{
    private readonly IPipelineService pipelineService;
    private readonly IResourceService resourceService;
    private readonly IRepositoryService repositoryService;
    private readonly IOrganizationService organizationService;
    
    public AccessControlFacade(IPipelineService pipelineService, IResourceService resourceService, IRepositoryService repositoryService, IOrganizationService organizationService)
    {
        this.pipelineService = pipelineService;
        this.resourceService = resourceService;
        this.repositoryService = repositoryService;
        this.organizationService = organizationService;
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
    
    public async Task<AddUserOrganizationResponseMessage> AddUserOrganization(AddUserOrganizationRequestMessage message)
    {
        await organizationService.AddUserOrganization(message.User, message.Organization);
        return new AddUserOrganizationResponseMessage()
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
    
    public async Task<GetOrganizationsForUserResponseMessage> GetOrganizationsForUser(GetOrganizationsForUserRequestMessage message)
    {
        var organizations = await organizationService.GetOrganizationsForUser(message.User);
        return new GetOrganizationsForUserResponseMessage()
        {
            Organizations = organizations
        };
    }
    
    public async Task<RemoveUserPipelineResponseMessage> RemoveUserPipeline(RemoveUserPipelineRequestMessage message)
    {
        await pipelineService.RemoveUserPipeline(message.User, message.Pipeline);
        return new RemoveUserPipelineResponseMessage()
        {
            Success = true
        };
    }
    
    public async Task<RemoveUserRepositoryResponseMessage> RemoveUserRepository(RemoveUserRepositoryRequestMessage message)
    {
        await repositoryService.RemoveUserRepository(message.User, message.Repository);
        return new RemoveUserRepositoryResponseMessage()
        {
            Success = true
        };
    }
    
    public async Task<RemoveUserResourceResponseMessage> RemoveUserResource(RemoveUserResourceRequestMessage message)
    {
        await resourceService.RemoveUserResource(message.User, message.Resource);
        return new RemoveUserResourceResponseMessage()
        {
            Success = true
        };
    }
    
    public async Task<RemoveUserOrganizationResponseMessage> RemoveUserOrganization(RemoveUserOrganizationRequestMessage message)
    {
        await organizationService.RemoveUserOrganization(message.User, message.Organization);
        return new RemoveUserOrganizationResponseMessage()
        {
            Success = true
        };
    }
}