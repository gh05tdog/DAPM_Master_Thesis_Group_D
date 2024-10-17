using DAPM.AccessControlService.Core.Dtos;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;

namespace DAPM.ClientApi.Services.Interfaces;

public interface IAccessControlService
{
    Task<ICollection<PipelineDto>> GetUserPipelines(UserDto user);
    Task<ICollection<RepositoryDto>> GetUserRepositories(UserDto user);
    Task<ICollection<ResourceDto>> GetUserResources(UserDto user);
    Task<bool> AddUserToPipeline(UserDto user, PipelineDto pipeline);
    Task<bool> AddUserToResource(UserDto user, ResourceDto resource);
    Task<bool> AddUserToRepository(UserDto user, RepositoryDto repository);
    void HandleGetPipelinesForUserResponseMessage(GetPipelinesForUserResponseMessage message);
    void HandleGetRepositoriesForUserResponseMessage(GetRepositoriesForUserResponseMessage message);
    void HandleGetResourcesForUserResponseMessage(GetResourcesForUserResponseMessage message);
    void HandleAddUserPipelineResponseMessage(AddUserPipelineResponseMessage message);
    void HandleAddUserRepositoryResponseMessage(AddUserRepositoryResponseMessage message);
    void HandleAddUserResourceResponseMessage(AddUserResourceReponseMessage message);
}