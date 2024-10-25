using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Core.Extensions;
using DAPM.AccessControlService.Core.Services.Abstractions;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Core.Services;

public class ResourceService : IResourceService
{
    private readonly IResourceRepository resourceRepository;

    public ResourceService(IResourceRepository resourceRepository)
    {
        this.resourceRepository = resourceRepository;
    }

    public async Task<bool> AddUserResource(UserDto user, ResourceDto resource)
    {
        var userId = user.ToUserId();
        var resourceId = resource.ToResourceId();
        await resourceRepository.CreateUserResource(userId, resourceId);
        return true;
    }

    public async Task<ICollection<ResourceDto>> GetResourcesForUser(UserDto user)
    {
        var userId = user.ToUserId();
        var resourceIds = await resourceRepository.ReadResourcesForUser(userId);
        return resourceIds.Select(r => new ResourceDto{Id = r.Id}).ToList();
    }
}