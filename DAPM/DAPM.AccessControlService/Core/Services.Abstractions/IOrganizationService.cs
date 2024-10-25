using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Core.Services.Abstractions;

public interface IOrganizationService
{
    Task<bool> AddUserOrganization(UserDto user, OrganizationDto organization);
    Task<ICollection<OrganizationDto>> GetOrganizationsForUser(UserDto user);
}