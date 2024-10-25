using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Core.Extensions;
using DAPM.AccessControlService.Core.Services.Abstractions;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Core.Services;

public class RepositoryService : IRepositoryService
{
    private readonly IRepositoryRepository repositoryRepository;

    public RepositoryService(IRepositoryRepository repositoryRepository)
    {
        this.repositoryRepository = repositoryRepository;
    }

    public async Task<bool> AddUserRepository(UserDto user, RepositoryDto repository)
    {
        var userId = user.ToUserId();
        var repositoryId = repository.ToRepositoryId();
        await repositoryRepository.CreateUserRepository(new UserRepository(userId, repositoryId));
        return true;
    }

    public async Task<ICollection<RepositoryDto>> GetRepositoriesForUser(UserDto user)
    {
        var userId = user.ToUserId();
        var repositoryIds = await repositoryRepository.ReadRepositoriesForUser(userId);
        return repositoryIds.Select(r => new RepositoryDto{Id = r.Id}).ToList();
    }
}