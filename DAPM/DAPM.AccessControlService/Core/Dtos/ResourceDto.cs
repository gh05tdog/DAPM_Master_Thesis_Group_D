using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Dtos;

public record ResourceDto
{
    public Guid Id { get; set; }
    
    public ResourceId ToResourceId() => new(Id);
}