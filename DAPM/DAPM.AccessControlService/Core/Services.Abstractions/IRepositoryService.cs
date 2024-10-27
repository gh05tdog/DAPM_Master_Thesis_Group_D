using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Core.Services.Abstractions;

public interface IRepositoryService
{
    Task<bool> AddUserRepository(UserRepositoryDto userRepository);
    Task<ICollection<RepositoryDto>> GetRepositoriesForUser(UserDto user);
    Task<bool> RemoveUserRepository(UserRepositoryDto userRepository);
    Task<ICollection<UserRepositoryDto>> GetAllUserRepositories();
    Task<bool> UserHasAccessToRepository(UserRepositoryDto userRepository);
}