using DAPM.AccessControlService.Test.EndToEnd.Dtos;

namespace DAPM.AccessControlService.Test.EndToEnd.Responses;

public class GetRepositoriesForUserResponseMessage
{
    public ICollection<RepositoryDto> Repositories { get; set; }
}