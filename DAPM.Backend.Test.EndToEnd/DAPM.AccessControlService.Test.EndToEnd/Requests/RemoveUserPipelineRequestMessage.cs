using DAPM.AccessControlService.Test.EndToEnd.Dtos;

namespace DAPM.AccessControlService.Test.EndToEnd.Requests;

public class RemoveUserPipelineRequestMessage
{
    public UserDto User { get; set; }
    public PipelineDto Pipeline { get; set; }
}