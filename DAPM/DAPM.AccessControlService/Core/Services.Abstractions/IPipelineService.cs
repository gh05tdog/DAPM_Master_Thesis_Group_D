using DAPM.AccessControlService.Core.Dtos;

namespace DAPM.AccessControlService.Core.Services.Abstractions;

public interface IPipelineService
{
    Task AddUserPipeline(UserDto user, PipelineDto pipeline);
    Task<ICollection<PipelineDto>> GetPipelinesForUser(UserDto user); 
}