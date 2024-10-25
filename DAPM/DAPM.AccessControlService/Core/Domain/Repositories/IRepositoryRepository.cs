using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Domain.Repositories;

public interface IRepositoryRepository
{
    Task CreateUserRepository(UserId userId, RepositoryId repositoryId);
    Task<ICollection<RepositoryId>> ReadRepositoriesForUser(UserId userId);
}