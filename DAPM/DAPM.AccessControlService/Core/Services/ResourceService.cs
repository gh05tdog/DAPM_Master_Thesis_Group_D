using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Core.Dtos;
using DAPM.AccessControlService.Core.DTOs;
using DAPM.AccessControlService.Core.Services.Abstractions;

namespace DAPM.AccessControlService.Core.Services;

public class ResourceService : IResourceService
{
    private readonly IResourceRepository resourceRepository;

    public ResourceService(IResourceRepository resourceRepository)
    {
        this.resourceRepository = resourceRepository;
    }

    public async Task AddUserResource(UserDto user, ResourceDto resource)
    {
        var userId = user.ToUserId();
        var resourceId = resource.ToResourceId();
        await resourceRepository.AddUserResource(userId, resourceId);
    }

    public async Task<ICollection<ResourceDto>> GetResourcesForUser(UserDto user)
    {
        var userId = user.ToUserId();
        var resourceIds = await resourceRepository.GetResourcesForUser(userId);
        return resourceIds.Select(r => new ResourceDto(r)).ToList();
    }
}