using DAPM.AccessControlService.Test.EndToEnd.Dtos;

namespace DAPM.AccessControlService.Test.EndToEnd.Responses;

public class GetResourcesForUserResponseMessage
{
    public ICollection<ResourceDto> Resources { get; set; }
}