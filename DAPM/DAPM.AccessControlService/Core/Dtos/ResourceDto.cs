using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Dtos;

public record ResourceDto
{
    public readonly Guid Id;
    
    public ResourceDto(Guid id)
    {
        this.Id = id;
    }
    
    public ResourceDto(ResourceId resourceId)
    {
        Id = resourceId.Id;
    }
    
    public ResourceId ToResourceId() => new(Id);
}