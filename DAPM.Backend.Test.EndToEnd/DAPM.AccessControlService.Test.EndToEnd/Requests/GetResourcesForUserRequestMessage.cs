using DAPM.AccessControlService.Test.EndToEnd.Dtos;

namespace DAPM.AccessControlService.Test.EndToEnd.Requests;

public class GetResourcesForUserRequestMessage
{
    public UserDto User { get; set; }
}