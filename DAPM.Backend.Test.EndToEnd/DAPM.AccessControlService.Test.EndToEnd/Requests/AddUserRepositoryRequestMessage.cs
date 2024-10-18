using DAPM.AccessControlService.Test.EndToEnd.Dtos;

namespace DAPM.AccessControlService.Test.EndToEnd.Requests;

public class AddUserRepositoryRequestMessage
{
    public UserDto User { get; set; }
    public RepositoryDto Repository { get; set; }
}