using RabbitMQLibrary.Messages.AccessControl.Requests;
using RabbitMQLibrary.Messages.AccessControl.Responses;

namespace DAPM.AccessControlService.Infrastructure;

public interface IAccessControlFacade
{
    Task<AddUserPipelineResponseMessage> AddUserPipeline(AddUserPipelineRequestMessage message);
    Task<AddUserRepositoryResponseMessage> AddUserRepository(AddUserRepositoryRequestMessage message);
    Task<AddUserResourceResponseMessage> AddUserResource(AddUserResourceRequestMessage message);
    Task<AddUserOrganizationResponseMessage> AddUserOrganization(AddUserOrganizationRequestMessage message);
    Task<GetPipelinesForUserResponseMessage> GetPipelinesForUser(GetPipelinesForUserRequestMessage message);
    Task<GetRepositoriesForUserResponseMessage> GetRepositoriesForUser(GetRepositoriesForUserRequestMessage message);
    Task<GetResourcesForUserResponseMessage> GetResourcesForUser(GetResourcesForUserRequestMessage message);
    Task<GetOrganizationsForUserResponseMessage> GetOrganizationsForUser(GetOrganizationsForUserRequestMessage message);
    Task<RemoveUserPipelineResponseMessage> RemoveUserPipeline(RemoveUserPipelineRequestMessage message);
    Task<RemoveUserRepositoryResponseMessage> RemoveUserRepository(RemoveUserRepositoryRequestMessage message);
    Task<RemoveUserResourceResponseMessage> RemoveUserResource(RemoveUserResourceRequestMessage message);
    Task<RemoveUserOrganizationResponseMessage> RemoveUserOrganization(RemoveUserOrganizationRequestMessage message);
    Task<GetAllUserOrganizationsResponseMessage> GetAllUserOrganizations();
    Task<GetAllUserPipelinesResponseMessage> GetAllUserPipelines();
    Task<GetAllUserRepositoriesResponseMessage> GetAllUserRepositories();
    Task<GetAllUserResourcesResponseMessage> GetAllUserResources();
}