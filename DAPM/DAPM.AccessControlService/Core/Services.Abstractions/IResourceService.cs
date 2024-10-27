using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Core.Services.Abstractions;

public interface IResourceService
{
    Task<bool> AddUserResource(UserResourceDto userResource);
    Task<ICollection<ResourceDto>> GetResourcesForUser(UserDto user);
    Task<bool> RemoveUserResource(UserResourceDto userResource);
    Task<ICollection<UserResourceDto>> GetAllUserResources();
    Task<bool> UserHasAccessToResource(UserResourceDto userResource);
}