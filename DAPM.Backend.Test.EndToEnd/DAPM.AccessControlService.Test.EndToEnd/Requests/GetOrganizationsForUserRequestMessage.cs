using DAPM.AccessControlService.Test.EndToEnd.Dtos;

namespace DAPM.AccessControlService.Test.EndToEnd.Requests;

public class GetOrganizationsForUserRequestMessage
{
    public UserDto User { get; set; }
}