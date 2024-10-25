using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Domain.Repositories;

public interface IOrganizationRepository
{
    Task AddUserOrganization(UserId userId, OrganizationId organizationId);
    Task<ICollection<OrganizationId>> GetOrganizationsForUser(UserId userId);
}