using DAPM.AccessControlService.Test.EndToEnd.Dtos;

namespace DAPM.AccessControlService.Test.EndToEnd.Responses;

public class GetAllUserPipelinesResponseMessage
{
    public ICollection<UserPipelineDto> Pipelines { get; set; }
}