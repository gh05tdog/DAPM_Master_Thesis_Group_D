using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Domain.Queries;

public interface IUserOrganizationQueries
{
    Task<bool> UserHasAccessToOrganization(UserOrganization userOrganization);
}