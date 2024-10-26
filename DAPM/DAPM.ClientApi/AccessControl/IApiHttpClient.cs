using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.ClientApi.AccessControl;

public interface IApiHttpClient
{
    Task<bool> AddUserPipelineAsync(UserPipelineDto request);
    Task<bool> AddUserResourceAsync(UserResourceDto request);
    Task<bool> AddUserRepositoryAsync(UserRepositoryDto request);
    Task<bool> AddUserOrganizationAsync(UserOrganizationDto request);
    Task<ICollection<PipelineDto>> GetPipelinesForUserAsync(UserDto request);
    Task<ICollection<ResourceDto>> GetResourcesForUserAsync(UserDto request);
    Task<ICollection<RepositoryDto>> GetRepositoriesForUserAsync(UserDto request);
    Task<ICollection<OrganizationDto>> GetOrganizationsForUserAsync(UserDto request);
}
