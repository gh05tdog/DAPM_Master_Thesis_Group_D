using DAPM.AccessControlService.Test.EndToEnd.Dtos;

namespace DAPM.AccessControlService.Test.EndToEnd.Responses;

public class GetOrganizationsForUserResponseMessage
{
    public ICollection<OrganizationDto> Organizations { get; set; }
}