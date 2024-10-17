using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Dtos;

public record RepositoryDto
{
    public readonly Guid Id;
    
    public RepositoryDto(Guid id)
    {
        this.Id = id;
    }
    
    public RepositoryDto(RepositoryId repositoryId)
    {
        Id = repositoryId.Id;
    }
    
    public RepositoryId ToRepositoryId() => new(Id);
}