using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Dtos;

public class PipelineDto
{
    public Guid Id { get; set; }
    
    public PipelineId ToPipelineId() => new(Id);
}