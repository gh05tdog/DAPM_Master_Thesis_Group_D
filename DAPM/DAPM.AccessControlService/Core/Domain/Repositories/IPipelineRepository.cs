using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Domain.Repositories;

public interface IPipelineRepository
{
    Task CreateUserPipeline(UserId userId, PipelineId pipelineId);
    Task<ICollection<PipelineId>> ReadPipelinesForUser(UserId userId);
    
}