using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Core.Services.Abstractions;

public interface IOrganizationService
{
    Task<bool> AddUserOrganization(UserOrganizationDto userOrganization);
    Task<ICollection<OrganizationDto>> GetOrganizationsForUser(UserDto user);
    Task<bool> RemoveUserOrganization(UserOrganizationDto userOrganization);
    Task<ICollection<UserOrganizationDto>> GetAllUserOrganizations();
}