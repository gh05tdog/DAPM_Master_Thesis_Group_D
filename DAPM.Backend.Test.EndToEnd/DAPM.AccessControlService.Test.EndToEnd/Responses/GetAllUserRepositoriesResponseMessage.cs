using DAPM.AccessControlService.Test.EndToEnd.Dtos;

namespace DAPM.AccessControlService.Test.EndToEnd.Responses;

public class GetAllUserRepositoriesResponseMessage
{
    public ICollection<UserRepositoryDto> Repositories { get; set; }
}