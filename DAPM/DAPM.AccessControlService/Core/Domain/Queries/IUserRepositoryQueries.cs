using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Domain.Queries;

public interface IUserRepositoryQueries
{
    Task<bool> UserHasAccessToRepository(UserRepository userRepository);
}