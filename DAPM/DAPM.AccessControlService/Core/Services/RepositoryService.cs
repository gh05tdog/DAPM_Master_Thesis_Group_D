using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Core.Dtos;
using DAPM.AccessControlService.Core.DTOs;
using DAPM.AccessControlService.Core.Services.Abstractions;

namespace DAPM.AccessControlService.Core.Services;

public class RepositoryService : IRepositoryService
{
    private readonly IRepositoryRepository repositoryRepository;

    public RepositoryService(IRepositoryRepository repositoryRepository)
    {
        this.repositoryRepository = repositoryRepository;
    }

    public Task AddUserRepository(UserDto user, RepositoryDto repository)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<RepositoryDto>> GetRepositoriesForUser(UserDto user)
    {
        throw new NotImplementedException();
    }
}