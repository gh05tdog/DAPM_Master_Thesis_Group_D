using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Domain.Repositories;

public interface IPipelineRepository
{
    Task AddUserPipeline(UserId userId, PipelineId pipelineId);
    Task<ICollection<PipelineId>> GetPipelinesForUser(UserId userId);
}