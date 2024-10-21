using DAPM.AccessControlService.Test.EndToEnd.Dtos;

namespace DAPM.AccessControlService.Test.EndToEnd.Responses;

public class GetPipelinesForUserResponseMessage
{
    public ICollection<PipelineDto> Pipelines { get; set; }
}