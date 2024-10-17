using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Core.Dtos;
using DAPM.AccessControlService.Core.Services.Abstractions;

namespace DAPM.AccessControlService.Core.Services;

public class RepositoryService : IRepositoryService
{
    private readonly IRepositoryRepository repositoryRepository;

    public RepositoryService(IRepositoryRepository repositoryRepository)
    {
        this.repositoryRepository = repositoryRepository;
    }

    public async Task AddUserRepository(UserDto user, RepositoryDto repository)
    {
        var userId = user.ToUserId();
        var repositoryId = repository.ToRepositoryId();
        await repositoryRepository.AddUserRepository(userId, repositoryId);
    }

    public async Task<ICollection<RepositoryDto>> GetRepositoriesForUser(UserDto user)
    {
        var userId = user.ToUserId();
        var repositoryIds = await repositoryRepository.GetRepositoriesForUser(userId);
        return repositoryIds.Select(r => new RepositoryDto(r)).ToList();
    }
}