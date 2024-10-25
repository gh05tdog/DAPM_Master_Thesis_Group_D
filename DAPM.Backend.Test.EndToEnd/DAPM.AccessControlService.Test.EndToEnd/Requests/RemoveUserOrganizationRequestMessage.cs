using DAPM.AccessControlService.Test.EndToEnd.Dtos;

namespace DAPM.AccessControlService.Test.EndToEnd.Requests;

public class RemoveUserOrganizationRequestMessage
{
    public UserDto User { get; set; }
    public OrganizationDto Organization { get; set; }
}