using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Dtos;

public record PipelineDto
{
    public readonly Guid Id;
    
    public PipelineDto(Guid id)
    {
        this.Id = id;
    }

    public PipelineDto(PipelineId pipelineId)
    {
        Id = pipelineId.Id;
    }
    
    public PipelineId ToPipelineId() => new(Id);
}