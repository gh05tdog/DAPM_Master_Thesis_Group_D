using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Domain.Queries;

public interface IUserResourceQueries
{
    Task<bool> UserHasAccessToResource(UserResource userResource);
}