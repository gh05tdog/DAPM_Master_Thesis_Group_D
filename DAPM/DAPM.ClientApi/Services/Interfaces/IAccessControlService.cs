using DAPM.AccessControlService.Core.Dtos;

namespace DAPM.ClientApi.Services.Interfaces;

public interface IAccessControlService
{
    Task<bool> UserHasAccessToPipeline(UserDto user, PipelineDto pipeline);
    Task<bool> UserHasAccessToRepository(UserDto user, RepositoryDto repository);
    Task<bool> UserHasAccessToResource(UserDto user, ResourceDto resource);
    Task<bool> AddUserToPipeline(UserDto user, PipelineDto pipeline);
    Task<bool> AddUserToResource(UserDto user, ResourceDto resource);
    Task<bool> AddUserToRepository(UserDto user, RepositoryDto repository);
}