using DAPM.AccessControlService.Core.Dtos;
using DAPM.AccessControlService.Core.DTOs;
using DAPM.AccessControlService.Core.Services.Abstractions;

namespace DAPM.AccessControlService.Core.Services;

public class ResourceService : IResourceService
{
    public Task AddUserResource(UserDto user, ResourceDto resource)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<ResourceDto>> GetResourcesForUser(UserDto user)
    {
        throw new NotImplementedException();
    }
}