using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Domain.Queries;

public interface IUserPipelineQueries
{
    Task<bool> UserHasAccessToPipeline(UserPipeline userPipeline);
}