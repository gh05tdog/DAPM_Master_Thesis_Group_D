namespace RabbitMQLibrary.Models.AccessControl;

public class UserOrganizationDto
{
    Guid UserId { get; set; }
    Guid OrganizationId { get; set; }
}