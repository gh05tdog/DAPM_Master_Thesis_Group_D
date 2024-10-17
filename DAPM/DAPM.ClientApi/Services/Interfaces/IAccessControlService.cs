using DAPM.AccessControlService.Core.Dtos;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;

namespace DAPM.ClientApi.Services.Interfaces;

public interface IAccessControlService
{
    Task<bool> UserHasAccessToPipeline(UserDto user, PipelineDto pipeline);
    Task<bool> UserHasAccessToRepository(UserDto user, RepositoryDto repository);
    Task<bool> UserHasAccessToResource(UserDto user, ResourceDto resource);
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