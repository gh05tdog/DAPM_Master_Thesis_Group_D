using DAPM.AccessControlService.Core.Dtos;
using DAPM.AccessControlService.Core.DTOs;

namespace DAPM.AccessControlService.Core.Services.Abstractions;

public interface IRepositoryService
{
    Task AddUserRepository(UserDto user, RepositoryDto repository);
    Task<ICollection<RepositoryDto>> GetRepositoriesForUser(UserDto user);
}