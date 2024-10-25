namespace RabbitMQLibrary.Models.AccessControl;

public class UserOrganizationDto
{
    public Guid UserId { get; set; }
    public Guid OrganizationId { get; set; }
}