using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Dtos;

public record ResourceDto
{
    private readonly Guid id;
    
    public ResourceDto(Guid id)
    {
        this.id = id;
    }
    
    public ResourceDto(ResourceId resourceId)
    {
        id = resourceId.Id;
    }
    
    public ResourceId ToResourceId() => new(id);
}