using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Dtos;

public record RepositoryDto
{
    private readonly Guid id;
    
    public RepositoryDto(Guid id)
    {
        this.id = id;
    }
    
    public RepositoryDto(RepositoryId repositoryId)
    {
        id = repositoryId.Id;
    }
    
    public RepositoryId ToRepositoryId() => new(id);
}