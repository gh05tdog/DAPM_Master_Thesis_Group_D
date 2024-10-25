using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Core.Services.Abstractions;

public interface IPipelineService
{
    Task<bool> AddUserPipeline(UserDto user, PipelineDto pipeline);
    Task<ICollection<PipelineDto>> GetPipelinesForUser(UserDto user); 
    Task<bool> RemoveUserPipeline(UserDto user, PipelineDto pipeline);
}