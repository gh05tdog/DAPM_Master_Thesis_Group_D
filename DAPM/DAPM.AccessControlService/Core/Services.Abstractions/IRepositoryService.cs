using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Core.Services.Abstractions;

public interface IRepositoryService
{
    Task<bool> AddUserRepository(UserDto user, RepositoryDto repository);
    Task<ICollection<RepositoryDto>> GetRepositoriesForUser(UserDto user);
    Task<bool> RemoveUserRepository(UserDto user, RepositoryDto repository);
    Task<ICollection<UserRepositoryDto>> GetAllUserRepositories();
}