using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Domain.Repositories;

public interface IResourceRepository
{
    Task CreateUserResource(UserResource userResource);
    Task<ICollection<ResourceId>> ReadResourcesForUser(UserId userId);
    Task DeleteUserResource(UserResource userResource);
}