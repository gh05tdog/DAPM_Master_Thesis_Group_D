using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Domain.Repositories;

public interface IOrganizationRepository
{
    Task CreateUserOrganization(UserId userId, OrganizationId organizationId);
    Task<ICollection<OrganizationId>> ReadOrganizationsForUser(UserId userId);
}