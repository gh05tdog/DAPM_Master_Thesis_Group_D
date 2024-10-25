using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Domain.Repositories;

public interface IResourceRepository
{
    Task CreateUserResource(UserId userId, ResourceId resourceId);
    Task<ICollection<ResourceId>> ReadResourcesForUser(UserId userId);
}