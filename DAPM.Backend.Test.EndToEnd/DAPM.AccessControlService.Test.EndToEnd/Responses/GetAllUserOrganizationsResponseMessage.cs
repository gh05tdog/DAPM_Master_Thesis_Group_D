using DAPM.AccessControlService.Test.EndToEnd.Dtos;

namespace DAPM.AccessControlService.Test.EndToEnd.Responses;

public class GetAllUserOrganizationsResponseMessage
{
    public ICollection<UserOrganizationDto> Organizations { get; set; }
}