using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Domain.Repositories;

public interface IRepositoryRepository : IRepository
{
    Task AddUserRepository(UserId userId, RepositoryId repositoryId);
    Task<ICollection<RepositoryId>> GetRepositoriesForUser(UserId userId);
}