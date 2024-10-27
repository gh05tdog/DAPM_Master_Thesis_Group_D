using DAPM.AccessControlService.Test.EndToEnd.Dtos;

namespace DAPM.AccessControlService.Test.EndToEnd.Responses;

public class GetAllUserResourcesResponseMessage
{
    public ICollection<UserResourceDto> Resources { get; set; }
}