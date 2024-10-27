namespace TestUtilities.Dtos;

public class UserAccessRequestDto
{
    public UserDto? User { get; set; }
    public OrganizationDto? Organization { get; set; }
    public PipelineDto? Pipeline { get; set; }
    public ResourceDto? Resource { get; set; }
    public RepositoryDto? Repository { get; set; }
}