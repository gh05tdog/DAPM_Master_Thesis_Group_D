namespace DAPM.AccessControlService.Test.EndToEnd.Dtos;

public class UserAccessResponseDto
{
    public bool HasOrganizationAccess { get; set; }
    public bool HasPipelineAccess { get; set; }
    public bool HasRepositoryAccess { get; set; }
    public bool HasResourceAccess { get; set; }
}