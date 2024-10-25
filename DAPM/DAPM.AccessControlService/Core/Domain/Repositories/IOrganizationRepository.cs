using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Domain.Repositories;

public interface IOrganizationRepository
{
    Task CreateUserOrganization(UserOrganization userOrganization);
    Task<ICollection<OrganizationId>> ReadOrganizationsForUser(UserId userId);
    Task DeleteUserOrganization(UserOrganization userOrganization);
}