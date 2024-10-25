using DAPM.AccessControlService.Test.EndToEnd.Dtos;

namespace DAPM.AccessControlService.Test.EndToEnd.Requests;

public class RemoveUserResourceRequestMessage
{
    public UserDto User { get; set; }
    public ResourceDto Resource { get; set; }
}