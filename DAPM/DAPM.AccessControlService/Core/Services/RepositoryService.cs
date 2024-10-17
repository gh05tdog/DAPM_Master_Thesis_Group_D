using DAPM.AccessControlService.Core.Dtos;
using DAPM.AccessControlService.Core.DTOs;
using DAPM.AccessControlService.Core.Services.Abstractions;

namespace DAPM.AccessControlService.Core.Services;

public class RepositoryService : IRepositoryService
{
    public Task AddUserRepository(UserDto user, RepositoryDto repository)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<RepositoryDto>> GetRepositoriesForUser(UserDto user)
    {
        throw new NotImplementedException();
    }
}