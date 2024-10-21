using RabbitMQLibrary.Messages.AccessControl.Requests;
using RabbitMQLibrary.Messages.AccessControl.Responses;

namespace DAPM.AccessControlService.Infrastructure;

public interface IAccessControlFacade
{
    Task<AddUserPipelineResponseMessage> AddUserPipeline(AddUserPipelineRequestMessage message);
    Task<AddUserRepositoryResponseMessage> AddUserRepository(AddUserRepositoryRequestMessage message);
    Task<AddUserResourceResponseMessage> AddUserResource(AddUserResourceRequestMessage message);
    Task<GetPipelinesForUserResponseMessage> GetPipelinesForUser(GetPipelinesForUserRequestMessage message);
    Task<GetRepositoriesForUserResponseMessage> GetRepositoriesForUser(GetRepositoriesForUserRequestMessage message);
    Task<GetResourcesForUserResponseMessage> GetResourcesForUser(GetResourcesForUserRequestMessage message);
}