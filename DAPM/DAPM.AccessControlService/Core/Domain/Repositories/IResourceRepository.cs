using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Domain.Repositories;

public interface IResourceRepository : IRepository
{
    Task AddUserResource(UserId userId, ResourceId resourceId);
    Task<ICollection<ResourceId>> GetResourcesForUser(UserId userId);
}