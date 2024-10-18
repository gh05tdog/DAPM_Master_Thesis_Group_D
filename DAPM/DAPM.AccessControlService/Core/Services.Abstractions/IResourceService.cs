using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Core.Services.Abstractions;

public interface IResourceService
{
    Task<bool> AddUserResource(UserDto user, ResourceDto resource);
    Task<ICollection<ResourceDto>> GetResourcesForUser(UserDto user);
}