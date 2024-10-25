using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Domain.Repositories;

public interface IRepositoryRepository
{
    Task CreateUserRepository(UserRepository userRepository);
    Task<ICollection<RepositoryId>> ReadRepositoriesForUser(UserId userId);
    Task DeleteUserRepository(UserRepository userRepository);
    Task<ICollection<UserRepository>> ReadAllUserRepositories();
}