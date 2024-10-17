using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Dtos;

public record RepositoryDto
{
    public Guid Id { get; set; }
    
    public RepositoryId ToRepositoryId() => new(Id);
}