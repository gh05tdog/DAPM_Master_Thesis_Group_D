using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.ClientApi.AccessControl;

public interface IAccessControlService
{
    Task<ICollection<PipelineDto>> GetUserPipelines(UserDto user);
    Task<ICollection<RepositoryDto>> GetUserRepositories(UserDto user);
    Task<ICollection<ResourceDto>> GetUserResources(UserDto user);
    Task<ICollection<OrganizationDto>> GetUserOrganizations(UserDto user);
    Task<bool> AddUserToPipeline(UserDto user, PipelineDto pipeline);
    Task<bool> AddUserToResource(UserDto user, ResourceDto resource);
    Task<bool> AddUserToRepository(UserDto user, RepositoryDto repository);
    Task<bool> AddUserToOrganization(UserDto user, OrganizationDto organization);
    Task<bool> UserHasAccessToPipeline(UserDto user, PipelineDto pipeline);
    Task<bool> UserHasAccessToResource(UserDto user, ResourceDto resource);
    Task<bool> UserHasAccessToRepository(UserDto user, RepositoryDto repository);
    Task<bool> UserHasAccessToOrganization(UserDto user, OrganizationDto organization);
}