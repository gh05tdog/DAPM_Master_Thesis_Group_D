using DAPM.AccessControlService.Core.Domain.Queries;
using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Core.Extensions;
using DAPM.AccessControlService.Core.Services.Abstractions;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Core.Services;

public class RepositoryService : IRepositoryService
{
    private readonly IRepositoryRepository repositoryRepository;
    private readonly IUserRepositoryQueries userRepositoryQueries;

    public RepositoryService(IRepositoryRepository repositoryRepository, IUserRepositoryQueries userRepositoryQueries)
    {
        this.repositoryRepository = repositoryRepository;
        this.userRepositoryQueries = userRepositoryQueries;
    }

    public async Task<bool> AddUserRepository(UserRepositoryDto userRepository)
    {
        await repositoryRepository.CreateUserRepository(userRepository.ToUserRepository());
        return true;
    }

    public async Task<ICollection<RepositoryDto>> GetRepositoriesForUser(UserDto user)
    {
        var userId = user.ToUserId();
        var repositoryIds = await repositoryRepository.ReadRepositoriesForUser(userId);
        return repositoryIds.Select(r => new RepositoryDto{Id = r.Id}).ToList();
    }
    
    public async Task<bool> RemoveUserRepository(UserRepositoryDto userRepository)
    {
        await repositoryRepository.DeleteUserRepository(userRepository.ToUserRepository());
        return true;
    }
    
    public async Task<ICollection<UserRepositoryDto>> GetAllUserRepositories()
    {
        var userRepositories = await repositoryRepository.ReadAllUserRepositories();
        return userRepositories.Select(ur => new UserRepositoryDto
        {
            UserId = ur.UserId.Id,
            RepositoryId = ur.RepositoryId.Id
        }).ToList();
    }

    public async Task<bool> UserHasAccessToRepository(UserRepositoryDto userRepository)
    {
        return await userRepositoryQueries.UserHasAccessToRepository(userRepository.ToUserRepository());
    }
}