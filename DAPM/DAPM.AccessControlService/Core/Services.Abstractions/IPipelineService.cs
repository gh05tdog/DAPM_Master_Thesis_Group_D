using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Core.Services.Abstractions;

public interface IPipelineService
{
    Task<bool> AddUserPipeline(UserPipelineDto userPipeline);
    Task<ICollection<PipelineDto>> GetPipelinesForUser(UserDto user); 
    Task<bool> RemoveUserPipeline(UserPipelineDto userPipeline);
    Task<ICollection<UserPipelineDto>> GetAllUserPipelines();
}