using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Dtos;

public record PipelineDto
{
    private readonly Guid id;
    
    public PipelineDto(Guid id)
    {
        this.id = id;
    }

    public PipelineDto(PipelineId pipelineId)
    {
        id = pipelineId.Id;
    }
    
    public PipelineId ToPipelineId() => new(id);
}